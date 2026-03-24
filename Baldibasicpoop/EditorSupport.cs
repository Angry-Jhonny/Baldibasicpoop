using System;
using System.Collections.Generic;
using System.Text;
using PlusStudioLevelLoader;
using PlusLevelStudio;
using UnityEngine;
using MTM101BaldAPI.AssetTools;
using PlusLevelStudio.Editor;
using PlusStudioLevelFormat;
using PlusLevelStudio.Editor.Tools;
using MTM101BaldAPI;

using Baldibasicpoop.CustomItems;
using Baldibasicpoop.Structures;
using Baldibasicpoop.Events;

namespace Baldibasicpoop.Editor
{
    public static class EditorSupport
    {
        public static void AddEditorContent()
        {
            AssetManager assetMan = BasePlugin.Instance.assetMan;
            EditorInterface.AddNPCVisual("misterbenz", assetMan.Get<NPC>("MrBenz"));
            EditorInterface.AddNPCVisual("mii13", assetMan.Get<NPC>("Mii13"));
            EditorInterface.AddNPCVisual("bigdylan", assetMan.Get<NPC>("BigDylan"));
            EditorInterface.AddNPCVisual("gomez", assetMan.Get<NPC>("Baltimore"));
            EditorInterface.AddNPCVisual("nun", assetMan.Get<NPC>("EvilNun"));

            LevelStudioPlugin.Instance.structureTypes.Add("cagetrap", typeof(HallDoorStructureLocation));

            LevelStudioPlugin.Instance.selectableTextures.Add("BeanzFloor");
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanzWall");
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanzCeil");
            LevelStudioPlugin.Instance.selectableTextures.Add("ConnorTexture");

            LevelStudioPlugin.Instance.selectableShopItems.Add("pringuls");
            LevelStudioPlugin.Instance.selectableShopItems.Add("shit");
            LevelStudioPlugin.Instance.selectableShopItems.Add("smallphilip");
            LevelStudioPlugin.Instance.selectableShopItems.Add("tedi");
            LevelStudioPlugin.Instance.selectableShopItems.Add("atom");

            EditorInterface.AddStructureGenericVisual("cagetrap", assetMan.Get<Structure_CageTrap>("CageTrap").prefab.gameObject);

            EditorInterface.AddObjectVisualWithCustomCapsuleCollider("beanzphone", LevelLoaderPlugin.Instance.basicObjects["beanzphone"], 1, 1, 1, new Vector3(0,0.5f,0));
            EditorInterface.AddObjectVisualWithCustomCapsuleCollider("beanzlamp", LevelLoaderPlugin.Instance.basicObjects["beanzlamp"], 1, 2, 1, new Vector3(0, 1, 0));
            EditorInterface.AddObjectVisualWithCustomCapsuleCollider("connorball", LevelLoaderPlugin.Instance.basicObjects["connorball"], 4, 4, 1, new Vector3(0, 5, 0));

            EditorInterfaceModes.AddModeCallback(AddContentToMode);
            LevelStudioPlugin.Instance.defaultRoomTextures.Add("beanHouse", new TextureContainer("BeanzFloor", "BeanzWall", "BeanzCeil"));
            LevelStudioPlugin.Instance.defaultRoomTextures.Add("connorRoom", new TextureContainer("ConnorTexture", "ConnorTexture", "ConnorTexture"));

            LevelStudioPlugin.Instance.eventSprites.Add("shotgun", assetMan.Get<Sprite>("Editor_Shotgun_Icon"));
        }

