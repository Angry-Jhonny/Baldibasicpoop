using Baldibasicpoop.CustomItems;
using Baldibasicpoop.Editor;
using Baldibasicpoop.NPCS;
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
using PlusStudioLevelFormat;
using PlusStudioLevelLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Baldibasicpoop
{
    [BepInPlugin("baldicancerpoop", "Baldi Poop", "1.2.0")]
    
    [BepInDependency("mtm101.rulerp.baldiplus.levelstudio", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("mtm101.rulerp.baldiplus.levelstudioloader", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("mtm101.rulerp.bbplus.baldidevapi")]

    public class BasePlugin : BaseUnityPlugin
    {
        public static BasePlugin Instance { get; internal set; }

        public AssetManager assetMan = new AssetManager();
        public MaterialMaker matMaker = new MaterialMaker();

        public static RoomCategory beanzRoomCat = EnumExtensions.ExtendEnum<RoomCategory>("BeanzRoom");

        private IEnumerator RegisterImportant()
        {
            yield return 3;
            yield return "Preloading...";
            try
            {
                // ASSETS //
                    // Images //

                assetMan.Add<Sprite>("MainMenuImage", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "PoopMainMenu.png"), 96));

                assetMan.Add<Texture2D>("BeanWall", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanWall.png"));
                assetMan.Add<Texture2D>("BeanCeil", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanCeiling.png"));
                assetMan.Add<Texture2D>("BeanFloor", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanCarpet.png"));
                assetMan.Add<Texture2D>("Connor", AssetLoader.TextureFromMod(this, "Rooms", "NonCanonConnor", "NonCanonConnor.png"));



                assetMan.Add<Sprite>("Benz_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "MrBen", "MrBen.png"), new Vector2(0.5f, 0.4f), 32));
                assetMan.Add<Sprite>("Benz_Explod", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "MrBen", "MrBenExplodsisv.png"), new Vector2(0.5f, 0.4f), 32));

                assetMan.Add<Sprite>("Mii13_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Mystman13", "Mii13.png"), new Vector2(0.5f, 0.4f), 32));

                assetMan.Add<Sprite>("DYL_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BigDylan", "DYL_Idle.png"), new Vector2(0.5f, 0.4f), 48));
                assetMan.Add<Sprite>("DYL_Stare", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BigDylan", "DYL_Stare.png"), new Vector2(0.5f, 0.4f), 48));
                assetMan.Add<Sprite>("DYL_yhejoseph", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BigDylan", "JOS_Chase.png"), new Vector2(0.5f, 0.4f), 48));



                assetMan.Add<Sprite>("PringulsLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Pringuls", "PringulsIcon_Large.png"), 50));
                assetMan.Add<Sprite>("PringulsSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Pringuls", "PringulsIcon_Small.png"), 25));

                assetMan.Add<Sprite>("ShitLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Shit", "ShitIcon_Large.png"), 50));
                assetMan.Add<Sprite>("ShitSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Shit", "ShitIcon_Small.png"), 25));



                assetMan.Add<Sprite>("BeanPhoneSprite", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Objects", "Billboards", "MrBenPhone.png"), new Vector2(0.5f, 0), 24));
                assetMan.Add<Sprite>("BeanLampSprite", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Objects", "Billboards", "MrBenLamp.png"), new Vector2(0.5f, 0), 24));



                assetMan.Add<Texture2D>("PST_UglyKids", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_UglyKids.png")));
                assetMan.Add<Texture2D>("PST_BaldiSong", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_BaldiSong.png")));
                assetMan.Add<Texture2D>("PST_Chef", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_Chef.png")));
                assetMan.Add<Texture2D>("PST_Depression", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_Depression.png")));
                assetMan.Add<Texture2D>("PST_Wide", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_Wide.png")));



                assetMan.Add<Sprite>("Editor_MrBenz", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "npc_benz.png"), 1));
                assetMan.Add<Sprite>("Editor_Mii13", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "npc_mii13.png"), 1));
                assetMan.Add<Sprite>("Editor_Dylan", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "npc_dylan.png"), 1));
                assetMan.Add<Sprite>("Editor_BeanPhone", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "object_beanphone.png"), 1));
                assetMan.Add<Sprite>("Editor_BeanLamp", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "object_beanlamp.png"), 1));
                assetMan.Add<Sprite>("Editor_ConnorBall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "object_connorball.png"), 1));
                assetMan.Add<Sprite>("Editor_BeanHouse", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "room_beanhouse.png"), 1));
                assetMan.Add<Sprite>("Editor_Connor", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "room_connor.png"), 1));

                // SoundObject //

                assetMan.Add<SoundObject>("SFX_ChipsFall", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("Item", "Pringuls", "SFX_ChipsFall.wav")), "SFX_ChipsFall", SoundType.Effect, Color.white, -1f));
                assetMan.Add<SoundObject>("SFX_Die", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("Item", "Shit", "SFX_Die.wav")), "SFX_Die", SoundType.Voice, Color.white, -1f));

                assetMan.Add<SoundObject>("GS_Cleaning", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "GottaSweep", "GS_Cleaning.wav")), "GS_Cleaning", SoundType.Effect, Color.green, -1f));

                assetMan.Add<SoundObject>("BEN_Explod", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Explod.wav")), "BEN_Explod", SoundType.Effect, new Color(131f / 255f, 75f / 255f, 55f / 255f), -1f));

                assetMan.Add<SoundObject>("Mii13_Hey", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "Mystman13", "Mii13_Hey.wav")), "Mii13_Hey", SoundType.Voice, new Color(51f / 255f, 59f / 255f, 67f / 255f), -1f));

                assetMan.Add<SoundObject>("SFX_Wiplash", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "BigDylan", "SFX_Wiplash.wav")), "SFX_Wiplash", SoundType.Effect, Color.red, -1f));
                assetMan.Add<SoundObject>("JOS_Screm", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "BigDylan", "JOS_Screm.wav")), "JOS_Screm", SoundType.Effect, Color.red, -1f));

                ////////////////////////////////////////////////// OBJECTS //////////////////////////////////////////////////

                GameObject BeanzPhoneBase = GameObject.Instantiate<GameObject>(Resources.FindObjectsOfTypeAll<EnvironmentObject>().First(x => x.name == "Plant" && x.GetInstanceID() >= 0 && x.transform.parent == null).gameObject, MTM101BaldiDevAPI.prefabTransform);
                BeanzPhoneBase.GetComponentInChildren<SpriteRenderer>().sprite = assetMan.Get<Sprite>("BeanPhoneSprite");
                BeanzPhoneBase.name = "BeanzPhone";
                assetMan.Add<GameObject>("BeanzPhone", BeanzPhoneBase);
                LevelLoaderPlugin.Instance.basicObjects.Add("beanzphone", assetMan.Get<GameObject>("BeanzPhone"));

                GameObject BeanzLampBase = GameObject.Instantiate<GameObject>(Resources.FindObjectsOfTypeAll<EnvironmentObject>().First(x => x.name == "Plant" && x.GetInstanceID() >= 0 && x.transform.parent == null).gameObject, MTM101BaldiDevAPI.prefabTransform);
                BeanzLampBase.GetComponentInChildren<SpriteRenderer>().sprite = assetMan.Get<Sprite>("BeanLampSprite");
                BeanzLampBase.name = "BeanzLamp";
                assetMan.Add<GameObject>("BeanzLamp", BeanzLampBase);
                LevelLoaderPlugin.Instance.basicObjects.Add("beanzlamp", assetMan.Get<GameObject>("BeanzLamp"));

                GameObject ConnorBall = new GameObject("ConnorBall");
                ConnorBall.transform.SetParent(MTM101BaldiDevAPI.prefabTransform);
                GameObject ConnorBallMesh = new GameObject("ConnorBallMesh");
                ConnorBallMesh.transform.SetParent(ConnorBall.transform);
                ConnorBallMesh.AddComponent<MeshFilter>().mesh = Resources.FindObjectsOfTypeAll<Mesh>().First(x => x.name == "Sphere" && x.GetInstanceID() >= 0);
                MeshRenderer renderer = ConnorBallMesh.AddComponent<MeshRenderer>();
                renderer.material = matMaker.MakeMaterial(assetMan, "Connor");
                renderer.material.mainTexture = assetMan.Get<Texture2D>("Connor");
                ConnorBallMesh.transform.localScale = Vector3.one * 8;
                ConnorBallMesh.transform.position = new Vector3(0, 5, 0);
                ConnorBallMesh.AddComponent<SphereCollider>().radius = 0.8f;
                //Spinner spinner = ConnorBall.AddComponent<Spinner>();
                //spinner.ReflectionSetVariable("target", ConnorBallMesh.transform);
                assetMan.Add<GameObject>("ConnorBall", ConnorBall);
                LevelLoaderPlugin.Instance.basicObjects.Add("connorball", assetMan.Get<GameObject>("ConnorBall"));


                ////////////////////////////////////////////////// ROOMS ////////////////////////////////////////////////////

                assetMan.Add<StandardDoorMats>("BeanzDoorMats", ObjectCreators.CreateDoorDataObject("BeanDoor", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanDoor_Open.png"), AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanDoor_Closed.png")));
                LevelLoaderPlugin.Instance.roomSettings.Add("beanHouse", new RoomSettings(BasePlugin.beanzRoomCat, RoomType.Room, new Color(131f / 255f, 75f / 255f, 55f / 255f), assetMan.Get<StandardDoorMats>("BeanzDoorMats"), null));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzFloor", assetMan.Get<Texture2D>("BeanFloor"));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzWall", assetMan.Get<Texture2D>("BeanWall"));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzCeil", assetMan.Get<Texture2D>("BeanCeil"));

                string beanRoomPath = Path.Combine(AssetLoader.GetModPath(this), "Rooms", "BeanzHouse", "beanhouse.rbpl");

                List<WeightedRoomAsset> potentialBeanzRoom = new List<WeightedRoomAsset>();
                BinaryReader reader = new BinaryReader(File.OpenRead(beanRoomPath));
                BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
                reader.Close();
                ExtendedRoomAsset beanroomasset = LevelImporter.CreateRoomAsset(formatAsset);
                beanroomasset.lightPre = LevelLoaderPlugin.Instance.lightTransforms["standardhanging"];
                potentialBeanzRoom.Add(new WeightedRoomAsset()
                {
                    selection = beanroomasset,
                    weight = 999999999
                });



                assetMan.Add<StandardDoorMats>("ConnorDoorMats", ObjectCreators.CreateDoorDataObject("ConnorDoor", AssetLoader.TextureFromMod(this, "Rooms", "NonCanonConnor", "Connor_Open.png"), AssetLoader.TextureFromMod(this, "Rooms", "NonCanonConnor", "Connor_Closed.png")));
                LevelLoaderPlugin.Instance.roomSettings.Add("connorRoom", new RoomSettings(BasePlugin.beanzRoomCat, RoomType.Room, Color.white, assetMan.Get<StandardDoorMats>("ConnorDoorMats"), null));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("ConnorTexture", assetMan.Get<Texture2D>("Connor"));

                ////////////////////////////////////////////////// NPC'S //////////////////////////////////////////////////

                MisterBenz benz = new NPCBuilder<MisterBenz>(base.Info)
                    .SetName("Mister Benz")
                    .AddTrigger()
                    .SetEnum("MrBenz")
                    .SetMinMaxAudioDistance(10f, 150f)
                    .AddSpawnableRoomCategories(beanzRoomCat)
                    .AddPotentialRoomAsset(beanroomasset, 100)
                    .SetWanderEnterRooms()
                    .IgnorePlayerOnSpawn()
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/MrBen/PRI_Benz.png"), "PRI_Beanz1", "PRI_Beanz2")
                    .Build();
                benz.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Idle");
                assetMan.Add<NPC>("MrBenz", benz);


                Mystman13 Mii13 = new NPCBuilder<Mystman13>(base.Info)
                    .SetName("Mystman13")
                    .AddTrigger()
                    .SetEnum("Mii13")
                    .AddLooker()
                    .SetMinMaxAudioDistance(10f, 150f)
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/Mystman13/PRI_Mii13.png"), "PRI_Mii131", "PRI_Mii132")
                    .Build();
                Mii13.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Mii13_Idle");
                assetMan.Add<NPC>("Mii13", Mii13);


                BigDylan dylan = new NPCBuilder<BigDylan>(base.Info)
                    .SetName("Big Dylan")
                    .AddTrigger()
                    .SetEnum("BigDylan")
                    .AddLooker()
                    .SetMinMaxAudioDistance(25f, 300f)
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/BigDylan/PRI_Dylan.png"), "PRI_Dylan1", "PRI_Dylan2")
                    .Build();
                dylan.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("DYL_Idle");
                assetMan.Add<NPC>("BigDylan", dylan);

                ////////////////////////////////////////////////// ITEMS /////////////////////////////////////////////////////

                ItemObject pringuls = new ItemBuilder(Info)
                    .SetNameAndDescription("Itm_Pringuls", "Desc_Pringuls")
                    .SetSprites(assetMan.Get<Sprite>("PringulsSmall"), assetMan.Get<Sprite>("PringulsLarge"))
                    .SetEnum("Pringuls")
                    .SetShopPrice(140)
                    .SetGeneratorCost(30)
                    .SetItemComponent<ITM_Pringuls>()
                    .SetMeta(ItemFlags.Persists, new string[] { "food" })
                    .Build();
                assetMan.Add<ItemObject>("Pringuls", pringuls);

                ItemObject shit = new ItemBuilder(Info)
                    .SetNameAndDescription("Itm_Shit", "Desc_Shit")
                    .SetSprites(assetMan.Get<Sprite>("ShitSmall"), assetMan.Get<Sprite>("ShitLarge"))
                    .SetEnum("Shit")
                    .SetShopPrice(10)
                    .SetGeneratorCost(70)
                    .SetItemComponent<ITM_Shit>()
                    .SetMeta(ItemFlags.Persists, new string[] { "food" })
                    .Build();
                ((ITM_Shit)shit.item).eatSound = (SoundObject)((ITM_ZestyBar)ItemMetaStorage.Instance.FindByEnum(Items.ZestyBar).value.item).ReflectionGetVariable("audEat");
                ((ITM_Shit)shit.item).dieSound = assetMan.Get<SoundObject>("SFX_Die");
                assetMan.Add<ItemObject>("Shit", shit);

                ////////////////////////////////////////////////// POSTERS //////////////////////////////////////////////////

                PosterTextData[] TextNone = new PosterTextData[] { new PosterTextData() { } };

                PosterTextData[] PST_UglyKidsTextData = new PosterTextData[] {
                    new PosterTextData {
                    textKey = "PST_UglyKids",
                    font = BaldiFonts.BoldComicSans12.FontAsset(),
                    fontSize = (int)BaldiFonts.BoldComicSans12.FontSize(),
                    position = new IntVector2(46,129),
                    size = new IntVector2(83,24),
                    style = TMPro.FontStyles.Normal,
                    color = Color.black,
                    alignment = TMPro.TextAlignmentOptions.Center}
                };

                PosterObject PST_UglyKids = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_UglyKids"), PST_UglyKidsTextData);
                assetMan.Add<PosterObject>("PST_UglyKids", PST_UglyKids);

                PosterTextData[] PST_BaldiSongTextData = new PosterTextData[] {
                    new PosterTextData {
                    textKey = "PST_BaldiSong1",
                    font = BaldiFonts.ComicSans18.FontAsset(),
                    fontSize = (int)BaldiFonts.ComicSans18.FontSize(),
                    position = new IntVector2(52,175),
                    size = new IntVector2(152,58),
                    style = TMPro.FontStyles.Normal,
                    color = Color.black,
                    alignment = TMPro.TextAlignmentOptions.Center},
                    new PosterTextData {
                    textKey = "PST_BaldiSong2",
                    font = BaldiFonts.ComicSans18.FontAsset(),
                    fontSize = (int)BaldiFonts.ComicSans18.FontSize(),
                    position = new IntVector2(52,23),
                    size = new IntVector2(152,58),
                    style = TMPro.FontStyles.Normal,
                    color = Color.black,
                    alignment = TMPro.TextAlignmentOptions.Center}
                };

                PosterObject PST_BaldiSong = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_BaldiSong"), PST_BaldiSongTextData);
                assetMan.Add<PosterObject>("PST_BaldiSong", PST_BaldiSong);

                PosterObject PST_Depression = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_Depression"), TextNone);
                assetMan.Add<PosterObject>("PST_Depression", PST_Depression);

                PosterObject PST_Chef = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_Chef"), TextNone);
                assetMan.Add<PosterObject>("PST_Chef", PST_Chef);

                PosterTextData[] PST_WideTextData = new PosterTextData[] {
                    new PosterTextData {
                    textKey = "PST_Wide",
                    font = BaldiFonts.ComicSans18.FontAsset(),
                    fontSize = (int)BaldiFonts.ComicSans18.FontSize(),
                    position = new IntVector2(4,216),
                    size = new IntVector2(248,36),
                    style = TMPro.FontStyles.Normal,
                    color = Color.black,
                    alignment = TMPro.TextAlignmentOptions.Center}
                };

                PosterObject PST_Wide = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_Wide"), PST_WideTextData);
                assetMan.Add<PosterObject>("PST_Wide", PST_Wide);

                ////////////////////////////////////////////////// GENERATOR SETTINGS //////////////////////////////////////////////////

                GeneratorManagement.Register(this, GenerationModType.Finalizer, delegate (string level, int levelNum, SceneObject obj)
                {
                    foreach (CustomLevelObject customLevelObject in obj.GetCustomLevelObjects())
                    {
                        // NPCS
                        if (level == "F1" || level == "END") {
                            obj.forcedNpcs = obj.forcedNpcs.AddToArray(benz);
                            obj.forcedNpcs = obj.forcedNpcs.AddToArray(dylan);
                        }
                        else if (level == "F2")
                        {
                            obj.potentialNPCs.Add(new WeightedNPC
                            {
                                weight = 100,
                                selection = Mii13
                            });
                        }

                        // POSTERS

                        customLevelObject.posters = customLevelObject.posters.AddToArray(new WeightedPosterObject
                        {
                            weight = 60,
                            selection = PST_UglyKids
                        });
                        customLevelObject.posters = customLevelObject.posters.AddToArray(new WeightedPosterObject
                        {
                            weight = 20,
                            selection = PST_BaldiSong
                        });
                        customLevelObject.posters = customLevelObject.posters.AddToArray(new WeightedPosterObject
                        {
                            weight = 10,
                            selection = PST_Depression
                        });
                        customLevelObject.posters = customLevelObject.posters.AddToArray(new WeightedPosterObject
                        {
                            weight = 90,
                            selection = PST_Chef
                        });
                        customLevelObject.posters = customLevelObject.posters.AddToArray(new WeightedPosterObject
                        {
                            weight = 100,
                            selection = PST_Wide
                        });

                        // ITEMS

                        customLevelObject.potentialItems = customLevelObject.potentialItems.AddToArray(new WeightedItemObject
                        {
                            weight = 79,
                            selection = pringuls
                        });
                        obj.shopItems = obj.shopItems.AddItem(new WeightedItemObject
                        {
                            selection = pringuls,
                            weight = 79
                        }).ToArray();

                        customLevelObject.potentialItems = customLevelObject.potentialItems.AddToArray(new WeightedItemObject
                        {
                            weight = 15,
                            selection = shit
                        });
                        obj.shopItems = obj.shopItems.AddItem(new WeightedItemObject
                        {
                            selection = shit,
                            weight = 15
                        }).ToArray();
                        // ROOMS
                    }
                });

                LoaderSupport.AddLoaderContent();
                if (Chainloader.PluginInfos.ContainsKey("mtm101.rulerp.baldiplus.levelstudio"))
                {
                    EditorSupport.AddEditorContent();
                }

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
