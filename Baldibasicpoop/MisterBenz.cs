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
            behaviorStateMachine.ChangeState(new MisterBenz_WanderFunc(this));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Idle"); // collects the sprite inside the assetmanager on the plugin
            pam = base.GetComponent<PropagatedAudioManager>();
		
        }

        public void Explode(Collider other) // just added this cuz your npc will explode for s
        {
            if (!collided)
            {
                other.GetComponent<Entity>().AddForce(new Force(Vector3.zero, 10f, -10f));
                collided = true;
            }
        }

        private void LateUpdate()
        {
            if (collided)
            {
                if (timeToAppear > 0f)
                {
                    timeToAppear -= Time.deltaTime;
                }
				Navigator.Entity.SetInteractionState(false);
				spriteRenderer[0].gameObject.SetActive(false);
            }
            if (timeToAppear <= 0f)
            {
                collided = false;
                Navigator.Entity.SetInteractionState(true);
				spriteRenderer[0].gameObject.SetActive(true);
            }
            
        }

        public float wanderSpeed = 10f;
        public float chaseSpeed = 18f; // i added this if your npc chases the player

        public bool collided;
        public float timeToAppear = 15f;

        [SerializeField]
        public PropagatedAudioManager pam;
    }
}
