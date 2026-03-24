using Baldibasicpoop.NPCS;

namespace Baldibasicpoop.CustomItems
{
	public class ITM_Pringuls : Item
	{
		public override bool Use(PlayerManager pm)
		{
			PlayerManager plrMang = pm;
			plrMang.ec.MakeNoise(plrMang.transform.position, 64, true);

            foreach (NPC npc in pm.ec.Npcs)
            {
                if (npc.Character == Character.Sweep)
                {
                    npc.GetComponent<GottaSweep>().behaviorStateMachine.ChangeState(new GottaSweep_Pringuls(npc, npc.GetComponent<GottaSweep>(), plrMang.transform.localPosition));
                }
            }

            UnityEngine.Object.Destroy(base.gameObject);
			return true;
		}
	}
}
