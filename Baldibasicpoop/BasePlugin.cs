using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.AssetTools.SpriteSheets;
using MTM101BaldAPI.Components;
using MTM101BaldAPI.ObjectCreation;
using MTM101BaldAPI.OptionsAPI;
using MTM101BaldAPI.Reflection;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI.SaveSystem;
using MTM101BaldAPI.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MTM101BaldAPI.Components.Animation;

namespace Baldibasicpoop
{

    [BepInPlugin("baldicancerpoop", "Angry Productions", "1.0.0")]

    [BepInDependency("mtm101.rulerp.bbplus.baldidevapi")]

    public class BasePlugin : BaseUnityPlugin
    {
        public static BasePlugin Instance { get; internal set; }

        public AssetManager assetMan = new AssetManager();

        private IEnumerator RegisterImportant()
        {
            yield return 3;
            yield return "Preloading...";
            try
            {
                assetMan.Add<Texture2D>("Benz_Idle_Tex", AssetLoader.TextureFromMod(this, Path.Combine("NPC", "MrBen", "MrBen.png")));
                assetMan.Add<Texture2D>("Benz_Explod_Tex", AssetLoader.TextureFromMod(this, Path.Combine("NPC", "MrBen", "MrBenExplodsisv.png")));
                assetMan.Add<Sprite>("Benz_Idle", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("Benz_Idle_Tex"), 40));
                assetMan.Add<Sprite>("Benz_Explod", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("Benz_Explod_Tex"), 40));
                assetMan.Add<SoundObject>("BEN_Explod", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(BasePlugin.Instance, Path.Combine("NPC", "MrBen", "BEN_Explod.wav")), "*EXPLOSION*", SoundType.Effect, Color.white, -1f));
                AssetLoader.LocalizationFromMod(this);

                //PosterObject Poster = ObjectCreators.CreatePosterObject(); // HOW TF DO I CODE THIS!!!!!
    
                MisterBenz benz = new NPCBuilder<MisterBenz>(base.Info)
                    .SetName("Mister Benz")
                    .AddTrigger()
                    .SetEnum("MrBenz")
                    .SetMinMaxAudioDistance(10f, 150f)
                    .SetWanderEnterRooms()
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/MrBen/PRI_Benz.png"), "PST_MisterBenz_Name", "PST_MisterBenz_Desc")
                    .Build();
    
                GeneratorManagement.Register(this, GenerationModType.Finalizer, delegate (string level, int levelNum, SceneObject obj)
                {
                    foreach (CustomLevelObject customLevelObject in obj.GetCustomLevelObjects())
                    {
                        // removed the if statement so the npc will force spawn on every floor
                        obj.potentialNPCs.Add(new WeightedNPC
                        {
                            weight = 999,
                            selection = benz
                        });
                    
                    }
                });
            }
            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        void Awake()
        {
            Instance = this;
            Harmony harmony = new Harmony("baldicancerpoop");

            ModdedSaveGame.AddSaveHandler(Info);

            harmony.PatchAllConditionals();

            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterImportant(), LoadingEventOrder.Pre);
        }
    }
}
