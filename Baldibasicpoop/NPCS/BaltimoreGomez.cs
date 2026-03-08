using System;
using System.Collections;
using System.Collections.Generic;
using MTM101BaldAPI;
using MTM101BaldAPI.Reflection;
using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    internal class BaltimoreGomez_StateBase : NpcState
    {
        public BaltimoreGomez_StateBase(BaltimoreGomez gomez) : base(gomez)
        {
            this.gomez = gomez;
        }

        protected BaltimoreGomez gomez;
    }

    public class BaltimoreGomez : NPC
    {
        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new BaltimoreGomez_Wander(this, 0));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAGZ_Idle");
            pam = base.GetComponent<PropagatedAudioManager>();

        }

        public float wanderSpeed = 10f;

        [SerializeField]
        public PropagatedAudioManager pam;
    }

    internal class BaltimoreGomez_Chase : BaltimoreGomez_StateBase
    {
        private float priority = 0;
        public BaltimoreGomez_Chase(BaltimoreGomez gomez) : base(gomez)
        {
        }

        public override void Enter()
        {
            base.Enter();
            gomez.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAGZ_Slapi");
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            gomez.behaviorStateMachine.ChangeState(new BaltimoreGomez_Wander(gomez, 30));
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
            base.ChangeNavigationState(new NavigationState_TargetPlayer(this.npc, 128, player.transform.position));
        }

        public override void Hear(GameObject source, Vector3 position, int value)
        {
            base.Hear(source, position, value);
            if (value >= priority)
            {
                priority = value;
                base.ChangeNavigationState(new NavigationState_TargetPosition(this.npc, value, position));
            }
        }
    }

    internal class BaltimoreGomez_Wander : BaltimoreGomez_StateBase
    {
        private float time;
        private bool lookingAtGaymer = false;

        private float timeBeforeChaseFixed = 3f;
        private float timeBeforeChase;

        public BaltimoreGomez_Wander(BaltimoreGomez gomez, float delay) : base(gomez)
        {
            time = delay;
        }

        public override void Enter()
        {
            base.Enter();
            timeBeforeChase = timeBeforeChaseFixed;
            gomez.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAGZ_Idle");
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void Sighted()
        {
            base.Sighted();
            lookingAtGaymer = true;
        }

        public override void Unsighted()
        {
            base.Unsighted();
            lookingAtGaymer = false;
        }

        public override void Update()
        {
            base.Update();
            if (time > 0f)
            {
                time -= gomez.ec.NpcTimeScale * Time.deltaTime;
            }
            else if (timeBeforeChase > 0f && lookingAtGaymer)
            {
                timeBeforeChase -= gomez.ec.NpcTimeScale * Time.deltaTime;
            }
            else if (!lookingAtGaymer)
            {
                timeBeforeChase = timeBeforeChaseFixed;
            }
            else if (lookingAtGaymer)
            {
                gomez.behaviorStateMachine.ChangeState(new BaltimoreGomez_Chase(gomez));
            }
        }
    }
}
