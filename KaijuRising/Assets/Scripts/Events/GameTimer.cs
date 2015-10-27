using UnityEngine;
using UnityEngine.Networking;

using UnityEngine.UI;
using System.Collections;

public class GameTimer : NetworkBehaviour 
{
	public int connectedPlayers = 0;
	public float timeBuffer;
	
	[SyncVar]
	public float currentTime = 5;
	private bool startCounting;
	private bool playersConnected = false;
	private GameObject networkManager;
	
	public string timeDownText = "Time Left: ";
	public string timeUpText = "Time Ended";
	public Text timeLeftText;
	
	public void addPlayer()
	{
		connectedPlayers++;

		if(connectedPlayers == 2)
		{
			timeLeftText.text = "" + timeDownText + currentTime.ToString ("F0");
			StartCoroutine(beginTime());
		}
	}

//	private void Start()
//	{
//		//networkManager = GameObject.Find("Custom Network Manager");
//
//		if (isServer == true)
//		{
//
//			StartCoroutine(beginTime());
//		}
//	}

	private void FixedUpdate () 
	{
		if(!isServer)
			return;

		/*	Custom manager doesnt exist in online scene as of yet

		if(networkManager.GetComponent<CustomNetworkManager>().numPlayers >= 2 && playersConnected == false)
		{
			playersConnected = true;
		}
		*/
		if(startCounting == true)
		{
			incrementTime();
			gameTimeOver();
		}
	}
	
	private IEnumerator beginTime()
	{
		yield return new WaitForSeconds(timeBuffer);
		startCounting = true;
	}
	
	private void incrementTime()
	{	
		currentTime -= Time.fixedDeltaTime;
		timeLeftText.text = "" + timeDownText + currentTime.ToString ("F0") ;
		Rpc_updateTime();
	}	

	[ClientRpc]
	private void Rpc_updateTime()
	{
		timeLeftText.text = "" + timeDownText + currentTime.ToString ("F0") ;
	}

	private void gameTimeOver()
	{
		if (currentTime <= 0)
		{
			startCounting = false;
			currentTime = 0;
			timeLeftText.text = timeUpText;
			GameObject.FindObjectOfType<ScoreSystem>().endGame();
			Destroy (gameObject);
			//End condition
		}
	}
}