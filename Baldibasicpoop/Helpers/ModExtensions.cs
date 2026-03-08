using Baldibasicpoop.Structures;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Baldibasicpoop.Helpers
{
    public class ModExtensions
    {
        public Material MakeMaterial(AssetManager assetMan, string mainMaterial)
        {
            Material tileBaseMat = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "TileBase" && x.GetInstanceID() >= 0);
            Shader standardShader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name == "Shader Graphs/Standard");

            Material mat = new Material(tileBaseMat) { name = mainMaterial };
            mat.shader = standardShader;
            mat.SetMainTexture(assetMan.Get<Texture2D>(mainMaterial));
            return mat;
        }
        public Material MakeMaterial(AssetManager assetMan, string mainMaterial, string lightGuide)
        {
            Material tileBaseMat = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "TileBase" && x.GetInstanceID() >= 0);
            Shader standardShader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name == "Shader Graphs/Standard");

            Material mat = new Material(tileBaseMat) { name = mainMaterial };
            mat.shader = standardShader;
            mat.SetMainTexture(assetMan.Get<Texture2D>(mainMaterial));
            mat.SetTexture("_LightGuide", assetMan.Get<Texture2D>(lightGuide));
            return mat;
        }



        public void AddNPCToLevel(SceneObject scene, NPC npc, int Weight)
        {
            scene.potentialNPCs.Add(
            new WeightedNPC()
            {
                selection = npc,
                weight = Weight
            });
        }

        public void AddForcedNPC(SceneObject scene, NPC npc)
        {
            scene.forcedNpcs = scene.forcedNpcs.AddToArray(npc);
            scene.additionalNPCs = Mathf.Max(scene.additionalNPCs - 1, 0);
        }

        public void AddForcedNPC(SceneObject scene, NPC[] npcs)
        {
            foreach (NPC npc in npcs)
            {
                scene.forcedNpcs = scene.forcedNpcs.AddToArray(npc);
                scene.additionalNPCs = Mathf.Max(scene.additionalNPCs - 1, 0);
            }
        }

        public void AddItemToShop(SceneObject scene, ItemObject item, int Weight)
        {
            scene.shopItems = scene.shopItems.AddRangeToArray(new WeightedItemObject[]
            {
                new WeightedItemObject()
                {
                    selection = item,
                    weight = Weight
                }});
        }

        public void AddItemToLevel(CustomLevelObject obj, ItemObject item, int Weight)
        {
            obj.potentialItems = obj.potentialItems.AddRangeToArray(new WeightedItemObject[]
            {
                new WeightedItemObject()
                {
                    selection = item,
                    weight = Weight
                }
            });
        }

        public void AddStructureWithParametersToLevel(CustomLevelObject obj, StructureBuilder structure, IntVector2[] minmax, int Weight)
        {
            obj.potentialStructures = obj.potentialStructures.AddToArray(new WeightedStructureWithParameters()
            {
                selection = new StructureWithParameters()
                {
                    parameters = new StructureParameters()
                    {
                        minMax = minmax
                    },
                    prefab = structure
                },
                weight = Weight
            });
        }

        public void AddForcedStructureWithParametersToLevel(CustomLevelObject obj, StructureBuilder structure, IntVector2[] minmax)
        {
            obj.forcedStructures = obj.forcedStructures.AddToArray(new StructureWithParameters()
            {
                parameters = new StructureParameters()
                {
                    minMax = minmax
                },
                prefab = structure
            });
        }

        public Image AddUIOverlay(Sprite Image)
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
