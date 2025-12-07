using System;

namespace Baldibasicpoop
{
    internal class MisterBenz_Wander : MisterBenz_StateBase
    {
        public MisterBenz_Wander(MisterBenz pointPointer) : base(pointPointer)
        {
        }

        public override void Enter()
        {
            base.Enter();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
        }
    }
}