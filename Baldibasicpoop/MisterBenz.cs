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
            base.Navigator.SetSpeed(wanderSpeed);
            base.Navigator.maxSpeed = wanderSpeed;
        }

        public float wanderSpeed = 10f;
    }
}
