using System;
using System.Collections;
using System.Collections.Generic;
using MTM101BaldAPI.Reflection;
using MTM101BaldAPI.Components;
using MTM101BaldAPI.Components.Animation;
using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    public class Mystman13_StateBase : NpcState
    {
        public Mystman13_StateBase(Mystman13 Mii13) : base(Mii13)
        {
            this.Mii13 = Mii13;
        }

        protected Mystman13 Mii13;
    }

    public class Mystman13 : NPC
    {

        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new Mystman13_Wander(this));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Mii13_Idle");

            AudioMan = base.GetComponent<AudioManager>();
        }

        public void CatchPlayer()
        {
            time = 30;
            int index = UnityEngine.Random.Range(0, URL.Length);
            Application.OpenURL("https://www.youtube.com/watch?v=" + URL[index]);
            StopChasingPlayer();
        }

        public void StartChasingPlayer()
        {
            behaviorStateMachine.ChangeState(new Mystman13_Chase(this));
            Chasing = true;
        }

        public void StopChasingPlayer()
        {
            chaseSpeed = wanderSpeed;
            behaviorStateMachine.ChangeState(new Mystman13_Wander(this));
            Chasing = false;
        }

        public void UpdateMoveSpeed()
        {
            if (Chasing)
            {
                base.Navigator.SetSpeed(chaseSpeed);
                base.Navigator.maxSpeed = chaseSpeed;
            }
            else
            {
                base.Navigator.SetSpeed(wanderSpeed);
                base.Navigator.maxSpeed = wanderSpeed;
            }
        }

        private void LateUpdate()
        {
            if (time > 0f)
            {
                time -= ec.NpcTimeScale * Time.deltaTime;
            }
        }

        public bool Chasing = false;

        public string[] URL = new string[]
        {
            "HO5s5NrSWbU", // the promo video <3
            "dqPqr545gQQ", // episode 1
            "YQLifO7McNo", // episode 2
            "WyktoNopPs0", // episode 3
            "xZ8E76m_Y-M", // episode 4
            "lNfM_Aws5ZA", // episode 5
        };

        public float speedMultiplier = 1f;

        public float wanderSpeed = 16f;
        public float chaseSpeed = 16f;

        public float time = 0;

        [SerializeField]
        public AudioManager AudioMan;
    }

    public class Mystman13_Chase : Mystman13_StateBase
    {

        public Mystman13_Chase(Mystman13 Mii13) : base(Mii13)
        {
        }

        public override void Update()
        {
            base.Update();
            Mii13.UpdateMoveSpeed();
        }

        public override void OnStateTriggerEnter(Collider other, bool validCollision)
        {
            if (!validCollision)
            {
                return;
            }
            Entity otherEnt = other.GetComponent<Entity>();
            if (otherEnt != null)
            {
                if (otherEnt.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Mii13.CatchPlayer();
                }
            }
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            Mii13.StopChasingPlayer();
            ChangeNavigationState(new NavigationState_WanderRandom(npc, 0));
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
            Mii13.chaseSpeed += Mii13.ec.NpcTimeScale * Time.deltaTime * Mii13.speedMultiplier;
            ChangeNavigationState(new NavigationState_TargetPlayer(npc, 127, player.transform.position));
        }

        public override void PlayerLost(PlayerManager player)
        {
            base.PlayerLost(player);
        }
    }

    public class Mystman13_Wander : Mystman13_StateBase
    {
        public Mystman13_Wander(Mystman13 Mii13) : base(Mii13)
        {
        }

        public override void Update()
        {
            base.Update();
            Mii13.UpdateMoveSpeed();
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

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
            if (Mii13.time > 0f) { }
            else {
                Mii13.AudioMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("Mii13_Hey"));
                Mii13.chaseSpeed = Mii13.wanderSpeed;
                Mii13.StartChasingPlayer();
            }
        }
    }
}
