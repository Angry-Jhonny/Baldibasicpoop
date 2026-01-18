using UnityEngine;
using System.Collections;
using MTM101BaldAPI;

namespace Baldibasicpoop.CustomItems
{
	public class ITM_Shit : Item
	{
		public override bool Use(PlayerManager pm)
		{
			Singleton<CoreGameManager>.Instance.audMan.PlaySingle(eatSound);
			wait = UnityEngine.Random.Range(1, 3);
			atePoop = true;
			return true;
		}
		private void Update()
		{
			if (atePoop)
			{
				if (wait > 0f)
				{
					wait -= Time.deltaTime;
					if (dying)
                    {
						Singleton<CoreGameManager>.Instance.GetCamera(0).camCom.fieldOfView = UnityEngine.Random.Range(70, 100);
						Singleton<CoreGameManager>.Instance.GetCamera(0).billboardCam.fieldOfView = UnityEngine.Random.Range(70, 100);
						Singleton<CoreGameManager>.Instance.GetCamera(0).offestPos = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0, UnityEngine.Random.Range(-0.2f, 0.2f));
					}
				}
				else
				{
					if (!dying)
					{
						dying = true;
						wait = dieSound.soundClip.length;
						Singleton<CoreGameManager>.Instance.audMan.PlaySingle(dieSound);
					}
					else
					{
						Singleton<CoreGameManager>.Instance.SetLives(Singleton<CoreGameManager>.Instance.Lives - 1, true);
						Singleton<CoreGameManager>.Instance.SetAttempts(Singleton<CoreGameManager>.Instance.Attempts + 1);

						Singleton<CoreGameManager>.Instance.GetCamera(0).camCom.fieldOfView = 60;
						Singleton<CoreGameManager>.Instance.GetCamera(0).billboardCam.fieldOfView = 60;
						Singleton<CoreGameManager>.Instance.GetCamera(0).offestPos = Vector3.zero;

						if (Singleton<CoreGameManager>.Instance.Lives <= 0)
                        {
							Singleton<GlobalCam>.Instance.SetListener(val: true);
							Singleton<CoreGameManager>.Instance.ReturnToMenu();
						}
						else
                        {
							Singleton<BaseGameManager>.Instance.RestartLevel();
							UnityEngine.Object.Destroy(base.gameObject);
						}
					}
				}
			}
		}

		private bool atePoop;
		private float wait;
		private bool dying;

		public SoundObject eatSound;
		public SoundObject dieSound;
	}
}
