using Baldibasicpoop.NPCS;
using MTM101BaldAPI.Reflection;
using UnityEngine;

namespace Baldibasicpoop.Events
{
    public class ShotgunEvent : RandomEvent
    {
        public override void Begin()
        {
            base.Begin();
            foreach (NPC npc in ec.Npcs)
            {
                if (npc.Character == Character.Baldi)
                {
                    npc.GetComponent<Baldi>().behaviorStateMachine.ChangeState(new Baldi_Chase_Shotgun(npc.GetComponent<Baldi>(), npc.GetComponent<Baldi>(), this));
                }
            }
        }

        public override void End()
        {
            base.End();
            foreach (NPC npc in ec.Npcs)
            {
                if (npc.Character == Character.Baldi)
                {
                    Baldi baldi = npc.GetComponent<Baldi>();
                    baldi.RestoreRuler();
                    Animator animator = (Animator)baldi.ReflectionGetVariable("animator");
                    animator.enabled = true;
                }
            }
        }

        public HudGauge gauge;
    }
}
