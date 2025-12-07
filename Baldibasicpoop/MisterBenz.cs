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
                var offset = (other.transform.position - transform.position).normalized;
                other.GetComponent<Entity>().AddForce(new Force(offset, explosionSpeed * 1.9f, -explosionSpeed));
                collided = true;
            }
        }

        private void LateUpdate()
        {
            if (collided)
            {
                spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Explod_Tex");
                if (timeToDisappear > 0f)
                {
                    timeToDisappear -= Time.deltaTime;
                }
                else
                {
                    spriteRenderer[0].gameObject.SetActive(false);
                }
				Navigator.Entity.SetInteractionState(false);
            }
            
        }

        public float wanderSpeed = 10f;
        public float chaseSpeed = 18f; // i added this if your npc chases the player

        const float explosionSpeed = 25f;

        public bool collided;
        public float timeToDisappear;

        [SerializeField]
        public PropagatedAudioManager pam;
    }
}
