using UnityEngine;
using UnityEngine.Networking;

using UnityEngine.UI;
using System.Collections;

public class GameTimer : NetworkBehaviour 
{
	public float timeBuffer;
	
	[SyncVar]
	public float currentTime = 5;
	private bool startTime;
	private bool playersConnected = false;
	private GameObject networkManager;
	
	public string timeDownText = "Time Left: ";
	public string timeUpText = "Time Ended";
	public Text timeLeftText;
	
	void Start()
	{
		//networkManager = GameObject.Find("Custom Network Manager");
		startTime = true;
		if (isServer == true)
		{
			StartCoroutine(beginTime());
		}
	}
	
	void FixedUpdate () 
	{
		/*	Custom manager doesnt exist in online scene as of yet

		if(networkManager.GetComponent<CustomNetworkManager>().numPlayers >= 2 && playersConnected == false)
		{
			playersConnected = true;
		}
		*/
		if(startTime == true)
		{
			incrementTime();
			gameTimeOver();
		}
	}
	
	private IEnumerator beginTime()
	{
		yield return new WaitForSeconds(timeBuffer);
		startTime = true;
	}
	
	private void incrementTime()
	{	
		currentTime -= Time.fixedDeltaTime;
		timeLeftText.text = "" + timeDownText +currentTime.ToString ("F0") ;
	}	
	
	private void gameTimeOver()
	{
		if (currentTime <= 0)
		{
			startTime = false;
			currentTime = 0;
			timeLeftText.text = timeUpText;
			//End condition
		}
	}
}