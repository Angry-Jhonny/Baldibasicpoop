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
using PlusStudioLevelLoader;
using PlusStudioLevelFormat;
using MTM101BaldAPI.Components.Animation;

namespace Baldibasicpoop
{

    [BepInPlugin("baldicancerpoop", "Baldi Poop", "1.1.0")]
    
    [BepInDependency("mtm101.rulerp.baldiplus.levelstudio", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("mtm101.rulerp.baldiplus.levelstudioloader", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("mtm101.rulerp.bbplus.baldidevapi")]

    public class BasePlugin : BaseUnityPlugin
    {
        public static BasePlugin Instance { get; internal set; }

        public AssetManager assetMan = new AssetManager();

        public static RoomCategory beanzRoomCat = EnumExtensions.ExtendEnum<RoomCategory>("BeanzRoom");

        private IEnumerator RegisterImportant()
        {
            yield return 3;
            yield return "Preloading...";
            try
            {
                // ASSETS //
                    // Images //

                assetMan.Add<Texture2D>("BeanWall", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanWall.png"));
                assetMan.Add<Texture2D>("BeanCeil", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanCeiling.png"));
                assetMan.Add<Texture2D>("BeanFloor", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanCarpet.png"));

                assetMan.Add<Sprite>("Benz_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "MrBen", "MrBen.png"), new Vector2(0.5f, 0.4f), 32));
                assetMan.Add<Sprite>("Benz_Explod", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "MrBen", "MrBenExplodsisv.png"), new Vector2(0.5f, 0.4f), 32));

                assetMan.Add<Sprite>("PringulsLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Pringuls", "PringulsIcon_Large.png"), 50));
                assetMan.Add<Sprite>("PringulsSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Pringuls", "PringulsIcon_Small.png"), 25));
                assetMan.Add<Sprite>("PringulsMess", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Pringuls", "PringulChip.png"), 32));

                assetMan.Add<Sprite>("BeanPhoneSprite", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Objects", "Billboards", "MrBenPhone.png"), 96));

                assetMan.Add<Texture2D>("PST_UglyKids", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "UglyKids.png")));

                // SoundObject //

                assetMan.Add<SoundObject>("SFX_ChipsFall", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(BasePlugin.Instance, Path.Combine("Item", "Pringuls", "SFX_ChipsFall.wav")), "SFX_ChipsFall", SoundType.Effect, Color.white, -1f));

                assetMan.Add<SoundObject>("GS_Cleaning", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(BasePlugin.Instance, Path.Combine("NPC", "GottaSweep", "GS_Cleaning.wav")), "GS_Cleaning", SoundType.Effect, Color.white, -1f));
                assetMan.Add<SoundObject>("BEN_Explod", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(BasePlugin.Instance, Path.Combine("NPC", "MrBen", "BEN_Explod.wav")), "BEN_Explod", SoundType.Effect, new Color(135 / 255, 115 / 255, 97 / 255), -1f));

                ////////////////////////////////////////////////// OBJECTS //////////////////////////////////////////////////

                GameObject BeanzPhoneBase = GameObject.Instantiate<GameObject>(Resources.FindObjectsOfTypeAll<EnvironmentObject>().First(x => x.name == "Plant" && x.GetInstanceID() >= 0 && x.transform.parent == null).gameObject, MTM101BaldiDevAPI.prefabTransform);
                BeanzPhoneBase.GetComponentInChildren<SpriteRenderer>().sprite = assetMan.Get<Sprite>("BeanPhoneSprite");
                BeanzPhoneBase.name = "BeanzPhone";
                assetMan.Add<GameObject>("BeanzPhone", BeanzPhoneBase);

                ////////////////////////////////////////////////// ROOMS //////////////////////////////////////////////////

