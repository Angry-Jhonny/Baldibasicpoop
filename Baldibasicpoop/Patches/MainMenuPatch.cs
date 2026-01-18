using HarmonyLib;
using MTM101BaldAPI.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Baldibasicpoop.Patches
{
    [HarmonyPatch(typeof(MainMenu))]
    [HarmonyPatch("Start")]
    internal class MainMenuPatch
    {
        static void Postfix(MainMenu __instance)
        {
            __instance.gameObject.transform.Find("Image").GetComponent<Image>().sprite = BasePlugin.Instance.assetMan.Get<Sprite>("MainMenuImage");
        }
    }
}