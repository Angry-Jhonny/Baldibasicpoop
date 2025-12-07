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
            pm.ec.MakeNoise(pm.transform.position, 120);
            for (int i=0; i < UnityEngine.Object.FindObjectsOfType<GottaSweep>().Length; i++)
            {
                if (i < UnityEngine.Object.FindObjectsOfType<GottaSweep>().Length)
                {
                    UnityEngine.Object.FindObjectsOfType<GottaSweep>()[i].navigationStateMachine.ChangeState(new NavigationState_TargetPosition(UnityEngine.Object.FindObjectsOfType<GottaSweep>()[i], 4330, pm.transform.position));
                }
            }
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(dropSound);
            return true;
        }
    }
}
