using UnityEngine;
using MTM101BaldAPI;

namespace Baldibasicpoop.CustomItems
{
	public class ITM_Pringuls : Item
	{
		public override bool Use(PlayerManager pm)
		{
			PlayerManager plrMang = pm;
			plrMang.ec.MakeNoise(plrMang.transform.position, 120);
			for (int i = 0; i < UnityEngine.Object.FindObjectsOfType<GottaSweep>().Length; i++)
			{
				if (i < UnityEngine.Object.FindObjectsOfType<GottaSweep>().Length)
				{
					GottaSweep gs = UnityEngine.Object.FindObjectsOfType<GottaSweep>()[i];
					gs.behaviorStateMachine.ChangeState(new GottaSweep_SweepingTime(gs, gs));
				}
			}
			UnityEngine.Object.Destroy(base.gameObject);
			return true;
		}
	}
}
