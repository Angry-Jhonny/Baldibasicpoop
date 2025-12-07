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
                assetMan.Add<Sprite>("Benz_Idle", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("NPC/MrBen/MrBen.png"), 40));
                assetMan.Add<Sprite>("Benz_Explod", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("NPC/MrBen/MrBen.png"), 40));
    
                AssetLoader.LocalizationFromMod(this);
    
                PosterObject benzPoster = ObjectCreators.CreateCharacterPoster(assetMan.Get<Texture2D>("Sprites/Point-Pointer/Poster"), "PST_MisterBenz_Name", "PST_MisterBenz_Desc");
                //PosterObject Poster = ObjectCreators.CreatePosterObject(); // HOW TF DO I CODE THIS!!!!!
    
                MisterBenz benz = new NPCBuilder<MisterBenz>(base.Info).AddTrigger().SetEnum("MrBenz").SetName("Mister Benz").SetMinMaxAudioDistance(0f, 100f).SetWanderEnterRooms().SetPoster(benzPoster).Build();
    
                GeneratorManagement.Register(this, GenerationModType.Finalizer, delegate (string level, int levelNum, SceneObject obj)
                {
                    foreach (CustomLevelObject customLevelObject in obj.GetCustomLevelObjects())
                    {
                        if (obj.levelTitle.StartsWith("F1"))
                        {
                            obj.potentialNPCs.Add(new WeightedNPC
                            {
                                weight = 999,
                                selection = benz
                            });
                        }
                    }
                });
            }
            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break
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
