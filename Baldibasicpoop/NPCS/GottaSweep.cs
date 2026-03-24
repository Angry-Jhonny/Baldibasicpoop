using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    public class GottaSweep_Pringuls : GottaSweep_StateBase
    {
        private float sweepTime;
        private bool cleanedMess;

        private Vector3 position;

        public GottaSweep_Pringuls(NPC npc, GottaSweep gottaSweep, Vector3 pos) : base(npc, gottaSweep)
        {
            position = pos;
        }

        public override void Enter()
        {
            base.Enter();
            ChangeNavigationState(new NavigationState_TargetPosition(npc, 0, position));
            cleanedMess = false;
            sweepTime = gottaSweep.GetRandomSweepTime;
            gottaSweep.StartSweeping();
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            cleanedMess = true;
            ChangeNavigationState(new NavigationState_WanderRandom(npc, 0));
        }

        public override void Update()
        {
            base.Update();
            if (cleanedMess)
            {
                sweepTime -= Time.deltaTime * npc.TimeScale;
                if (sweepTime <= 0f)
                {
                    npc.behaviorStateMachine.ChangeState(new GottaSweep_Returning(npc, gottaSweep));
                }
            }
        }
    }
}
