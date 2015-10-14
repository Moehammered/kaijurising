using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour 
{
	public float timeLimit;
	public float timeBuffer;
	private float currentTime;
	private bool startTime;
	private bool playersConnected = false;
	private GameObject networkManager;

	void Start()
	{
		networkManager = GameObject.Find("Custom Network Manager");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(networkManager.GetComponent<CustomNetworkManager>().numPlayers >= 2 && playersConnected == false)
		{
			playersConnected = true;
			StartCoroutine(beginTime());
		}
	
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
		currentTime += Time.fixedDeltaTime;
		print(currentTime); 
	}	
	
	private void gameTimeOver()
	{
		if (currentTime >= timeLimit)
		{
			startTime = false;
			currentTime = 0;
			print("Time Ended");
			//End condition
		}
	}
}
