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

namespace Baldibasicpoop
{
    public static class EditorSupport
    {
        public static void AddEditorContent()
        {
            AssetManager assetMan = BasePlugin.Instance.assetMan;
            EditorInterface.AddNPCVisual("misterbenz", assetMan.Get<NPC>("MisterBenz"));
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanFloor");
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanWall");
            LevelStudioPlugin.Instance.selectableTextures.Add("BeanCeil");
            LevelStudioPlugin.Instance.selectableShopItems.AddRange(new string[] { "pringuls" });

            EditorInterfaceModes.AddModeCallback(AddContentToMode);
            LevelStudioPlugin.Instance.defaultRoomTextures.Add("beanHouse", new TextureContainer("BeanFloor", "BeanWall", "BeanCeil"));
        }

        public static void AddContentToMode(EditorMode mode, bool vanillaCompliant)
        {
            AssetManager assetMan = BasePlugin.Instance.assetMan;
            // by default, AddToolToCategory doesnt create the category if it doesn't exist, so if any of these dont exist in the mode we are editing, this will do nothing.
            EditorInterfaceModes.AddToolToCategory(mode, "rooms", new RoomTool("beanHouse", assetMan.Get<Sprite>("Editor_JailCell")));
            EditorInterfaceModes.AddToolToCategory(mode, "posters", new PosterTool(assetMan.Get<NPC>("MisterBenz").Poster.baseTexture.name));
            EditorInterfaceModes.AddToolToCategory(mode, "npcs", new NPCTool("misterbenz", assetMan.Get<Sprite>("Editor_MrBenz")));
            EditorInterfaceModes.AddToolToCategory(mode, "objects", new ObjectTool("BeanzPhone", assetMan.Get<Sprite>("Editor_BeanPhone")));
            EditorInterfaceModes.AddToolsToCategory(mode, "items", new ItemTool[]
            {
                new ItemTool("pringuls")
            });
        }
    }

    public static class LoaderSupport
    {
        public static void AddLoaderContent()
        {
            AssetManager assetMan = BasePlugin.Instance.assetMan;
            LevelLoaderPlugin.Instance.npcAliases.Add("misterbenz", assetMan.Get<NPC>("MisterBenz"));
            LevelLoaderPlugin.Instance.posterAliases.Add(assetMan.Get<NPC>("MisterBenz").Poster.baseTexture.name, assetMan.Get<NPC>("MisterBenz").Poster);
            LevelLoaderPlugin.Instance.itemObjects.Add("pringuls", assetMan.Get<ItemObject>("Pringuls"));
        }
    }
}