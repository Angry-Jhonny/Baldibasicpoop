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
            behaviorStateMachine.ChangeState(new MisterBenz_Wander(this));
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Idle"); // collects the sprite inside the assetmanager on the plugin
        }

        public void Explode() // just added this cuz your npc will explode for s
        {
        }

        public float wanderSpeed = 10f;
        public float chaseSpeed = 18f; // i added this if your npc chases the player
    }
}
