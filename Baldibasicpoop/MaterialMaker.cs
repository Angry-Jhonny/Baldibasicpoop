using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI;
using System.Linq;
using UnityEngine;
using BepInEx;

namespace Baldibasicpoop
{
    public class MaterialMaker
    {
        public Material MakeMaterial(AssetManager assetMan, string mainMaterial, string lightGuide = null)
        {
            Material tileBaseMat = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "TileBase" && x.GetInstanceID() >= 0);
            Shader standardShader = Resources.FindObjectsOfTypeAll<Shader>().First(x => x.name == "Shader Graphs/Standard");

            Material mat = new Material(tileBaseMat) { name = mainMaterial };
            mat.shader = standardShader;
            mat.SetMainTexture(assetMan.Get<Texture2D>(mainMaterial));
            if (lightGuide != null)
            {
                mat.SetTexture("_LightGuide", assetMan.Get<Texture2D>(lightGuide));
            }
            return mat;
        }
    }
}
