using System;
using System.Collections;
using System.Collections.Generic;
using MTM101BaldAPI.Reflection;
using UnityEngine;

namespace Baldibasicpoop
{
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
                    timeToDisappear -= Time.deltaTime;
                }
                else
                {
                    spriteRenderer[0].gameObject.SetActive(false);
                }
                if (delay > 0f)
                {
                    delay -= Time.deltaTime;
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
}