        public static void AddContentToMode(EditorMode mode, bool vanillaCompliant)
        {
            AssetManager assetMan = BasePlugin.Instance.assetMan;

            EditorInterfaceModes.AddToolsToCategory(mode, "rooms", new RoomTool[]
            {
                new RoomTool("beanHouse", assetMan.Get<Sprite>("Editor_BeanHouse")),
                new RoomTool("connorRoom", assetMan.Get<Sprite>("Editor_Connor"))
            });

            EditorInterfaceModes.AddToolsToCategory(mode, "posters", new PosterTool[]
            {
                new PosterTool("pri_benz"),
                new PosterTool("pri_mii13"),
                new PosterTool("pri_dylan"),
                new PosterTool("pri_gomez"),
                new PosterTool("pri_nun"),

                new PosterTool("pst_uglykids"),
                new PosterTool("pst_baldisong"),
                new PosterTool("pst_depression"),
                new PosterTool("pst_chef"),
                new PosterTool("pst_wide"),
                new PosterTool("pst_strangle")
            });

            EditorInterfaceModes.AddToolsToCategory(mode, "npcs", new NPCTool[]
            {
                new NPCTool("misterbenz", assetMan.Get<Sprite>("Editor_MrBenz")),
                new NPCTool("mii13", assetMan.Get<Sprite>("Editor_Mii13")),
                new NPCTool("bigdylan", assetMan.Get<Sprite>("Editor_Dylan")),
                new NPCTool("gomez", assetMan.Get<Sprite>("Editor_Gomez")),
                new NPCTool("nun", assetMan.Get<Sprite>("Editor_EvilNun"))
            });

            EditorInterfaceModes.AddToolsToCategory(mode, "objects", new EditorTool[]
            {
                new ObjectToolNoRotation("beanzphone", assetMan.Get<Sprite>("Editor_BeanPhone")),
                new ObjectToolNoRotation("beanzlamp", assetMan.Get<Sprite>("Editor_BeanLamp")),
                new ObjectToolNoRotation("connorball", assetMan.Get<Sprite>("Editor_ConnorBall"))
            });

            EditorInterfaceModes.AddToolsToCategory(mode, "items", new ItemTool[]
            {
                new ItemTool("pringuls"),
                new ItemTool("shit"),
                new ItemTool("smallphilip"),
                new ItemTool("atom"),
                new ItemTool("tedi")
            });

            EditorInterfaceModes.AddToolsToCategory(mode, "structures", new EditorTool[]
            {
                new HallDoorStructureTool("cagetrap", assetMan.Get<Sprite>("Editor_CageTrap")),
            });

            mode.availableRandomEvents.Add("shotgun");
        }
    }

    public static class LoaderSupport
    {
        public static void AddLoaderContent()
        {
            AssetManager assetMan = BasePlugin.Instance.assetMan;

            LevelLoaderPlugin.Instance.npcAliases.Add("misterbenz", assetMan.Get<NPC>("MrBenz"));
            LevelLoaderPlugin.Instance.npcAliases.Add("mii13", assetMan.Get<NPC>("Mii13"));
            LevelLoaderPlugin.Instance.npcAliases.Add("bigdylan", assetMan.Get<NPC>("BigDylan"));
            LevelLoaderPlugin.Instance.npcAliases.Add("gomez", assetMan.Get<NPC>("Baltimore"));
            LevelLoaderPlugin.Instance.npcAliases.Add("nun", assetMan.Get<NPC>("EvilNun"));

            LevelLoaderPlugin.Instance.itemObjects.Add("pringuls", assetMan.Get<ItemObject>("Pringuls"));
            LevelLoaderPlugin.Instance.itemObjects.Add("shit", assetMan.Get<ItemObject>("Shit"));
            LevelLoaderPlugin.Instance.itemObjects.Add("smallphilip", assetMan.Get<ItemObject>("SmallPhilip"));
            LevelLoaderPlugin.Instance.itemObjects.Add("tedi", assetMan.Get<ItemObject>("Tedi"));
            LevelLoaderPlugin.Instance.itemObjects.Add("atom", assetMan.Get<ItemObject>("AFuckingAtom"));

            LevelLoaderPlugin.Instance.posterAliases.Add("pri_benz", assetMan.Get<NPC>("MrBenz").Poster);
            LevelLoaderPlugin.Instance.posterAliases.Add("pri_mii13", assetMan.Get<NPC>("Mii13").Poster);
            LevelLoaderPlugin.Instance.posterAliases.Add("pri_dylan", assetMan.Get<NPC>("BigDylan").Poster);
            LevelLoaderPlugin.Instance.posterAliases.Add("pri_gomez", assetMan.Get<NPC>("Baltimore").Poster);
            LevelLoaderPlugin.Instance.posterAliases.Add("pri_nun", assetMan.Get<NPC>("EvilNun").Poster);

            LevelLoaderPlugin.Instance.posterAliases.Add("pst_uglykids", assetMan.Get<PosterObject>("PST_UglyKids"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_baldisong", assetMan.Get<PosterObject>("PST_BaldiSong"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_depression", assetMan.Get<PosterObject>("PST_Depression"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_chef", assetMan.Get<PosterObject>("PST_Chef"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_wide", assetMan.Get<PosterObject>("PST_Wide"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_strangle", assetMan.Get<PosterObject>("PST_Strangle"));

            LevelLoaderPlugin.Instance.randomEventAliases.Add("shotgun", assetMan.Get<RandomEvent>("shotgunEvent"));

            LevelLoaderPlugin.Instance.structureAliases.Add("cagetrap", new LoaderStructureData(assetMan.Get<Structure_CageTrap>("CageTrap"), new Dictionary<string, GameObject>()));
        }
    }
}