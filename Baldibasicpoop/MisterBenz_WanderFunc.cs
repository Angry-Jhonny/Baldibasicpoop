using System;

namespace Baldibasicpoop
{
    internal class MisterBenz_Wander : MisterBenz_StateBase
    {
        public MisterBenz_Wander(MisterBenz benz) : base(benz)
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

        public override void OnStateTriggerStay(Collider other, bool validCollision)
		{
            base.OnStateTriggerStay(other, validCollision);
            Entity component = other.GetComponent<Entity>();
			if (component != null && (component.gameObject.layer == LayerMask.NameToLayer("NPCs") || component.gameObject.layer == LayerMask.NameToLayer("Player")))
			{
				benz.Explode(other);
			}
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
        }
    }
}
