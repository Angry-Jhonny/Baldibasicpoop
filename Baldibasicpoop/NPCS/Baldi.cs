using Baldibasicpoop.CustomMono;
using Baldibasicpoop.Events;
using MTM101BaldAPI;
using MTM101BaldAPI.Reflection;
using System.Collections;
using UnityEngine;

namespace Baldibasicpoop.NPCS
{
    public class Baldi_Chase_Shotgun : Baldi_Chase
    {
        private bool SawPlayer;
        private int bullets;
        private ShotgunEvent se;

        private AudioManager audMan;
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private PlayerManager pm;

        public Baldi_Chase_Shotgun(NPC npc, Baldi baldi, ShotgunEvent shotgunEvent) : base(npc, baldi)
        {
            audMan = (AudioManager)baldi.ReflectionGetVariable("audMan");
            spriteRenderer = baldi.spriteRenderer[0];
            se = shotgunEvent;
        }

        public override void Enter()
        {
            pm = Singleton<CoreGameManager>.Instance.GetPlayer(0);
            spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun5");
            animator = (Animator)baldi.ReflectionGetVariable("animator");
            animator.enabled = false;
        }

        public override void Sighted()
        {
            base.Sighted();
            SawPlayer = true;
        }
        public override void PlayerInSight(PlayerManager player)
        {
            base.PlayerInSight(player);
            pm = player;
        }

        public override void Unsighted()
        {
            base.Unsighted();
            SawPlayer = false;
        }

        public override void OnStateTriggerStay(Entity otherEntity, Collider other, bool validCollision)
        {
            base.OnStateTriggerStay(otherEntity, other, validCollision);
        }

        IEnumerator ShootAnim()
        {
            spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun0");
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun1");
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun2");
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun3");
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun4");
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun5");
        }

        protected override void ActivateSlapAnimation()
        {
            if (!SawPlayer || bullets == 0)
            {
                spriteRenderer.sprite = BasePlugin.Instance.assetMan.Get<Sprite>("BAL_Shotgun5");
                audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("BAL_Reload"));
                bullets++;
            }
            else
            {
                audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("BAL_Shoot"));
                bullets--;
                if (bullets <= 0)
                {
                    bullets = 0;
                }
                if (pm)
                {
                    npc.StartCoroutine(ShootAnim());
                    if (!pm.invincible)
                    {
                        if (!se.gauge)
                        {
                            se.gauge = Singleton<CoreGameManager>.Instance.GetHud(0).gaugeManager.ActivateNewGauge(BasePlugin.Instance.assetMan.Get<Sprite>("ShotgunGauge"), 10);
                            ShotgunGauge mono = se.gauge.gameObject.AddComponent<ShotgunGauge>();
                            mono.pm = pm;
                            mono.time = 10;
                            mono.gauge = se.gauge;
                        }
                        else
                        {
                            ShotgunGauge mono = se.gauge.gameObject.GetComponent<ShotgunGauge>();

                            mono.time = 10;
                        }
                    }
                }
            }
        }
    }
}
