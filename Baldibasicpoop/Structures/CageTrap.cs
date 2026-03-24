using Baldibasicpoop.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Baldibasicpoop.Structures
{
    public class Structure_CageTrap : StructureBuilder
    {
        public CageTrap prefab;
        public override void PostOpenCalcGenerate(LevelGenerator lg, System.Random rng)
        {
            base.PostOpenCalcGenerate(lg, rng);
            List<List<Cell>> halls = lg.Ec.FindHallways();
            for (int i = 0; i < halls.Count; i++)
            {
                halls[i].RemoveAll(x => Directions.OpenDirectionsFromBin(x.ConstBin).Count > 2);
                halls[i].RemoveAll(x => x.shape == TileShapeMask.Open);
            }
            int trapCount = rng.Next(parameters.minMax[0].x, parameters.minMax[0].z);
            for (int i = 0; i < trapCount; i++)
            {
                if (halls.Count == 0)
                {
                    Debug.LogWarning("Couldn't find hall for Cage Traps #" + i + "!");
                    return;
                }
                int chosenHallIndex = rng.Next(0, halls.Count);
                Cell chosenCell = halls[chosenHallIndex][rng.Next(0, halls[chosenHallIndex].Count)];
                halls.RemoveAt(chosenHallIndex);
                Place(chosenCell);
            }
        }

        public override void Load(List<StructureData> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Place(ec.CellFromPosition(data[i].position));
            }
        }

        public void Place(Cell cellAt)
        {
            CageTrap scanner = GameObject.Instantiate<CageTrap>(prefab, cellAt.room.objectObject.transform);
            scanner.transform.position = cellAt.FloorWorldPosition;
            scanner.ec = ec;
            scanner.room = cellAt.room;
            cellAt.HardCoverEntirely();
        }
    }

    public class CageTrap : MonoBehaviour
    {
        public EnvironmentController ec;
        public RoomController room;

        private ActivityModifier currentModifier;

        private MovementModifier moveMod = new MovementModifier(Vector3.zero, 0);

        public AudioManager audMan;

        public SoundObject audTrigger;
        public SoundObject audActivate;
        public SoundObject audUnActivate;
        public GameObject CageGraphic;

        private HudGauge gauge;
        //private Image UIImage;
        public Sprite GaugeIcon;
        public Sprite TrapCover;

        public MeshRenderer buttonMesh;
        public Material pressedMat;
        public Material unpressedMat;

        private float trapTime = 10f;
        private float reloadTime;
        private bool triggered = false;
        private bool powered = true;

        public UsefulHelpers usefulHelpers = BasePlugin.Instance.usefulHelpers;

        private void Update()
        {
            if (room.type == RoomType.Hall)
            {
                if (ec.timeOut)
                {
                    SetPower(false);
                }
            }
            else
            {
                SetPower(room.Powered);
            }
            if (!powered) return;
            if (triggered)
            {
                reloadTime -= Time.deltaTime * ec.EnvironmentTimeScale;
                if (reloadTime <= 0f)
                {
                    buttonMesh.material = unpressedMat;
                    triggered = false;
                    audMan.PlaySingle(audActivate);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!powered) return;
            if (!triggered && (other.tag == "Player" || (other.tag == "NPC" && !other.isTrigger)))
            {
                currentModifier = other.GetComponent<ActivityModifier>();
                currentModifier.moveMods.Add(moveMod);
                StartCoroutine(TrapEntity(trapTime, other.GetComponent<Entity>()));
                triggered = true;
                reloadTime = 30f;
                buttonMesh.material = pressedMat;
                audMan.PlaySingle(audTrigger);
                audMan.PlaySingle(audUnActivate);
            }
        }

        private IEnumerator TrapEntity(float time, Entity ent)
        {
            CageGraphic.transform.position -= new Vector3(0, 9.5f, 0);
            if (ent.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                gauge = Singleton<CoreGameManager>.Instance.GetHud(0).gaugeManager.ActivateNewGauge(GaugeIcon, trapTime);

                //UIImage = usefulHelpers.UI_AddUIOverlay(TrapCover);
            }

            while (time > 0f)
            {
                if (Vector3.Distance(ent.transform.position, transform.position) > 10f)
                {
                    time = 0f;
                }
                else
                {
                    ent.transform.position = new Vector3(transform.position.x, ent.transform.position.y, transform.position.z);
                }
                    time -= Time.deltaTime * ec.EnvironmentTimeScale;
                if (gauge != null)
                {
                    gauge.SetValue(trapTime, time);
                }
                yield return null;
            }
            CageGraphic.transform.position += new Vector3(0, 9.5f, 0);
            currentModifier.moveMods.Remove(moveMod);
            if (gauge != null)
            {
                //GameObject.Destroy(UIImage);
                gauge.Deactivate();
            }
        }

        public void SetPower(bool power)
        {
            if (power == powered) return;
            if (power)
            {
                audMan.PlaySingle(audActivate);
                triggered = false;
                buttonMesh.material = unpressedMat;
            }
            else
            {
                buttonMesh.material = unpressedMat;
            }
        }
    }
}