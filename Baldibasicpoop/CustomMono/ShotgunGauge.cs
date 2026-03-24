using Baldibasicpoop.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Baldibasicpoop.CustomMono
{
    internal class ShotgunGauge : MonoBehaviour
    {
        public HudGauge gauge;
        public PlayerManager pm;

        private ActivityModifier actMod;
        private MovementModifier moveMod = new MovementModifier(Vector3.zero, 0.25f);
        private Image image;

        public float time;

        void Start()
        {
            actMod = pm.gameObject.GetComponent<ActivityModifier>();
            actMod.moveMods.Add(moveMod);
            image = BasePlugin.Instance.usefulHelpers.UI_AddUIOverlay(BasePlugin.Instance.assetMan.Get<Sprite>("BloodOverlay"));
            StartCoroutine(Timer(10));
        }

        private IEnumerator Timer(float timer)
        {
            time = timer;

            while (time > 0f)
            {
                time -= Time.deltaTime * pm.ec.EnvironmentTimeScale;
                if (gauge != null)
                {
                    gauge.SetValue(10, time);
                }

                yield return null;
            }

            actMod.moveMods.Remove(moveMod);

            if (gauge != null)
            {
                gauge.Deactivate();
                Object.Destroy(image);
            }
        }
    }
}
