using Baldibasicpoop.CustomItems;
using Baldibasicpoop.Editor;
using Baldibasicpoop.Events;
using Baldibasicpoop.Helpers;
using Baldibasicpoop.NPCS;
using Baldibasicpoop.Structures;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.AssetTools.SpriteSheets;
using MTM101BaldAPI.ObjectCreation;
using MTM101BaldAPI.OBJImporter;
using MTM101BaldAPI.Reflection;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI.SaveSystem;
using MTM101BaldAPI.UI;
using MTM101BaldAPI.Components.Animation;
using PlusStudioLevelLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using GeneratorHelpers = Baldibasicpoop.Helpers.GeneratorHelpers;


namespace Baldibasicpoop
{
    [BepInPlugin("thumbly.baldiplus.baldisbasicspoop", "Baldi Poop", "1.2.0")]
    
    [BepInDependency("mtm101.rulerp.baldiplus.levelstudio", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("mtm101.rulerp.baldiplus.levelstudioloader", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("mtm101.rulerp.bbplus.baldidevapi")]

    public class BasePlugin : BaseUnityPlugin
    {
        public static BasePlugin Instance { get; internal set; }

        public AssetManager assetMan = new AssetManager();
        public UsefulHelpers usefulHelpers = new UsefulHelpers();
        public GeneratorHelpers generatorHelpers = new GeneratorHelpers();

        public static RoomCategory beanzRoomCat = EnumExtensions.ExtendEnum<RoomCategory>("BeanzRoom");
        public static RoomCategory connorRoomCat = EnumExtensions.ExtendEnum<RoomCategory>("ConnorRoom");

        //public static Sticker StickerEnum = EnumExtensions.ExtendEnum<Sticker>("Sticker");

        private IEnumerator RegisterTemplate()
        {
            yield return 3;
            yield return "BBPoop : Loading Template...";
            try
            {

            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterAssets()
        {
            yield return 3;
            yield return "BBPoop : Loading Assets...";
            try
            {
                // Images //

                assetMan.Add<Sprite>("MainMenuImage", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "PoopMainMenu.png"), 96));

                assetMan.Add<Texture2D>("BeanWall", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanWall.png"));
                assetMan.Add<Texture2D>("BeanCeil", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanCeiling.png"));
                assetMan.Add<Texture2D>("BeanFloor", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanCarpet.png"));
                assetMan.Add<Texture2D>("Connor", AssetLoader.TextureFromMod(this, "Rooms", "NonCanonConnor", "NonCanonConnor.png"));



                assetMan.Add<Texture2D>("CageAtlas", AssetLoader.TextureFromMod(this, "Structures", "Cage", "CageAtlas.png"));
                assetMan.Add<Texture2D>("CageButton_Unpressed", AssetLoader.TextureFromMod(this, "Structures", "Cage", "Indicator_Unpressed.png"));
                assetMan.Add<Texture2D>("CageButton_Unpressed_LG", AssetLoader.TextureFromMod(this, "Structures", "Cage", "Indicator_Unpressed_LightGuide.png"));
                assetMan.Add<Texture2D>("CageButton_Pressed", AssetLoader.TextureFromMod(this, "Structures", "Cage", "Indicator_Pressed.png"));
                assetMan.Add<Texture2D>("CageButton_Pressed_LG", AssetLoader.TextureFromMod(this, "Structures", "Cage", "Indicator_Pressed_LightGuide.png"));
                assetMan.Add<Sprite>("CageSprite", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Structures", "Cage", "CageSprite.png"), new Vector2(0.5f, 0f), 10));
                assetMan.Add<Sprite>("CageGauge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Structures", "Cage", "TrappedGauge.png"), new Vector2(0.5f, 0f), 10));
                assetMan.Add<Sprite>("CageOverlay", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Structures", "Cage", "CageUI.png"), new Vector2(0.5f, 0f), 10));



                assetMan.Add<Sprite>("BAL_Shotgun0", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Baldi", "Shotgun", "BAL_Gun00.png"), 32));
                assetMan.Add<Sprite>("BAL_Shotgun1", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Baldi", "Shotgun", "BAL_Gun01.png"), 32));
                assetMan.Add<Sprite>("BAL_Shotgun2", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Baldi", "Shotgun", "BAL_Gun02.png"), 32));
                assetMan.Add<Sprite>("BAL_Shotgun3", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Baldi", "Shotgun", "BAL_Gun03.png"), 32));
                assetMan.Add<Sprite>("BAL_Shotgun4", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Baldi", "Shotgun", "BAL_Gun04.png"), 32));
                assetMan.Add<Sprite>("BAL_Shotgun5", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Baldi", "Shotgun", "BAL_Gun05.png"), 32));

                assetMan.Add<Sprite>("ShotgunGauge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Events", "Shotgun", "ShotgunGauge.png"), 32));
                assetMan.Add<Sprite>("BloodOverlay", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Events", "Shotgun", "BloodOverlay.png"), 32));

                assetMan.Add<Sprite>("Benz_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "MrBen", "MrBen.png"), new Vector2(0.5f, 0.4f), 32));
                assetMan.Add<Sprite>("Benz_Explod", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "MrBen", "MrBenExplodsisv.png"), new Vector2(0.5f, 0.4f), 32));
                assetMan.Add<Sprite>("Benz_Tedi", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "MrBen", "MrBenTedi.png"), new Vector2(0.5f, 0.4f), 32));

                assetMan.Add<Sprite>("Mii13_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "Mystman13", "Mii13.png"), new Vector2(0.5f, 0.4f), 32));

                assetMan.Add<Sprite>("DYL_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BigDylan", "DYL_Idle.png"), new Vector2(0.5f, 0.4f), 48));
                assetMan.Add<Sprite>("DYL_Stare", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BigDylan", "DYL_Stare.png"), new Vector2(0.5f, 0.4f), 48));
                assetMan.Add<Sprite>("DYL_Chase", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BigDylan", "DYL_Chase.png"), new Vector2(0.5f, 0.4f), 48));
                assetMan.Add<Sprite>("DYL_Dead", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BigDylan", "DYL_Dead.png"), new Vector2(0.5f, 0.4f), 48));

                assetMan.Add<Sprite>("BAGZ_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BaltimoreGomez", "BAGZ_Idle.png"), new Vector2(0.5f, 0.35f), 24));
                assetMan.Add<Sprite>("BAGZ_Slapi", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BaltimoreGomez", "BAGZ_Slapi.png"), new Vector2(0.5f, 0.35f), 24));
                assetMan.Add<Sprite>("BAGZ_Nine", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "BaltimoreGomez", "BAGZ_Nine.png"), new Vector2(0.5f, 0.35f), 24));

                assetMan.Add<Sprite>("NUN_Idle", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "EvilNun", "NUN_Wander.png"), new Vector2(0.5f, 0.5f), 32));
                assetMan.Add<Sprite>("NUN_Chase", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "EvilNun", "NUN_Chasing.png"), new Vector2(0.5f, 0.5f), 32));
                assetMan.Add<Sprite>("NUN_KilledYoFuckingKneecapsLosor", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "NPC", "EvilNun", "NUN_Laugh.png"), new Vector2(0.5f, 0.5f), 32));



                assetMan.Add<Sprite>("PringulsLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Pringuls", "PringulsIcon_Large.png"), 50));
                assetMan.Add<Sprite>("PringulsSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Pringuls", "PringulsIcon_Small.png"), 25));

                assetMan.Add<Sprite>("ShitLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Shit", "ShitIcon_Large.png"), 50));
                assetMan.Add<Sprite>("ShitSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Shit", "ShitIcon_Small.png"), 25));

                assetMan.Add<Sprite>("PhilipLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "SmallPhilip", "PhilipIcon_Large.png"), 50));
                assetMan.Add<Sprite>("PhilipSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "SmallPhilip", "PhilipIcon_Small.png"), 25));

                assetMan.Add<Sprite>("TediLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Tedi", "TediIcon_Large.png"), 50));
                assetMan.Add<Sprite>("TediSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "Tedi", "TediIcon_Small.png"), 25));

                assetMan.Add<Sprite>("AtomLarge", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "AFuckingAtom", "AFuckingAtomIcon_Large.png"), 50));
                assetMan.Add<Sprite>("AtomSmall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Item", "AFuckingAtom", "AFuckingAtomIcon_Small.png"), 25));



                assetMan.Add<Sprite>("BeanPhoneSprite", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Objects", "Billboards", "MrBenPhone.png"), new Vector2(0.5f, 0), 24));
                assetMan.Add<Sprite>("BeanLampSprite", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Objects", "Billboards", "MrBenLamp.png"), new Vector2(0.5f, 0), 24));



                assetMan.Add<Texture2D>("PST_UglyKids", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_UglyKids.png")));
                assetMan.Add<Texture2D>("PST_BaldiSong", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_BaldiSong.png")));
                assetMan.Add<Texture2D>("PST_Chef", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_Chef.png")));
                assetMan.Add<Texture2D>("PST_Depression", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_Depression.png")));
                assetMan.Add<Texture2D>("PST_Wide", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_Wide.png")));
                assetMan.Add<Texture2D>("PST_Strangle", AssetLoader.TextureFromMod(this, Path.Combine("Posters", "PST_Strangle.png")));



                assetMan.Add<Sprite>("Editor_MrBenz", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "npc_benz.png"), 8));
                assetMan.Add<Sprite>("Editor_Mii13", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "npc_mii13.png"), 8));
                assetMan.Add<Sprite>("Editor_Gomez", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "npc_gomez.png"), 8));
                assetMan.Add<Sprite>("Editor_Dylan", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "npc_dylan.png"), 8));

                assetMan.Add<Sprite>("Editor_BeanPhone", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "object_beanphone.png"), 8));
                assetMan.Add<Sprite>("Editor_BeanLamp", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "object_beanlamp.png"), 8));
                assetMan.Add<Sprite>("Editor_ConnorBall", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "object_connorball.png"), 8));

                assetMan.Add<Sprite>("Editor_BeanHouse", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "room_beanhouse.png"), 8));
                assetMan.Add<Sprite>("Editor_Connor", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "room_connor.png"), 8));

                assetMan.Add<Sprite>("Editor_CageTrap", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "structure_cagetrap.png"), 8));

                assetMan.Add<Sprite>("Editor_Shotgun_Icon", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, "Editor", "RandomEvents", "shotgun.png"), 8));

                // SoundObject //
                Color benzColor = new Color(131f / 255f, 75f / 255f, 55f / 255f);
                Color mii13Color = new Color(51f / 255f, 59f / 255f, 67f / 255f);

                assetMan.Add<SoundObject>("SFX_ChipsFall", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("Item", "Pringuls", "SFX_ChipsFall.wav")), "SFX_ChipsFall", SoundType.Effect, Color.white));
                assetMan.Add<SoundObject>("SFX_Die", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("Item", "Shit", "SFX_Die.wav")), "SFX_Die", SoundType.Voice, Color.white));

                assetMan.Add<SoundObject>("BEN_Explod", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Explod.wav")), "BEN_Explod", SoundType.Effect, benzColor));
                assetMan.Add<SoundObject>("BEN_Teddy1", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Teddy1.wav")), "BEN_Teddy", SoundType.Voice, benzColor));
                assetMan.Add<SoundObject>("BEN_Teddy2", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Teddy2.wav")), "BEN_Teddy", SoundType.Voice, benzColor));
                assetMan.Add<SoundObject>("BEN_Teddy3", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Teddy3.wav")), "BEN_Teddy", SoundType.Voice, benzColor));
                assetMan.Add<SoundObject>("BEN_Gibberish1", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Gibberish1.wav")), "BEN_Gibberish", SoundType.Voice, benzColor));
                assetMan.Add<SoundObject>("BEN_Gibberish2", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Gibberish2.wav")), "BEN_Gibberish", SoundType.Voice, benzColor));
                assetMan.Add<SoundObject>("BEN_Gibberish3", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Gibberish3.wav")), "BEN_Gibberish", SoundType.Voice, benzColor));
                assetMan.Add<SoundObject>("BEN_Gibberish4", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "MrBen", "BEN_Gibberish4.wav")), "BEN_Gibberish", SoundType.Voice, benzColor));

                assetMan.Add<SoundObject>("Mii13_Hey", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "Mystman13", "Mii13_Hey.wav")), "Mii13_Hey", SoundType.Voice, mii13Color));

                assetMan.Add<SoundObject>("SFX_Wiplash", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "BigDylan", "SFX_Wiplash.wav")), "SFX_Wiplash", SoundType.Effect, Color.red));
                assetMan.Add<SoundObject>("DYL_Sing", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "BigDylan", "DYL_Sing.wav")), "DYL_Sing", SoundType.Music, Color.red));
                assetMan.Add<SoundObject>("DYL_Death", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "BigDylan", "DYL_Death.wav")), "DYL_Death", SoundType.Effect, Color.red));

                assetMan.Add<SoundObject>("BAL_Reload", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "Baldi", "Shotgun", "BAL_Reload.wav")), "BAL_Reload", SoundType.Voice, Color.green));
                assetMan.Add<SoundObject>("BAL_Shoot", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "Baldi", "Shotgun", "BAL_Shoot.wav")), "Sfx_Bang", SoundType.Voice, Color.green));

                assetMan.Add<SoundObject>("SFX_BoneCrack", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "EvilNun", "SFX_BoneCrack.wav")), "SFX_BoneCrack", SoundType.Effect, Color.white));
                assetMan.Add<SoundObject>("NUN_LaughLosor", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "EvilNun", "NUN_Laugh.wav")), "NUN_Laugh", SoundType.Voice, Color.gray));
                assetMan.Add<SoundObject>("NUN_KillKnees", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "EvilNun", "NUN_Knee.wav")), "NUN_Knee", SoundType.Voice, Color.gray));
                assetMan.Add<SoundObject>("NUN_Chase", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, Path.Combine("NPC", "EvilNun", "NUN_VirtualLoop.wav")), "MUS_NUN_Chase", SoundType.Music, Color.gray));

                assetMan.Add<SoundObject>("BAL_Event_Shotgun", ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(this, "Events", "Shotgun", "BAL_Event_Shotgun.wav"), "BAL_Event_Shotgun_1", SoundType.Voice, Color.green));

                assetMan.Get<SoundObject>("BAL_Event_Shotgun").additionalKeys = new SubtitleTimedKey[]
                {
                new SubtitleTimedKey()
                {
                    encrypted=false,
                    key="BAL_Event_Shotgun_2",
                    time=5.2f
                }
                };
            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterObjects()
        {
            yield return 3;
            yield return "BBPoop : Loading Objects...";
            try
            {
                GameObject PlantPrefab = Resources.FindObjectsOfTypeAll<EnvironmentObject>().First(x => x.name == "Plant" && x.GetInstanceID() >= 0 && x.transform.parent == null).gameObject;

                GameObject BeanzPhoneBase = GameObject.Instantiate<GameObject>(PlantPrefab, MTM101BaldiDevAPI.prefabTransform);
                BeanzPhoneBase.GetComponentInChildren<SpriteRenderer>().sprite = assetMan.Get<Sprite>("BeanPhoneSprite");
                BeanzPhoneBase.transform.position = new Vector3(0, 4f, 0);
                BeanzPhoneBase.name = "BeanzPhone";
                assetMan.Add<GameObject>("BeanzPhone", BeanzPhoneBase);
                LevelLoaderPlugin.Instance.basicObjects.Add("beanzphone", assetMan.Get<GameObject>("BeanzPhone"));

                GameObject BeanzLampBase = GameObject.Instantiate<GameObject>(PlantPrefab, MTM101BaldiDevAPI.prefabTransform);
                BeanzLampBase.GetComponentInChildren<SpriteRenderer>().sprite = assetMan.Get<Sprite>("BeanLampSprite");
                BeanzLampBase.transform.position = new Vector3(0, 4f, 0);
                BeanzLampBase.name = "BeanzLamp";
                assetMan.Add<GameObject>("BeanzLamp", BeanzLampBase);
                LevelLoaderPlugin.Instance.basicObjects.Add("beanzlamp", assetMan.Get<GameObject>("BeanzLamp"));

                GameObject ConnorBall = new GameObject("ConnorBall");
                ConnorBall.transform.SetParent(MTM101BaldiDevAPI.prefabTransform);
                GameObject ConnorBallMesh = new GameObject("ConnorBallMesh");
                ConnorBallMesh.transform.SetParent(ConnorBall.transform);
                ConnorBallMesh.AddComponent<MeshFilter>().mesh = Resources.FindObjectsOfTypeAll<Mesh>().First(x => x.name == "Sphere" && x.GetInstanceID() >= 0);
                MeshRenderer Connorrenderer = ConnorBallMesh.AddComponent<MeshRenderer>();
                Connorrenderer.material = usefulHelpers.MAT_CreateMaterial(assetMan, "Connor");
                ConnorBallMesh.transform.localScale = Vector3.one * 8;
                ConnorBallMesh.transform.position = new Vector3(0, 5, 0);
                ConnorBallMesh.AddComponent<SphereCollider>().radius = 0.8f;
                assetMan.Add<GameObject>("ConnorBall", ConnorBall);
                LevelLoaderPlugin.Instance.basicObjects.Add("connorball", assetMan.Get<GameObject>("ConnorBall"));
            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterStructures()
        {
            yield return 3;
            yield return "BBPoop : Loading Structures...";
            try
            {
                GameButton Button = Resources.FindObjectsOfTypeAll<GameButton>().First((GameButton x) => x.GetInstanceID() > 0);
                LockdownDoor lockdoor = Resources.FindObjectsOfTypeAll<LockdownDoor>().First((LockdownDoor x) => x.GetInstanceID() > 0);
                GameObject PlantPrefab = Resources.FindObjectsOfTypeAll<EnvironmentObject>().First(x => x.name == "Plant" && x.GetInstanceID() >= 0 && x.transform.parent == null).gameObject;

                Material IndicatorPressed = usefulHelpers.MAT_CreateMaterial(assetMan, "CageButton_Pressed", "CageButton_Pressed_LG");
                Material IndicatorUnpressed = usefulHelpers.MAT_CreateMaterial(assetMan, "CageButton_Unpressed", "CageButton_Unpressed_LG");

                Material CageMaterial = usefulHelpers.MAT_CreateMaterial(assetMan, "CageAtlas");

                GameObject CageTrapBase = new GameObject("CageTrap");
                CageTrapBase.transform.SetParent(MTM101BaldiDevAPI.prefabTransform);
                GameObject IndicatorMesh = new GameObject("CageIndicatorMesh");
                IndicatorMesh.transform.SetParent(CageTrapBase.transform);
                IndicatorMesh.AddComponent<MeshFilter>().mesh = Resources.FindObjectsOfTypeAll<Mesh>().First(x => x.name == "Quad" && x.GetInstanceID() >= 0);
                IndicatorMesh.transform.localScale = Vector3.one * 9.99f;
                IndicatorMesh.transform.position = new Vector3(0, 0.01f, 0);
                IndicatorMesh.transform.rotation = Quaternion.Euler(90, 0, 0);
                MeshRenderer Cagerenderer = IndicatorMesh.AddComponent<MeshRenderer>();
                Cagerenderer.material = IndicatorUnpressed;
                /* GameObject CageGraphic = Instantiate<GameObject>(PlantPrefab, CageTrapBase.transform);
                CageGraphic.GetComponentInChildren<SpriteRenderer>().sprite = assetMan.Get<Sprite>("CageSprite");
                CageGraphic.transform.SetParent(CageTrapBase.transform);
                CageGraphic.transform.position = new Vector3(0, 9, 0);
                CageGraphic.name = "CageSprite"; */ // This is for the old cage sprite

                GameObject CageGraphic = Instantiate<GameObject>(AssetLoader.ModelFromModManualMaterials(this, new Dictionary<string, Material>()
                {
                    { "CageMat", CageMaterial }
                }, "Structures", "Cage", "CageModel.obj"), CageTrapBase.transform);
                CageGraphic.transform.localScale = Vector3.one * 10;
                CageGraphic.transform.position = Vector3.up * 9;
                BoxCollider boxColl = CageGraphic.AddComponent<BoxCollider>();
                boxColl.size = new Vector3(1, 0.5f, 1);
                boxColl.center = Vector3.up * 0.5f;
                NavMeshObstacle navObst = CageGraphic.AddComponent<NavMeshObstacle>();
                navObst.size = boxColl.size;
                navObst.center = boxColl.center;

                CageTrap cageTrap = CageTrapBase.AddComponent<CageTrap>();
                cageTrap.audMan = CageTrapBase.AddComponent<PropagatedAudioManager>();
                cageTrap.CageGraphic = CageGraphic;
                cageTrap.audUnActivate = (SoundObject)Button.ReflectionGetVariable("audPress");
                cageTrap.audActivate = (SoundObject)Button.ReflectionGetVariable("audUnpress");
                cageTrap.audTrigger = (SoundObject)lockdoor.ReflectionGetVariable("doorEnd");
                cageTrap.buttonMesh = Cagerenderer;
                cageTrap.pressedMat = IndicatorPressed;
                cageTrap.unpressedMat = IndicatorUnpressed;
                cageTrap.GaugeIcon = assetMan.Get<Sprite>("CageGauge");
                cageTrap.TrapCover = assetMan.Get<Sprite>("CageOverlay");
                cageTrap.SetPower(true);

                CapsuleCollider scannerCollider = CageTrapBase.AddComponent<CapsuleCollider>();
                scannerCollider.radius = 2f;
                scannerCollider.height = 10f;
                scannerCollider.center = new Vector3(0, 5f, 0);
                scannerCollider.isTrigger = true;

                GameObject cageTrapBuilderObject = new GameObject("CageTrapBuilder");
                cageTrapBuilderObject.ConvertToPrefab(true);
                Structure_CageTrap cageTrapBuilder = cageTrapBuilderObject.AddComponent<Structure_CageTrap>();
                cageTrapBuilder.prefab = cageTrap;
                assetMan.Add<Structure_CageTrap>("CageTrap", cageTrapBuilder);
            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterRooms()
        {
            yield return 3;
            yield return "BBPoop : Loading Rooms...";
            try
            {
                Transform fluorescent_light = LevelLoaderPlugin.Instance.lightTransforms["fluorescent"];
                Transform standardhanging_light = LevelLoaderPlugin.Instance.lightTransforms["standardhanging"];
                Transform null_light = LevelLoaderPlugin.Instance.lightTransforms["null"];

                WeightedTransform fluorescent_weighted = new WeightedTransform()
                {
                    selection = fluorescent_light,
                    weight = 100
                };
                WeightedTransform standardhanging_weighted = new WeightedTransform()
                {
                    selection = standardhanging_light,
                    weight = 100
                };
                WeightedTransform null_weighted = new WeightedTransform()
                {
                    selection = null_light,
                    weight = 100
                };

                assetMan.Add<StandardDoorMats>("BeanzDoorMats", ObjectCreators.CreateDoorDataObject("BeanDoor", AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanDoor_Open.png"), AssetLoader.TextureFromMod(this, "Rooms", "BeanzHouse", "BeanDoor_Closed.png")));
                LevelLoaderPlugin.Instance.roomSettings.Add("beanHouse", new RoomSettings(BasePlugin.beanzRoomCat, RoomType.Room, new Color(131f / 255f, 75f / 255f, 55f / 255f), assetMan.Get<StandardDoorMats>("BeanzDoorMats"), null));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzFloor", assetMan.Get<Texture2D>("BeanFloor"));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzWall", assetMan.Get<Texture2D>("BeanWall"));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("BeanzCeil", assetMan.Get<Texture2D>("BeanCeil"));

                string beanRoomPath = Path.Combine(AssetLoader.GetModPath(this), "Rooms", "BeanzHouse", "beanhouse.rbpl");
                ItemObject[] beanItems = new ItemObject[] { assetMan.Get<ItemObject>("Tedi") };

                WeightedRoomAsset beanRoom = usefulHelpers.ROOM_ReadRoomFile(beanRoomPath, standardhanging_light, beanItems);
                assetMan.Add<WeightedRoomAsset>("BeanRoomAsset", beanRoom);


                WeightedTexture2D beanzFloor = new WeightedTexture2D()
                {
                    selection = assetMan.Get<Texture2D>("BeanFloor"),
                    weight = 100
                };

                WeightedTexture2D beanzWall = new WeightedTexture2D()
                {
                    selection = assetMan.Get<Texture2D>("BeanWall"),
                    weight = 100
                };

                WeightedTexture2D beanzCeiling = new WeightedTexture2D()
                {
                    selection = assetMan.Get<Texture2D>("BeanCeil"),
                    weight = 100
                };

                RoomGroup beanRoomGroup = usefulHelpers.ROOM_CreateRoomGroup(
                    "Beanz House",
                    1,
                    new IntVector2(0, 0),
                    beanRoom,
                    new WeightedTransform[] { standardhanging_weighted },
                    new WeightedTexture2D[] { beanzFloor },
                    new WeightedTexture2D[] { beanzWall },
                    new WeightedTexture2D[] { beanzCeiling }
                    );

                assetMan.Add<StandardDoorMats>("ConnorDoorMats", ObjectCreators.CreateDoorDataObject("ConnorDoor", AssetLoader.TextureFromMod(this, "Rooms", "NonCanonConnor", "Connor_Open.png"), AssetLoader.TextureFromMod(this, "Rooms", "NonCanonConnor", "Connor_Closed.png")));
                LevelLoaderPlugin.Instance.roomSettings.Add("connorRoom", new RoomSettings(BasePlugin.connorRoomCat, RoomType.Room, new Color(101f / 255f, 2f / 255f, 0f), assetMan.Get<StandardDoorMats>("ConnorDoorMats"), null));
                LevelLoaderPlugin.Instance.roomTextureAliases.Add("ConnorTexture", assetMan.Get<Texture2D>("Connor"));

                WeightedTexture2D connorTexture = new WeightedTexture2D()
{
                    selection = assetMan.Get<Texture2D>("Connor"),
                    weight = 100
                };

                string connorRoomPath = Path.Combine(AssetLoader.GetModPath(this), "Rooms", "NonCanonConnor", "connorlair.rbpl");

                WeightedRoomAsset connorRoom = usefulHelpers.ROOM_ReadRoomFile(connorRoomPath, null_light, 0, 0);

                RoomGroup connorRoomGroup = usefulHelpers.ROOM_CreateRoomGroup(
                    "Connor Lair",
                    1,
                    new IntVector2(1, 1),
                    connorRoom,
                    new WeightedTransform[] {null_weighted},
                    new WeightedTexture2D[] {connorTexture});
                assetMan.Add<RoomGroup>("ConnorRoomGroup", connorRoomGroup);

            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterNPCS()
        {
            yield return 3;
            yield return "BBPoop : Loading NPC's...";
            try
            {
                LookAtGuy lookAtGuy = Resources.FindObjectsOfTypeAll<LookAtGuy>().First((LookAtGuy x) => x.GetInstanceID() > 0);

                MisterBenz benz = new NPCBuilder<MisterBenz>(base.Info)
                    .SetName("Mister Benz")
                    .AddTrigger()
                    .SetEnum("MrBenz")
                    .SetMinMaxAudioDistance(10f, 150f)
                    .AddSpawnableRoomCategories(beanzRoomCat)
                    .AddPotentialRoomAssets(new WeightedRoomAsset[] {assetMan.Get<WeightedRoomAsset>("BeanRoomAsset") })
                    .SetWanderEnterRooms()
                    .IgnorePlayerOnSpawn()
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/MrBen/PRI_Benz.png"), "PRI_Beanz1", "PRI_Beanz2")
                    .Build();
                benz.explosionPrefab = (QuickExplosion)lookAtGuy.ReflectionGetVariable("explosionPrefab");
                benz.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Idle");
                assetMan.Add<NPC>("MrBenz", benz);


                Mystman13 mii13 = new NPCBuilder<Mystman13>(base.Info)
                    .SetName("Mystman13")
                    .AddTrigger()
                    .SetEnum("Mii13")
                    .AddLooker()
                    .SetMinMaxAudioDistance(10f, 150f)
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/Mystman13/PRI_Mii13.png"), "PRI_Mii131", "PRI_Mii132")
                    .Build();
                mii13.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Mii13_Idle");
                assetMan.Add<NPC>("Mii13", mii13);


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


                BaltimoreGomez baltimore = new NPCBuilder<BaltimoreGomez>(base.Info)
                    .SetName("Baltimore Gomez")
                    .AddTrigger()
                    .SetEnum("B_Gomez")
                    .AddLooker()
                    .SetMinMaxAudioDistance(10f, 120f)
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/BaltimoreGomez/PRI_Gomez.png"), "PRI_Gomez1", "PRI_Gomez2")
                    .Build();
                baltimore.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAGZ_Idle");
                assetMan.Add<NPC>("Baltimore", baltimore);

                IvolNun nun = new NPCBuilder<IvolNun>(base.Info)
                    .SetName("Ivol Nun")
                    .AddTrigger()
                    .SetEnum("IVOLNUN")
                    .AddLooker()
                    .SetMinMaxAudioDistance(30f, 500f)
                    .SetPoster(AssetLoader.TextureFromMod(this, "NPC/EvilNun/PRI_MotherfuckingNun.png"), "PRI_Nun1", "PRI_Nun2")
                    .Build();
                nun.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("NUN_Idle");
                assetMan.Add<NPC>("EvilNun", nun);
            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterItems()
        {
            yield return 3;
            yield return "BBPoop : Loading Items...";
            try
            {
                LookAtGuy lookAtGuy = Resources.FindObjectsOfTypeAll<LookAtGuy>().First((LookAtGuy x) => x.GetInstanceID() > 0);

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
                    .SetMeta(ItemFlags.Persists, new string[] { "food", "trash" })
                    .Build();
                ((ITM_Shit)shit.item).eatSound = (SoundObject)((ITM_ZestyBar)ItemMetaStorage.Instance.FindByEnum(Items.ZestyBar).value.item).ReflectionGetVariable("audEat");
                ((ITM_Shit)shit.item).dieSound = assetMan.Get<SoundObject>("SFX_Die");
                assetMan.Add<ItemObject>("Shit", shit);

                ItemObject philip = new ItemBuilder(Info)
                    .SetNameAndDescription("Itm_Philip", "Desc_Philip")
                    .SetSprites(assetMan.Get<Sprite>("PhilipSmall"), assetMan.Get<Sprite>("PhilipLarge"))
                    .SetEnum("SmallPhilip")
                    .SetShopPrice(160)
                    .SetGeneratorCost(79)
                    .SetItemComponent<ITM_SmallPhilip>()
                    .SetMeta(ItemFlags.Persists, new string[] { "tool" })
                    .Build();
                assetMan.Add<ItemObject>("SmallPhilip", philip);

                ItemObject tedi = new ItemBuilder(Info)
                    .SetNameAndDescription("Itm_Tedi", "Desc_Tedi")
                    .SetSprites(assetMan.Get<Sprite>("TediSmall"), assetMan.Get<Sprite>("TediLarge"))
                    .SetEnum("Tedi")
                    .SetShopPrice(10000)
                    .SetGeneratorCost(0)
                    .SetItemComponent<ITM_Tedi>()
                    .SetMeta(ItemFlags.None, new string[] { "plush" })
                    .Build();

                assetMan.Add<ItemObject>("Tedi", tedi);

                ItemObject atom = new ItemBuilder(Info)
                    .SetNameAndDescription("Itm_Atom", "Desc_Atom")
                    .SetSprites(assetMan.Get<Sprite>("AtomSmall"), assetMan.Get<Sprite>("AtomLarge"))
                    .SetEnum("AFakingAtom")
                    .SetShopPrice(2099)
                    .SetGeneratorCost(0)
                    .SetItemComponent<ITM_Atom>()
                    .SetMeta(ItemFlags.InstantUse, new string[] { "item" })
                    .SetAsInstantUse()
                    .Build();
                ((ITM_Atom)atom.item).explosion = (QuickExplosion)lookAtGuy.ReflectionGetVariable("explosionPrefab");
                assetMan.Add<ItemObject>("AFuckingAtom", atom);
            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterPosters()
        {
            yield return 3;
            yield return "BBPoop : Loading Posters...";
            try
            {
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

                PosterObject PST_Strangle = ObjectCreators.CreatePosterObject(assetMan.Get<Texture2D>("PST_Strangle"), TextNone);
                assetMan.Add<PosterObject>("PST_Strangle", PST_Strangle);
            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterStickers()
        {
            yield return 3;
            yield return "BBPoop : Loading Stickers...";
            try
            {

            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator RegisterEvents()
        {
            yield return 3;
            yield return "BBPoop : Loading Events...";
            try
            {

                ShotgunEvent shotgunEvent = new RandomEventBuilder<ShotgunEvent>(Info)
                    .SetEnum("Shotgun")
                    .SetMinMaxTime(90f, 120f)
                    .SetName("Baldi_Shotgun")
                    .SetSound(assetMan.Get<SoundObject>("BAL_Event_Shotgun"))
                    .Build();
                assetMan.Add<ShotgunEvent>("shotgunEvent", shotgunEvent);

            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        private IEnumerator LoadEditorStuff()
        {
            yield return 3;
            yield return "BBPoop : Loading Level Loader/Editor...";
            try
            {
                LoaderSupport.AddLoaderContent();
                if (Chainloader.PluginInfos.ContainsKey("mtm101.rulerp.baldiplus.levelstudio"))
                {
                    EditorSupport.AddEditorContent();
                }
            }

            catch (Exception x)
            {
                Debug.LogError(x.Message);
            }
            yield break;
        }

        void GeneratorModifications(string levelName, int levelId, SceneObject scene)
        {
            CustomLevelObject[] objects = scene.GetCustomLevelObjects();
            scene.MarkAsNeverUnload();
            bool isEndless = scene.GetMeta().tags.Contains("endless");



            NPC benz = assetMan.Get<NPC>("MrBenz");
            NPC mii13 = assetMan.Get<NPC>("Mii13");
            NPC dylan = assetMan.Get<NPC>("BigDylan");
            NPC baltimore = assetMan.Get<NPC>("Baltimore");
            NPC nun = assetMan.Get<NPC>("EvilNun");

            ItemObject pringuls = assetMan.Get<ItemObject>("Pringuls");
            ItemObject shit = assetMan.Get<ItemObject>("Shit");
            ItemObject philip = assetMan.Get<ItemObject>("SmallPhilip");
            ItemObject atom = assetMan.Get<ItemObject>("AFuckingAtom");

            PosterObject PST_UglyKids = assetMan.Get<PosterObject>("PST_UglyKids");
            PosterObject PST_BaldiSong = assetMan.Get<PosterObject>("PST_BaldiSong");
            PosterObject PST_Depression = assetMan.Get<PosterObject>("PST_Depression");
            PosterObject PST_Chef = assetMan.Get<PosterObject>("PST_Chef");
            PosterObject PST_Wide = assetMan.Get<PosterObject>("PST_Wide");
            PosterObject PST_Strangle = assetMan.Get<PosterObject>("PST_Strangle");

            RoomGroup connorGroup = assetMan.Get<RoomGroup>("ConnorRoomGroup");

            ShotgunEvent shotgunEvent = assetMan.Get<ShotgunEvent>("shotgunEvent");

            Structure_CageTrap cagetrap = assetMan.Get<Structure_CageTrap>("CageTrap");

            switch (levelName)
            {
                case "F1":
                    generatorHelpers.AddItemToShop(scene, atom, 100);
                    generatorHelpers.AddNPCToLevel(scene, assetMan.Get<NPC>("MrBenz"));
                    break;
                case "F2":
                    generatorHelpers.AddItemToShop(scene, pringuls, 79);
                    generatorHelpers.AddNPCToLevel(scene, mii13, 80);
                    generatorHelpers.AddNPCToLevel(scene, nun, 25);
                    generatorHelpers.AddItemToShop(scene, atom, 25);
                    break;
                case "F3":
                    generatorHelpers.AddNPCToLevel(scene, dylan, 45);
                    generatorHelpers.AddNPCToLevel(scene, baltimore);
                    generatorHelpers.AddNPCToLevel(scene, nun, 50);
                    generatorHelpers.AddItemToShop(scene, philip, 68);
                    generatorHelpers.AddItemToShop(scene, shit, 15);
                    generatorHelpers.AddItemToShop(scene, atom, 50);
                    break;
            }



            if (isEndless)
            {
                generatorHelpers.AddNPCToLevel(scene, mii13, 45);
                generatorHelpers.AddNPCToLevel(scene, dylan, 25);
                generatorHelpers.AddNPCToLevel(scene, benz);

                generatorHelpers.AddItemToShop(scene, shit, 5);
                generatorHelpers.AddItemToShop(scene, philip, 10);
                generatorHelpers.AddItemToShop(scene, pringuls, 45);
                generatorHelpers.AddItemToShop(scene, atom, 100);
            }

            for (int i = 0; i < objects.Length; i++)
            {
                CustomLevelObject obj = objects[i];
                if (obj.IsModifiedByMod(Info)) continue;

                switch (levelName)
                {
                    case "F1":
                        generatorHelpers.AddPosterToLevel(obj, PST_BaldiSong, 35);
                        generatorHelpers.AddPosterToLevel(obj, PST_Strangle, 10);

                        generatorHelpers.AddItemToLevel(obj, pringuls, 25);
                        generatorHelpers.AddItemToLevel(obj, atom, 38);
                        generatorHelpers.AddItemToLevel(obj, shit, 1);
                        obj.MarkAsModifiedByMod(Info);
                        break;
                    case "F2":
                        generatorHelpers.AddRoomGroupToLevel(obj, connorGroup);

                        generatorHelpers.AddPosterToLevel(obj, PST_UglyKids, 25);
                        generatorHelpers.AddPosterToLevel(obj, PST_Chef, 20);
                        generatorHelpers.AddPosterToLevel(obj, PST_Wide, 47);
                        generatorHelpers.AddPosterToLevel(obj, PST_Wide, 47);

                        generatorHelpers.AddItemToLevel(obj, atom, 93);
                        generatorHelpers.AddItemToLevel(obj, pringuls, 46);
                        generatorHelpers.AddItemToLevel(obj, philip, 32);

                        if (obj.type == LevelType.Schoolhouse)
                        {
                            IntVector2[] minmax = new IntVector2[]
                            {
                            new IntVector2(1, 2),
                            new IntVector2(4, 8)
                            };
                            generatorHelpers.AddStructureWithParametersToLevel(obj, cagetrap, minmax);
                        }
                        obj.MarkAsModifiedByMod(Info);
                        break;
                    case "F3":
                        generatorHelpers.AddEventToLevel(obj, shotgunEvent, 50);
                        generatorHelpers.AddPosterToLevel(obj, PST_BaldiSong, 74);
                        generatorHelpers.AddPosterToLevel(obj, PST_UglyKids, 25);
                        generatorHelpers.AddPosterToLevel(obj, PST_Wide, 34);
                        generatorHelpers.AddPosterToLevel(obj, PST_Depression, 10);

                        generatorHelpers.AddItemToLevel(obj, pringuls, 69);
                        generatorHelpers.AddItemToLevel(obj, philip, 15);
                        generatorHelpers.AddItemToLevel(obj, atom, 193);

                        if (obj.type == LevelType.Schoolhouse)
                        {
                            IntVector2[] minmax = new IntVector2[]
                            {
                            new IntVector2(3, 5),
                            new IntVector2(7, 9)
                            };
                            generatorHelpers.AddStructureWithParametersToLevel(obj, cagetrap, minmax);
                        }
                        obj.MarkAsModifiedByMod(Info);
                        break;
                    case "F4":
                        generatorHelpers.AddItemToLevel(obj, atom, 293);
                        generatorHelpers.AddEventToLevel(obj, shotgunEvent, 75);
                        generatorHelpers.AddItemToLevel(obj, philip, 68);
                        generatorHelpers.AddItemToLevel(obj, pringuls, 80);
                        obj.MarkAsModifiedByMod(Info);
                        break;
                    case "F5":
                        generatorHelpers.AddItemToLevel(obj, atom, 593);
                        generatorHelpers.AddEventToLevel(obj, shotgunEvent, 100);
                        generatorHelpers.AddItemToLevel(obj, philip, 68);
                        generatorHelpers.AddItemToLevel(obj, pringuls, 80);
                        obj.MarkAsModifiedByMod(Info);
                        break;
                    default:
                        return;
                }
                if (isEndless)
                {
                    generatorHelpers.AddRoomGroupToLevel(obj, connorGroup);

                    generatorHelpers.AddItemToLevel(obj, atom, 10);
                    generatorHelpers.AddItemToLevel(obj, pringuls, 65);
                    generatorHelpers.AddItemToLevel(obj, philip, 80);
                    generatorHelpers.AddItemToLevel(obj, shit, 5);
                    obj.MarkAsModifiedByMod(Info);
                }
                objects[i].MarkAsNeverUnload();
            }
        }

        void Awake()
        {
            Instance = this;
            Harmony harmony = new Harmony("thumbly.baldiplus.baldisbasicspoop");

            ModdedSaveGame.AddSaveHandler(Info);

            harmony.PatchAllConditionals();

            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterAssets(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterObjects(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterStructures(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterItems(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterRooms(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterNPCS(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterPosters(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, RegisterEvents(), LoadingEventOrder.Pre);
            LoadingEvents.RegisterOnAssetsLoaded(base.Info, LoadEditorStuff(), LoadingEventOrder.Pre);
            AssetLoader.LocalizationFromMod(this);
            GeneratorManagement.Register(this, GenerationModType.Addend, GeneratorModifications);
        }
    }
}
