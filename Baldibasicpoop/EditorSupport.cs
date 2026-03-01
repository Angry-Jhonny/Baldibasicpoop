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
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanzFloor");
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanzWall");
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanzCeil");
            LevelStudioPlugin.Instance.selectableTextures.Add("ConnorTexture");
            LevelStudioPlugin.Instance.selectableShopItems.Add("pringuls");
            LevelStudioPlugin.Instance.selectableShopItems.Add("shit");
            EditorInterface.AddObjectVisualWithCustomCapsuleCollider("beanzphone", LevelLoaderPlugin.Instance.basicObjects["beanzphone"], 1, 1, 1, new Vector3(0,0.5f,0));
            EditorInterface.AddObjectVisualWithCustomCapsuleCollider("beanzlamp", LevelLoaderPlugin.Instance.basicObjects["beanzlamp"], 1, 2, 1, new Vector3(0, 1, 0));
            EditorInterface.AddObjectVisualWithCustomCapsuleCollider("connorball", LevelLoaderPlugin.Instance.basicObjects["connorball"], 4, 4, 1, new Vector3(0, 5, 0));

            EditorInterfaceModes.AddModeCallback(AddContentToMode);
            LevelStudioPlugin.Instance.defaultRoomTextures.Add("beanHouse", new TextureContainer("BeanzFloor", "BeanzWall", "BeanzCeil"));
            LevelStudioPlugin.Instance.defaultRoomTextures.Add("connorRoom", new TextureContainer("ConnorTexture", "ConnorTexture", "ConnorTexture"));
        }

        public static void AddContentToMode(EditorMode mode, bool vanillaCompliant)
        {
            AssetManager assetMan = BasePlugin.Instance.assetMan;
            // by default, AddToolToCategory doesnt create the category if it doesn't exist, so if any of these dont exist in the mode we are editing, this will do nothing.
            EditorInterfaceModes.AddToolToCategory(mode, "rooms", new RoomTool("beanHouse", assetMan.Get<Sprite>("Editor_BeanHouse")));
            EditorInterfaceModes.AddToolToCategory(mode, "rooms", new RoomTool("connorRoom", assetMan.Get<Sprite>("Editor_Connor")));

            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pri_benz"));
            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pri_mii13"));
            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pri_dylan"));

            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pst_uglykids"));
            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pst_baldisong"));
            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pst_depression"));
            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pst_chef"));
            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool("pst_wide"));

            EditorInterfaceModes.AddToolToCategory(mode, "npcs", new NPCTool("misterbenz", assetMan.Get<Sprite>("Editor_MrBenz")));
            EditorInterfaceModes.AddToolToCategory(mode, "npcs", new NPCTool("mii13", assetMan.Get<Sprite>("Editor_Mii13")));
            EditorInterfaceModes.AddToolToCategory(mode, "npcs", new NPCTool("bigdylan", assetMan.Get<Sprite>("Editor_Dylan")));

            EditorInterfaceModes.AddToolToCategory(mode, "objects", new ObjectToolNoRotation("beanzphone", assetMan.Get<Sprite>("Editor_BeanPhone")));
            EditorInterfaceModes.AddToolToCategory(mode, "objects", new ObjectToolNoRotation("beanzlamp", assetMan.Get<Sprite>("Editor_BeanLamp")));
            EditorInterfaceModes.AddToolToCategory(mode, "objects", new ObjectToolNoRotation("connorball", assetMan.Get<Sprite>("Editor_ConnorBall")));

            EditorInterfaceModes.AddToolToCategory(mode, "items", new ItemTool("pringuls"));
            EditorInterfaceModes.AddToolToCategory(mode, "items", new ItemTool("shit"));
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

            LevelLoaderPlugin.Instance.itemObjects.Add("pringuls", assetMan.Get<ItemObject>("Pringuls"));
            LevelLoaderPlugin.Instance.itemObjects.Add("shit", assetMan.Get<ItemObject>("Shit"));

            LevelLoaderPlugin.Instance.posterAliases.Add("pri_benz", assetMan.Get<NPC>("MrBenz").Poster);
            LevelLoaderPlugin.Instance.posterAliases.Add("pri_mii13", assetMan.Get<NPC>("Mii13").Poster);
            LevelLoaderPlugin.Instance.posterAliases.Add("pri_dylan", assetMan.Get<NPC>("BigDylan").Poster);

            LevelLoaderPlugin.Instance.posterAliases.Add("pst_uglykids", assetMan.Get<PosterObject>("PST_UglyKids"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_baldisong", assetMan.Get<PosterObject>("PST_BaldiSong"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_depression", assetMan.Get<PosterObject>("PST_Depression"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_chef", assetMan.Get<PosterObject>("PST_Chef"));
            LevelLoaderPlugin.Instance.posterAliases.Add("pst_wide", assetMan.Get<PosterObject>("PST_Wide"));
        }
    }
}