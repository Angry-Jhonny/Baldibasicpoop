using MTM101BaldAPI;
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
            spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Idle");
            audMan = base.GetComponent<PropagatedAudioManager>();
		
        }

        public void Explode(Collider other)
        {
            if (!collided)
            {
                timeToDisappear = 5f;
				audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("BEN_Explod"));
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
                        ExplosionObj = Instantiate<GameObject>(explosionPrefab.gameObject);
                        ExplosionObj.transform.SetParent(base.transform);
                        ExplosionObj.transform.position = base.transform.position;
                        //spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Explod");
                        spriteRenderer[0].gameObject.SetActive(false);
                        var offset = (collidedGuy.transform.position - transform.position).normalized;
                        collidedGuy.GetComponent<Entity>().AddForce(new Force(offset, explosionSpeed * 1.9f, -explosionSpeed));
                    }
                    else if (!audMan.AnyAudioIsPlaying)
                    {
                        Object.Destroy(base.gameObject);
                    }
                }
                Navigator.Entity.SetInteractionState(false);
            }
            
        }

        public void TeddehChance()
        {
            int randomChance = Mathf.RoundToInt(UnityEngine.Random.Range(0, teddehChance));
            if (randomChance == 0 && !audMan.QueuedAudioIsPlaying)
            {
                int random = Mathf.RoundToInt(UnityEngine.Random.Range(1, 3));
                audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("BEN_Teddy" + random));
            }
        }

        public void GibberishChance()
        {
            int randomChance = Mathf.RoundToInt(UnityEngine.Random.Range(0, gibberishChance));
            if (randomChance == 0 && !audMan.QueuedAudioIsPlaying)
            {
                int random = Mathf.RoundToInt(UnityEngine.Random.Range(1, 4));
                audMan.PlaySingle(BasePlugin.Instance.assetMan.Get<SoundObject>("BEN_Gibberish" + random));
            }
        }

        public float wanderSpeed = 10f;
        public float chaseSpeed = 18f;

        public float explosionSpeed = 10f;

        public float delay = 0;

        public bool collided;
        public bool exploded;
        public Collider collidedGuy;
        public float timeToDisappear;

        public QuickExplosion explosionPrefab;
        private GameObject ExplosionObj;

        private float teddehChance = 15f;
        private float gibberishChance = 5f;

        [SerializeField]
        public PropagatedAudioManager audMan;
    }

    internal class MisterBenz_WanderTeddy : MisterBenz_StateBase
    {
        private float teddehTime = 1f;

        public MisterBenz_WanderTeddy(MisterBenz benz) : base(benz)
        {
        }

        public override void Enter()
        {
            base.Enter();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
            benz.spriteRenderer[0].sprite = BasePlugin.Instance.assetMan.Get<Sprite>("Benz_Tedi");
        }

        public override void Update()
        {
            base.Update();

            teddehTime -= Time.deltaTime * base.npc.TimeScale;
            if (teddehTime <= 0f)
            {
                benz.TeddehChance();
                teddehTime = 1f;
            }
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }
    }

    internal class MisterBenz_Wander : MisterBenz_StateBase
    {
        private float gibberishTime = 1f;

        public MisterBenz_Wander(MisterBenz benz) : base(benz)
        {
        }

        public override void Enter()
        {
            base.Enter();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void Update()
        {
            base.Update();

            gibberishTime -= Time.deltaTime * base.npc.TimeScale;
            if (gibberishTime <= 0f)
            {
                benz.GibberishChance();
                gibberishTime = 1f;
            }
        }

        public override void DestinationEmpty()
        {
            base.DestinationEmpty();
            base.ChangeNavigationState(new NavigationState_WanderRandom(this.npc, 0));
        }

        public override void OnStateTriggerStay(Entity otherEntity, Collider other, bool validCollision)
        {
            base.OnStateTriggerStay(otherEntity, other, validCollision);
            Entity component = other.GetComponent<Entity>();
            if (component != null && (component.gameObject.layer == LayerMask.NameToLayer("NPCs") || component.gameObject.layer == LayerMask.NameToLayer("Player")))
            {
                if (component.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    PlayerManager plr = other.GetComponent<PlayerManager>();
                    ItemManager itm = plr.itm;
                    if (itm.Has(EnumExtensions.GetFromExtendedName<Items>("Tedi")))
                    {
                        benz.behaviorStateMachine.ChangeState(new MisterBenz_WanderTeddy(benz));
                        itm.Remove(EnumExtensions.GetFromExtendedName<Items>("Tedi"));
                    }
                    else
                    {
                        benz.Explode(other);
                    }
                }
                else
                {
                    benz.Explode(other);
                }
            }
        }
    }
}
