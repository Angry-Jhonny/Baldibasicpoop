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

                ////////////////////////////////////////////////// NPC'S //////////////////////////////////////////////////

                assetMan.Add<Texture2D>("Benz_Idle_Tex", AssetLoader.TextureFromMod(this, Path.Combine("NPC", "MrBen", "MrBen.png")));
                assetMan.Add<Texture2D>("Benz_Explod_Tex", AssetLoader.TextureFromMod(this, Path.Combine("NPC", "MrBen", "MrBenExplodsisv.png")));
                assetMan.Add<Sprite>("Benz_Idle", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("Benz_Idle_Tex"), 40));
                assetMan.Add<Sprite>("Benz_Explod", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("Benz_Explod_Tex"), 40));

                assetMan.Add<SoundObject>("BEN_Explod", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(BasePlugin.Instance, Path.Combine("NPC", "MrBen", "BEN_Explod.wav")), "BEN_Explod", SoundType.Effect, new Color(135 / 255, 115 / 255, 97 / 255), -1f));

                MisterBenz benz = new NPCBuilder<MisterBenz>(base.Info)
                    .SetName("Mister Benz")
                    .AddTrigger()
                    .SetEnum("MrBenz")
                    .SetForcedSubtitleColor(new Color(135 / 255,115 / 255,97 / 255))
                    .SetMinMaxAudioDistance(10f, 150f)
                    .SetWanderEnterRooms()
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/MrBen/PRI_Benz.png"), "PRI_Beanz1", "PRI_Beanz2")
                    .Build();

                ////////////////////////////////////////////////// ITEMS //////////////////////////////////////////////////

                assetMan.Add<Texture2D>("PringulsLarge_Tex", AssetLoader.TextureFromMod(this, Path.Combine("Item", "Pringuls", "PringulsIcon_Large.png")));
                assetMan.Add<Texture2D>("PringulsSmall_Tex", AssetLoader.TextureFromMod(this, Path.Combine("Item", "Pringuls", "PringulsIcon_Small.png")));
                assetMan.Add<Sprite>("PringulsLarge", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("PringulsLarge_Tex"), 40));
                assetMan.Add<Sprite>("PringulsSmall", AssetLoader.SpriteFromTexture2D(assetMan.Get<Texture2D>("PringulsSmall_Tex"), 40));

                assetMan.Add<SoundObject>("SFX_ChipsFall", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(BasePlugin.Instance, Path.Combine("Item", "Pringuls", "SFX_ChipsFall.wav")), "SFX_ChipsFall", SoundType.Effect, Color.white, -1f));

                //EnvironmentObject PringulMess = new ObjectBuilder(Info)

                ItemObject Pringuls = new ItemBuilder(Info)
                    .SetNameAndDescription("Itm_Pringuls", "Desc_Pringuls")
                    .SetSprites(assetMan.Get<Sprite>("PringulsSmall"), assetMan.Get<Sprite>("PringulsLarge"))
                    .SetEnum("CottonCandy")
                    .SetShopPrice(480)
                    .SetGeneratorCost(40)
                    .SetItemComponent<ITM_Pringuls>()
                    .SetMeta(ItemFlags.None, new string[] { "food" })
                    .Build();
                ((ITM_Pringuls)Pringuls.item).dropSound = BasePlugin.Instance.assetMan.Get<SoundObject>("SFX_ChipsFall");
                assetMan.Add<ItemObject>("Pringuls", Pringuls);

                ////////////////////////////////////////////////// POSTERS //////////////////////////////////////////////////

                assetMan.Add<Texture2D>("PST_UglyKids", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "UglyKids.png")));

                PosterObject PST_UglyKids = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_UglyKids"), null);

                ////////////////////////////////////////////////// GENERATOR SETTINGS //////////////////////////////////////////////////

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
                        // to generate custom posters, you simply do this below
                        customLevelObject.posters = customLevelObject.posters.AddToArray(new WeightedPosterObject
                        {
                            weight = 60, // weight things (chance to spawn)
                            selection = PST_UglyKids
                        });
                    }
                });

                ////////////////////////////////////////////////// LOCALIZATION //////////////////////////////////////////////////

                AssetLoader.LocalizationFromMod(this);
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
