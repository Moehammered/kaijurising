﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyableSound : NetworkBehaviour {

	public AudioSource audioSource;
	private float time;
	
	public float seconds;

	void Start() 
	{
		time = 2f;
		StartCoroutine (destroyClip ());
	} 

	private IEnumerator destroyClip() 
	{
		while (time + 1f > 0)
		{
			time -= Time.deltaTime;
			yield return null; //wait for a frame
		}
		CmdEndEvent();
	}

	private void CmdEndEvent() 
	{
		Destroy (gameObject, 0);
		NetworkServer.Destroy (gameObject);
	}
}
