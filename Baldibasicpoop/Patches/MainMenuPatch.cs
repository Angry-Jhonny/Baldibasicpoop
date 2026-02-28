using HarmonyLib;
using MTM101BaldAPI.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Baldibasicpoop.Patches
{
    [HarmonyPatch(typeof(MainMenu))]
    [HarmonyPatch("Start")]
    internal class MainMenuPatch
    {
        static void Postfix(MainMenu __instance)
        {
            __instance.gameObject.transform.Find("Image").GetComponent<Image>().sprite = BasePlugin.Instance.assetMan.Get<Sprite>("MainMenuImage");
            __instance.gameObject.transform.Find("Copyright").GetComponent<TextMeshProUGUI>().text = "©2026 Angry Productions";
            __instance.gameObject.transform.Find("Version").GetComponent<TextMeshProUGUI>().text = "1.2.0";
            __instance.gameObject.transform.Find("Reminder").gameObject.SetActive(false);

        }
    }
}