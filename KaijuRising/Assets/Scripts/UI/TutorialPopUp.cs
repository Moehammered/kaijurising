using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TutorialPopUp : NetworkBehaviour 
{
	public Text destroyBuild;
	public Text killKaiju;
	public Text highestScore;

	public float delayAppear;

	public void Start()
	{
		if(!isLocalPlayer)
		{
			return;
		}
		StartCoroutine(showDestroyBuilding(delayAppear));
	}

	private IEnumerator showDestroyBuilding(float duration)
	{
		destroyBuild.gameObject.SetActive(true);
		float timer = duration;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		StartCoroutine(killKaijuDis(delayAppear));
	}

	private IEnumerator killKaijuDis(float duration)
	{
		killKaiju.gameObject.SetActive(true);
		float timer = duration;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		StartCoroutine(highestScoreDis(delayAppear));
	}

	private IEnumerator highestScoreDis(float duration)
	{
		highestScore.gameObject.SetActive(true);
		float timer = duration;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		destroyBuild.gameObject.SetActive(false);
		killKaiju.gameObject.SetActive(false);
		highestScore.gameObject.SetActive(false);
	}
}
