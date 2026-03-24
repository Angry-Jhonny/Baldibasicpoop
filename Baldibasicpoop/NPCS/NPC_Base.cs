using System;
using System.Collections;
using System.Collections.Generic;
using MTM101BaldAPI;
using MTM101BaldAPI.Reflection;
using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    internal class BaseNPC_StateBase : NpcState
    {
        public BaseNPC_StateBase(BaseNPC basenpc) : base(basenpc)
        {
            this.basenpc = basenpc;
        }

        protected BaseNPC basenpc;
    }

    public class BaseNPC : NPC
    {
        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new BaseNPC_Wander(this));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("basenpc_Idle");
            audMan = base.GetComponent<PropagatedAudioManager>();

        }

        public float wanderSpeed = 10f;

        [SerializeField]
        public PropagatedAudioManager audMan;
    }
    internal class BaseNPC_Wander : BaseNPC_StateBase
    {
        public BaseNPC_Wander(BaseNPC basenpc) : base(basenpc)
        {
        }

        public override void Enter()
        {
            base.Enter();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }
    }
}
