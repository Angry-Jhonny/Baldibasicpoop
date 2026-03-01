using Baldibasicpoop.NPCS;
using MTM101BaldAPI;
using UnityEngine;
using static Rewired.Controller;

namespace Baldibasicpoop.CustomItems
{
    public class ITM_SmallPhilip : Item
    {
        public override bool Use(PlayerManager pm)
        {
            bool flag = false;

            PlayerManager plrMang = pm;
            for (int i = 0; i < UnityEngine.Object.FindObjectsOfType<BigDylan>().Length; i++)
            {
                if (i < UnityEngine.Object.FindObjectsOfType<BigDylan>().Length)
                {
                    BigDylan bd = UnityEngine.Object.FindObjectsOfType<BigDylan>()[i];
                    if (Vector3.Distance(plrMang.transform.position, bd.transform.position) < pm.pc.Reach)
                    {
                        plrMang.ec.MakeNoise(plrMang.transform.position, 120);
                        bd.behaviorStateMachine.ChangeState(new BigDylan_Dead(bd));
                        flag = true;
                    }
                }
            }
            UnityEngine.Object.Destroy(base.gameObject);
            return flag;
        }
    }
}