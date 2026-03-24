using MTM101BaldAPI;
using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    internal class BigDylan_StateBase : NpcState
    {
        public BigDylan_StateBase(BigDylan dylan) : base(dylan)
        {
            this.dylan = dylan;
        }

        protected BigDylan dylan;
    }

    public class BigDylan : NPC
    {
        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new BigDylan_Wander(this));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("DYL_Idle");
            audMan = base.GetComponent<PropagatedAudioManager>();
            isDead = false;
        }

        public float wanderSpeed = 10f;
        public float chaseSpeed = 18f;

        public Collider collidedGuy;
        public bool isDead;

        [SerializeField]
        public PropagatedAudioManager audMan;
    }

    internal class BigDylan_Dead : BigDylan_StateBase
    {
        public BigDylan_Dead(BigDylan dylan) : base(dylan)
        {
        }

        public override void Enter()
        {
            base.Enter();
            base.ChangeNavigationState(new NavigationState_DoNothing(this.npc, 999));
            dylan.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("DYL_Dead");
            dylan.audMan.audioDevice.Stop();
            dylan.audMan.FlushQueue(true);
            dylan.audMan.SetLoop(false);
            dylan.audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("DYL_Death"));
            dylan.isDead = true;
        }

        public override void DestinationEmpty() => base.DestinationEmpty();
    }

    internal class BigDylan_Chase : BigDylan_StateBase
    {
        public BigDylan_Chase(BigDylan dylan) : base(dylan)
        {
        }

        public override void Enter()
        {
            base.Enter();
            dylan.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("DYL_Chase");
            dylan.audMan.QueueAudio(BasePlugin.Instance.assetMan.Get<SoundObject>("DYL_Sing"));
            dylan.audMan.SetLoop(true);
            dylan.audMan.maintainLoop = true;
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            Flee();
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
            base.ChangeNavigationState(new NavigationState_TargetPlayer(this.npc, 128, player.transform.position));
        }

        private void Flee()
        {
            dylan.audMan.audioDevice.Stop();
            dylan.audMan.FlushQueue(true);
            dylan.audMan.SetLoop(false);
            base.npc.behaviorStateMachine.ChangeState(new BigDylan_Flee(dylan));
        }

        public override void OnStateTriggerStay(Entity otherEntity, Collider other, bool validCollision)
        {
            base.OnStateTriggerStay(otherEntity, other, validCollision);
            Entity component = other.GetComponent<Entity>();
            if (component != null && (component.gameObject.layer == LayerMask.NameToLayer("Player")))
            {
                PlayerManager plr = other.GetComponent<PlayerManager>();
                ItemManager itm = plr.itm;
                if (itm.Has(EnumExtensions.GetFromExtendedName<Items>("SmallPhilip")))
                {
                    dylan.behaviorStateMachine.ChangeState(new BigDylan_Dead(dylan));
                    itm.Remove(EnumExtensions.GetFromExtendedName<Items>("SmallPhilip"));
                }
                else
                {
                    Flee();
                }
            }
        }
    }

    internal class BigDylan_Flee : BigDylan_StateBase
    {
        public BigDylan_Flee(BigDylan dylan) : base(dylan)
        {
        }

        public override void Enter()
        {
            base.Enter();
            delay = UnityEngine.Random.Range(15, 75);
            dylan.spriteRenderer[0].gameObject.SetActive(false);
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 128));
        }

        public override void Update()
        {
            base.Update();
            if (delay > 0f)
            {
                delay -= dylan.ec.NpcTimeScale * Time.deltaTime;
            }
            else
            {
                dylan.spriteRenderer[0].gameObject.SetActive(true);
                dylan.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("DYL_Idle");
                base.npc.behaviorStateMachine.ChangeState(new BigDylan_Wander(dylan));
            }
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 128));
        }

        public float delay;
    }

    internal class BigDylan_Stare : BigDylan_StateBase
    {
        public BigDylan_Stare(BigDylan dylan) : base(dylan)
        {
        }

        public override void Enter()
        {
            base.Enter();
            base.ChangeNavigationState(new NavigationState_DoNothing(this.npc, 999));
            dylan.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("DYL_Stare");
            dylan.audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("SFX_Wiplash"));
        }

        public override void DestinationEmpty() => base.DestinationEmpty();

        public override void PlayerLost(PlayerManager player)
        {
            base.PlayerLost(player);
            base.npc.behaviorStateMachine.ChangeState(new BigDylan_Flee(dylan));
        }

        public override void InPlayerSight(PlayerManager player)
        {
            base.InPlayerSight(player);
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);
            if (distance < 40f)
            {
                base.npc.behaviorStateMachine.ChangeState(new BigDylan_Chase(dylan));
            }
        }
    }

    internal class BigDylan_Wander : BigDylan_StateBase
    {
        public BigDylan_Wander(BigDylan dylan) : base(dylan)
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

        public override void InPlayerSight(PlayerManager player)
        {
            base.InPlayerSight(player);
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);
            base.npc.behaviorStateMachine.ChangeState(new BigDylan_Stare(dylan));
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerSighted(player);
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);
            if (distance < 60f)
            {
                base.npc.behaviorStateMachine.ChangeState(new BigDylan_Flee(dylan));
            }
        }
    }
}
