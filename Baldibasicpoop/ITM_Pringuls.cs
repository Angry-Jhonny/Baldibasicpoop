using MTM101BaldAPI.Components;
using MTM101BaldAPI.PlusExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Baldibasicpoop
{

    public class PringulsManager : MonoBehaviour
    {

    }

    public class ITM_Pringuls : Item
    {
        public SoundObject dropSound;
        public override bool Use(PlayerManager pm)
        {
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(dropSound);
            return true;
        }
    }
}