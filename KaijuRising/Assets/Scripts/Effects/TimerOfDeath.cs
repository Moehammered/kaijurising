using UnityEngine;
using System.Collections;

public class TimerOfDeath : MonoBehaviour 
{
	public bool startTicking = true;
	public float timeInSeconds = 5;

	private void Start ()
	{
		errorCheck ();
	}

	// Update is called once per frame
	private void Update () 
	{
		if (startTicking == true) 
		{
			timeInSeconds -= Time.deltaTime;
		}

		if (timeInSeconds <= 0) 
		{
			Destroy (gameObject);
		}
	}

	private void errorCheck ()
	{
		if (timeInSeconds < 0) 
		{
			Debug.LogError ("Time in seconds was below zero and gameobject was destroyed immediately");
		}

		if (timeInSeconds == 0) 
		{
			Debug.LogWarning ("Time in seconds was set to zero.");
		}
	}
}
