using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using PlusStudioLevelFormat;
using PlusStudioLevelLoader;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Baldibasicpoop.Helpers
{
    public class UsefulHelpers
    {
        /// <summary>
        /// Creates a material that is affected to Tile Lighting.
        /// </summary>
        /// <returns></returns>
        public Material MAT_CreateMaterial(AssetManager assetMan, string mainMaterial)
        {
            Material tileBaseMat = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "TileBase" && x.GetInstanceID() >= 0);
            Shader standardShader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name == "Shader Graphs/Standard");

            Material mat = new Material(tileBaseMat) { name = mainMaterial };
            mat.shader = standardShader;
            mat.SetMainTexture(assetMan.Get<Texture2D>(mainMaterial));
            return mat;
        }



        /// <summary>
        /// Creates a material that is affected to Tile Lighting with a LightGuide.
        /// </summary>
        /// <returns></returns>
        public Material MAT_CreateMaterial(AssetManager assetMan, string mainMaterial, string lightGuide)
        {
            Material tileBaseMat = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "TileBase" && x.GetInstanceID() >= 0);
            Shader standardShader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name == "Shader Graphs/Standard");

            Material mat = new Material(tileBaseMat) { name = mainMaterial };
            mat.shader = standardShader;
            mat.SetMainTexture(assetMan.Get<Texture2D>(mainMaterial));
            mat.SetTexture("_LightGuide", assetMan.Get<Texture2D>(lightGuide));
            return mat;
        }



        /// <summary>
        /// Creates a material with a Mask that is affected to Tile Lighting.
        /// </summary>
        /// <returns></returns>
        public Material MAT_CreateMaterialWithMask(AssetManager assetMan, string mainMaterial, string maskMaterial)
        {
            Material tileBaseMat = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "TileBase" && x.GetInstanceID() >= 0);
            Shader standardShader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name == "Shader Graphs/Standard");

            Material mat = new Material(tileBaseMat) { name = mainMaterial };
            mat.shader = standardShader;
            mat.SetMainTexture(assetMan.Get<Texture2D>(mainMaterial));
            mat.SetMaskTexture(assetMan.Get<Texture2D>(maskMaterial));
            return mat;
        }



        /// <summary>
        /// Creates a material with a Mask that is affected to Tile Lighting with a Lightguide.
        /// </summary>
        /// <returns></returns>
        public Material MAT_CreateMaterialWithMask(AssetManager assetMan, string mainMaterial, string lightGuide, string maskMaterial)
        {
            Material tileBaseMat = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "TileBase" && x.GetInstanceID() >= 0);
            Shader standardShader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name == "Shader Graphs/Standard");

            Material mat = new Material(tileBaseMat) { name = mainMaterial };
            mat.shader = standardShader;
            mat.SetMainTexture(assetMan.Get<Texture2D>(mainMaterial));
            mat.SetTexture("_LightGuide", assetMan.Get<Texture2D>(lightGuide));
            mat.SetMaskTexture(assetMan.Get<Texture2D>(maskMaterial));
            return mat;
        }



        /// <summary>
        /// Reads a .rbpl File and returns a ExtendedRoomAsset.
        /// </summary>
        /// <returns></returns>
        public WeightedRoomAsset ROOM_ReadRoomFile(string Path, Transform Light, float PosterChance = 0.25f)
        {
            WeightedRoomAsset potentialRoom = new WeightedRoomAsset();

            BinaryReader reader = new BinaryReader(File.OpenRead(Path));
            BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
            reader.Close();
            ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
            asset.lightPre = Light;
            asset.posterChance = PosterChance;
            potentialRoom.selection = asset;
            potentialRoom.weight = 100;
            return potentialRoom;
        }



        /// <summary>
        /// Reads a .rbpl File and returns a ExtendedRoomAsset.
        /// </summary>
        /// <returns></returns>
        public WeightedRoomAsset ROOM_ReadRoomFile(string Path, Transform Light, ItemObject[] items, float PosterChance = 0.25f)
        {
            WeightedRoomAsset potentialRoom = new WeightedRoomAsset();

            BinaryReader reader = new BinaryReader(File.OpenRead(Path));
            BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
            reader.Close();
            ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
            asset.lightPre = Light;
            asset.posterChance = PosterChance;
            asset.itemList.AddRange(items);
            potentialRoom.selection = asset;
            potentialRoom.weight = 100;
            return potentialRoom;
        }



        /// <summary>
        /// Reads a .rbpl File and returns a ExtendedRoomAsset.
        /// </summary>
        /// <returns></returns>
        public WeightedRoomAsset ROOM_ReadRoomFile(string Path, Transform Light, float WindowChance, float PosterChance = 0.25f)
        {
            WeightedRoomAsset potentialRoom = new WeightedRoomAsset();

            BinaryReader reader = new BinaryReader(File.OpenRead(Path));
            BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
            reader.Close();
            ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
            asset.lightPre = Light;
            asset.windowChance = WindowChance;
            asset.posterChance = PosterChance;
            potentialRoom.selection = asset;
            potentialRoom.weight = 100;
            return potentialRoom;
        }



        /// <summary>
        /// Reads a .rbpl File and returns a ExtendedRoomAsset.
        /// </summary>
        /// <returns></returns>
        public WeightedRoomAsset ROOM_ReadRoomFile(string Path, Transform Light, ItemObject[] items, float WindowChance, float PosterChance = 0.25f)
        {
            WeightedRoomAsset potentialRoom = new WeightedRoomAsset();

            BinaryReader reader = new BinaryReader(File.OpenRead(Path));
            BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
            reader.Close();
            ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
            asset.lightPre = Light;
            asset.windowChance = WindowChance;
            asset.posterChance = PosterChance;
            asset.itemList.AddRange(items);
            potentialRoom.selection = asset;
            potentialRoom.weight = 100;
            return potentialRoom;
        }



        /// <summary>
        /// Reads a folder of .rbpl Files and returns a ExtendedRoomAsset Array.
        /// </summary>
        /// <returns></returns>
        public List<WeightedRoomAsset> ROOM_ReadRoomFiles(string FolderPath, Transform Light, float PosterChance = 0.25f)
        {
            List<WeightedRoomAsset> potentialRooms = new List<WeightedRoomAsset>();

            string[] roomPaths = Directory.GetFiles(FolderPath, "*.rbpl");
            for (int i = 0; i < roomPaths.Length; i++)
            {
                BinaryReader reader = new BinaryReader(File.OpenRead(roomPaths[i]));
                BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
                reader.Close();
                ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
                asset.lightPre = Light;
                asset.posterChance= PosterChance;
                potentialRooms.Add(new WeightedRoomAsset()
                {
                    selection = asset,
                    weight = 100
                });
            }
            return potentialRooms;
        }



        /// <summary>
        /// Reads a folder of .rbpl Files and returns a ExtendedRoomAsset Array.
        /// </summary>
        /// <returns></returns>
        public List<WeightedRoomAsset> ROOM_ReadRoomFiles(string FolderPath, Transform Light, ItemObject[] items, float PosterChance = 0.25f)
        {
            List<WeightedRoomAsset> potentialRooms = new List<WeightedRoomAsset>();

            string[] roomPaths = Directory.GetFiles(FolderPath, "*.rbpl");
            for (int i = 0; i < roomPaths.Length; i++)
            {
                BinaryReader reader = new BinaryReader(File.OpenRead(roomPaths[i]));
                BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
                reader.Close();
                ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
                asset.lightPre = Light;
                asset.posterChance = PosterChance;
                asset.itemList.AddRange(items);
                potentialRooms.Add(new WeightedRoomAsset()
                {
                    selection = asset,
                    weight = 100
                });
            }
            return potentialRooms;
        }



        /// <summary>
        /// Reads a folder of .rbpl Files and returns a ExtendedRoomAsset Array.
        /// </summary>
        /// <returns></returns>
        public List<WeightedRoomAsset> ROOM_ReadRoomFiles(string FolderPath, Transform Light, float WindowChance, float PosterChance = 0.25f)
        {
            List<WeightedRoomAsset> potentialRooms = new List<WeightedRoomAsset>();

            string[] roomPaths = Directory.GetFiles(FolderPath, "*.rbpl");
            for (int i = 0; i < roomPaths.Length; i++)
            {
                BinaryReader reader = new BinaryReader(File.OpenRead(roomPaths[i]));
                BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
                reader.Close();
                ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
                asset.lightPre = Light;
                asset.windowChance = WindowChance;
                asset.posterChance = PosterChance;
                potentialRooms.Add(new WeightedRoomAsset()
                {
                    selection = asset,
                    weight = 100
                });
            }
            return potentialRooms;
        }



        /// <summary>
        /// Reads a folder of .rbpl Files and returns a ExtendedRoomAsset Array.
        /// </summary>
        /// <returns></returns>
        public List<WeightedRoomAsset> ROOM_ReadRoomFiles(string FolderPath, Transform Light, ItemObject[] items, float WindowChance, float PosterChance = 0.25f)
        {
            List<WeightedRoomAsset> potentialRooms = new List<WeightedRoomAsset>();

            string[] roomPaths = Directory.GetFiles(FolderPath, "*.rbpl");
            for (int i = 0; i < roomPaths.Length; i++)
            {
                BinaryReader reader = new BinaryReader(File.OpenRead(roomPaths[i]));
                BaldiRoomAsset formatAsset = BaldiRoomAsset.Read(reader);
                reader.Close();
                ExtendedRoomAsset asset = LevelImporter.CreateRoomAsset(formatAsset);
                asset.lightPre = Light;
                asset.windowChance = WindowChance;
                asset.posterChance = PosterChance;
                asset.itemList.AddRange(items);
                potentialRooms.Add(new WeightedRoomAsset()
                {
                    selection = asset,
                    weight = 100
                });
            }
            return potentialRooms;
        }



        /// <summary>
        /// Creates a Room Group with different Floor Wall Ceiling Textures.
        /// </summary>
        /// <returns></returns>
        public RoomGroup ROOM_CreateRoomGroup(string Name, float HallChance, IntVector2 MinMaxRooms, WeightedRoomAsset[] Rooms, WeightedTransform[] Light, WeightedTexture2D[] Floor, WeightedTexture2D[] Wall, WeightedTexture2D[] Ceiling)
        {
            RoomGroup roomGroup = new RoomGroup();
            roomGroup.name = Name;
            roomGroup.stickToHallChance = HallChance;
            roomGroup.minRooms = MinMaxRooms.x;
            roomGroup.maxRooms = MinMaxRooms.z;
            roomGroup.floorTexture = roomGroup.floorTexture.AddRangeToArray(Floor);
            roomGroup.wallTexture = roomGroup.wallTexture.AddRangeToArray(Wall);
            roomGroup.ceilingTexture = roomGroup.ceilingTexture.AddRangeToArray(Ceiling);
            roomGroup.light = roomGroup.light.AddRangeToArray(Light);
            roomGroup.potentialRooms = roomGroup.potentialRooms.AddRangeToArray(Rooms);
            return roomGroup;
        }



        /// <summary>
        /// Creates a Room Group with different Floor Wall Ceiling Textures but only one room type.
        /// </summary>
        /// <returns></returns>
        public RoomGroup ROOM_CreateRoomGroup(string Name, float HallChance, IntVector2 MinMaxRooms, WeightedRoomAsset Room, WeightedTransform[] Light, WeightedTexture2D[] Floor, WeightedTexture2D[] Wall, WeightedTexture2D[] Ceiling)
        {
            RoomGroup roomGroup = new RoomGroup();
            roomGroup.name = Name;
            roomGroup.stickToHallChance = HallChance;
            roomGroup.minRooms = MinMaxRooms.x;
            roomGroup.maxRooms = MinMaxRooms.z;
            roomGroup.floorTexture = roomGroup.floorTexture.AddRangeToArray(Floor);
            roomGroup.wallTexture = roomGroup.wallTexture.AddRangeToArray(Wall);
            roomGroup.ceilingTexture = roomGroup.ceilingTexture.AddRangeToArray(Ceiling);
            roomGroup.light = roomGroup.light.AddRangeToArray(Light);
            roomGroup.potentialRooms = roomGroup.potentialRooms.AddToArray(Room);
            return roomGroup;
        }



        /// <summary>
        /// Creates a Room Group with same Floor Wall Ceiling Textures.
        /// </summary>
        /// <returns></returns>
        public RoomGroup ROOM_CreateRoomGroup(string Name, float HallChance, IntVector2 MinMaxRooms, WeightedRoomAsset[] Rooms, WeightedTransform[] Light, WeightedTexture2D[] Texture)
        {
            RoomGroup roomGroup = new RoomGroup();
            roomGroup.name = Name;
            roomGroup.stickToHallChance = HallChance;
            roomGroup.minRooms = MinMaxRooms.x;
            roomGroup.maxRooms = MinMaxRooms.z;
            roomGroup.floorTexture = roomGroup.floorTexture.AddRangeToArray(Texture);
            roomGroup.wallTexture = roomGroup.wallTexture.AddRangeToArray(Texture);
            roomGroup.ceilingTexture = roomGroup.ceilingTexture.AddRangeToArray(Texture);
            roomGroup.light = roomGroup.light.AddRangeToArray(Light);
            roomGroup.potentialRooms = roomGroup.potentialRooms.AddRangeToArray(Rooms);
            return roomGroup;
        }



        /// <summary>
        /// Creates a Room Group with same Floor Wall Ceiling Textures but only one room type.
        /// </summary>
        /// <returns></returns>
        public RoomGroup ROOM_CreateRoomGroup(string Name, float HallChance, IntVector2 MinMaxRooms, WeightedRoomAsset Room, WeightedTransform[] Light, WeightedTexture2D[] Texture)
        {
            RoomGroup roomGroup = new RoomGroup();
            roomGroup.name = Name;
            roomGroup.stickToHallChance = HallChance;
            roomGroup.minRooms = MinMaxRooms.x;
            roomGroup.maxRooms = MinMaxRooms.z;
            roomGroup.floorTexture = roomGroup.floorTexture.AddRangeToArray(Texture);
            roomGroup.wallTexture = roomGroup.wallTexture.AddRangeToArray(Texture);
            roomGroup.ceilingTexture = roomGroup.ceilingTexture.AddRangeToArray(Texture);
            roomGroup.light = roomGroup.light.AddRangeToArray(Light);
            roomGroup.potentialRooms = roomGroup.potentialRooms.AddToArray(Room);
            return roomGroup;
        }



        /// <summary>
        /// Creates an UI Overlay like Beans gum or playtime jumprope.
        /// </summary>
        /// <returns></returns>
        public Image UI_AddUIOverlay(Sprite Image)
        {
            Image UIImage = Singleton<CoreGameManager>.Instance.GetHud(0).gameObject.AddComponent<Image>();
            UIImage.sprite = Image;
            RectTransform rect = UIImage.GetComponent<RectTransform>();
            rect.anchorMax = new Vector2(1, 1);
            rect.anchorMin = new Vector2(0, 0);
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;

            return UIImage;
        }
    }
}
