using Baldibasicpoop.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Baldibasicpoop.CustomMono
{
    internal class NunGauge : MonoBehaviour
    {
        public HudGauge gauge;
        public Entity ent;

        private ActivityModifier actMod;
        private MovementModifier moveMod = new MovementModifier(Vector3.zero, 0.25f);

        public float time;
        public bool isPlayer;

        void Start()
        {
            actMod = ent.gameObject.GetComponent<ActivityModifier>();
            actMod.moveMods.Add(moveMod);
            StartCoroutine(Timer(10));
        }

        private IEnumerator Timer(float timer)
        {
            time = timer;

            while (time > 0f)
            {
                time -= Time.deltaTime * ent.Ec.EnvironmentTimeScale;
                if (gauge != null && isPlayer)
                {
                    gauge.SetValue(10, time);
                }

                yield return null;
            }

            actMod.moveMods.Remove(moveMod);

            if (gauge != null && isPlayer)
            {
                gauge.Deactivate();
            }
        }
    }
}
