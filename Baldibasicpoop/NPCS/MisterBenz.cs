using System;
using System.Collections;
using System.Collections.Generic;
using MTM101BaldAPI.Reflection;
using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    internal class MisterBenz_StateBase : NpcState
    {
        public MisterBenz_StateBase(MisterBenz benz) : base(benz)
        {
            this.benz = benz;
        }

        protected MisterBenz benz;
    }

    public class MisterBenz : NPC
    {
        public override void Initialize()
        {
			base.Initialize();
            behaviorStateMachine.ChangeState(new MisterBenz_Wander(this));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Idle"); // collects the sprite inside the assetmanager on the plugin
            pam = base.GetComponent<PropagatedAudioManager>();
		
        }

        public void Explode(Collider other) // just added this cuz your npc will explode for s
        {
            if (!collided)
            {
                timeToDisappear = 5f;
				pam.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("BEN_Explod"));
                collidedGuy = other;
                delay = 0.5f;
                collided = true;
            }
        }

        private void LateUpdate()
        {
            if (collided)
            {
                if (timeToDisappear > 0f)
                {
                    timeToDisappear -= ec.NpcTimeScale * Time.deltaTime;
                }
                else
                {
                    spriteRenderer[0].gameObject.SetActive(false);
                }
                if (delay > 0f)
                {
                    delay -= ec.NpcTimeScale * Time.deltaTime;
                }
                else
                {
                    if (exploded == false)
                    {
                        exploded = true;
                        spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Explod");
                        var offset = (collidedGuy.transform.position - transform.position).normalized;
                        collidedGuy.GetComponent<Entity>().AddForce(new Force(offset, explosionSpeed * 1.9f, -explosionSpeed));
                    }
                }
                Navigator.Entity.SetInteractionState(false);
            }
            
        }

        public float wanderSpeed = 10f;
        public float chaseSpeed = 18f; // i added this if your npc chases the player

        public float explosionSpeed = 10f;

        public float delay = 0;

        public bool collided;
        public bool exploded;
        public Collider collidedGuy;
        public float timeToDisappear;

        [SerializeField]
        public PropagatedAudioManager pam;
    }

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
        //removed method playersaw cuz its redundant
    }
}
