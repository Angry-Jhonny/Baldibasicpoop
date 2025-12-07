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
				timeToAppear = 15f;
				pam.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("BEN_Explod"));
                other.GetComponent<Entity>().AddForce(new Force(-Singleton<CoreGameManager>.Instance.GetCamera(pm.playerNumber).transform.forward, 128f, -65.3f));
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
        public float timeToAppear;

        [SerializeField]
        public PropagatedAudioManager pam;
    }
}