                assetMan.Add<StandardDoorMats>("BeanzDoorMats", ObjectCreators.CreateDoorDataObject("BeanDoor", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanDoor_Open.png"), AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanDoor_Closed.png")));
                LevelLoaderPlugin.Instance.roomSettings.Add("beanHouse", new RoomSettings(BasePlugin.beanzRoomCat, RoomType.Room, new Color(131f / 255f, 75f / 255f, 55f / 255f), assetMan.Get<StandardDoorMats>("BeanzDoorMats"), null));
                LevelLoaderPlugin.Instance.basicObjects.Add("bean_Phone", assetMan.Get<GameObject>("BeanzPhone"));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzFloor", assetMan.Get<Texture2D>("BeanFloor"));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzWall", assetMan.Get<Texture2D>("BeanWall"));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzCeil", assetMan.Get<Texture2D>("BeanCeil"));

                string beanRoomPath = Path.Combine(AssetLoader.GetModPath(this), "Rooms", "BeanzHouse", "beanhouse_4_5.rbpl");

                List<WeightedRoomAsset> potentialBeanzRoom = new List<WeightedRoomAsset>();
                BinaryReader reader = new BinaryReader(File.OpenRead(beanRoomPath));
                BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
                reader.Close();
                ExtendedRoomAsset beanroomasset = LevelImporter.CreateRoomAsset(formatAsset);
                beanroomasset.lightPre = LevelLoaderPlugin.Instance.lightTransforms["standardhanging"];

                ////////////////////////////////////////////////// NPC'S //////////////////////////////////////////////////

                MisterBenz benz = new NPCBuilder<MisterBenz>(base.Info)
                    .SetName("Mister Benz")
                    .AddTrigger()
                    .SetEnum("MrBenz")
                    .SetForcedSubtitleColor(new Color(135 / 255, 115 / 255, 97 / 255))
                    .SetMinMaxAudioDistance(10f, 150f)
                    .AddPotentialRoomAssets(potentialBeanzRoom.ToArray())
                    .SetWanderEnterRooms()
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/MrBen/PRI_Benz.png"), "PRI_Beanz1", "PRI_Beanz2")
                    .Build();

                ////////////////////////////////////////////////// ITEMS //////////////////////////////////////////////////

                Entity PringulsMess = new EntityBuilder()
                    .SetName("Pringuls")
                    .AddTrigger(1f)
                    .SetLayerCollisionMask(2113541)
                    .Build();
                SpriteRenderer pringulmessRenderer = PringulsMess.gameObject.AddComponent<SpriteRenderer>();
                pringulmessRenderer.sprite = assetMan.Get<Sprite>("PringulsMess");
                ObjectPringulsMess pringulmess = PringulsMess.gameObject.AddComponent<ObjectPringulsMess>();
                pringulmess.audMan = pringulmess.gameObject.AddComponent<PropagatedAudioManager>();
                pringulmess.audClean = BasePlugin.Instance.assetMan.Get<SoundObject>("GS_Cleaning");

                ItemObject Pringuls = new ItemBuilder(Info)
                    .SetNameAndDescription("Itm_Pringuls", "Desc_Pringuls")
                    .SetSprites(assetMan.Get<Sprite>("PringulsSmall"), assetMan.Get<Sprite>("PringulsLarge"))
                    .SetEnum("Pringuls")
                    .SetShopPrice(480)
                    .SetGeneratorCost(40)
                    .SetItemComponent<ITM_Pringuls>()
                    .SetMeta(ItemFlags.Persists, new string[] { "food" })
                    .Build();
                ((ITM_Pringuls)Pringuls.item).dropSound = BasePlugin.Instance.assetMan.Get<SoundObject>("SFX_ChipsFall");
                ((ITM_Pringuls)Pringuls.item).PringulMessObject = PringulsMess;
                assetMan.Add<ItemObject>("Pringuls", Pringuls);

                ////////////////////////////////////////////////// POSTERS //////////////////////////////////////////////////

                PosterObject PST_UglyKids = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_UglyKids"), null);

                ////////////////////////////////////////////////// GENERATOR SETTINGS //////////////////////////////////////////////////

                GeneratorManagement.Register(this, GenerationModType.Finalizer, delegate (string level, int levelNum, SceneObject obj)
                {
                    foreach (CustomLevelObject customLevelObject in obj.GetCustomLevelObjects())
                    {
                        // NPCS
                        obj.potentialNPCs.Add(new WeightedNPC
                        {
                            weight = 999,
                            selection = benz
                        });
                        // POSTERS
                        customLevelObject.posters = customLevelObject.posters.AddToArray(new WeightedPosterObject
                        {
                            weight = 60,
                            selection = PST_UglyKids
                        });
                        // ROOMS
                        //potentialBeanzRoom.Add(new WeightedRoomAsset()
                        //{
                        //    selection = beanroomasset,
                        //    weight = 100
                        //});
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
