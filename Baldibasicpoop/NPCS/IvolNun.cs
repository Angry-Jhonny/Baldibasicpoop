using Baldibasicpoop.CustomMono;
using MTM101BaldAPI;
using MTM101BaldAPI.Reflection;
using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    internal class IvolNun_StateBase : NpcState
    {
        public IvolNun_StateBase(IvolNun ivolnun) : base(ivolnun)
        {
            this.ivolnun = ivolnun;
        }

        protected IvolNun ivolnun;
    }

    public class IvolNun : NPC
    {
        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new IvolNun_Wander(this, 0));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("NUN_Idle");
            audMan = base.GetComponent<PropagatedAudioManager>();
        }

        public void SetChase(bool Chase, bool KillKnee = false)
        {
            if (Chase)
            {
                spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("NUN_Chase");
                audMan.QueueAudio(BasePlugin.Instance.assetMan.Get<SoundObject>("NUN_Chase"));
                audMan.SetLoop(true);
            }
            else
            {
                if (KillKnee) { spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("NUN_KilledYoFuckingKneecapsLosor"); }
                else { spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("NUN_Idle"); }
                audMan.audioDevice.Stop();
                audMan.FlushQueue(true);
                audMan.SetLoop(false);
            }
        }

        public void BreakAnkles(Entity entity)
        {
            if (entity != null && (entity.gameObject.layer == LayerMask.NameToLayer("NPCs") || entity.gameObject.layer == LayerMask.NameToLayer("Player")))
            {
                bool isplayer = false;
                if (entity.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    isplayer = true;
                }
                NunGauge gauge = entity.gameObject.AddComponent<NunGauge>();
                gauge.gauge = null;
                if (isplayer)
                {
                    gauge.gauge = Singleton<CoreGameManager>.Instance.GetHud(0).gaugeManager.ActivateNewGauge(BasePlugin.Instance.assetMan.Get<Sprite>("NUN_KilledYoFuckingKneecapsLosor"), 10);
                }
                gauge.isPlayer = isplayer;
                gauge.ent = entity;
                gauge.time = 10;
                SetChase(false, true);
                behaviorStateMachine.ChangeState(new IvolNun_Wander(this, 30));
                audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("SFX_BoneCrack"));
                audMan.QueueAudio(BasePlugin.Instance.assetMan.Get<SoundObject>("NUN_KillKnees"));
                audMan.QueueAudio(BasePlugin.Instance.assetMan.Get<SoundObject>("NUN_LaughLosor"));
            }
        }

        public float wanderSpeed = 12f;
        public float chaseSpeed = 8f;

        [SerializeField]
        public AudioManager audMan;
    }

    internal class IvolNun_Chase : IvolNun_StateBase
    {
        public IvolNun_Chase(IvolNun ivolnun) : base(ivolnun)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ivolnun.SetChase(true);
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            ivolnun.SetChase(false);
            ivolnun.behaviorStateMachine.ChangeState(new IvolNun_Wander(ivolnun, 30));
        }

        public override void OnStateTriggerEnter(Entity otherEntity, Collider other, bool validCollision)
        {
            base.OnStateTriggerEnter(otherEntity, other, validCollision);
            if (!validCollision)
            {
                return;
            }
            Entity otherEnt = other.GetComponent<Entity>();
            if (otherEnt == null) return;
            ivolnun.BreakAnkles(otherEnt);
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
            ChangeNavigationState(new NavigationState_TargetPlayer(npc, 127, player.transform.position));
        }
    }

    internal class IvolNun_Wander : IvolNun_StateBase
    {
        float timerr;

        public IvolNun_Wander(IvolNun ivolnun, float timer) : base(ivolnun)
        {
            timerr = timer;
        }

        public override void Enter()
        {
            base.Enter();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void Update()
        {
            base.Update();
            if (timerr > 0f)
            {
                timerr -= ivolnun.ec.NpcTimeScale * Time.deltaTime;
            }
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
            if (!(timerr > 0f))
            {
                ivolnun.chaseSpeed = ivolnun.wanderSpeed;
                ivolnun.behaviorStateMachine.ChangeState(new IvolNun_Chase(ivolnun));
            }
        }
    }
}
