using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Grow : NetworkBehaviour {

	/*
	 * This goes onto a pickup object and provides a growth power up.
	 * This is currently permanent, but a timer can be included.
	 */ 

	private void OnTriggerEnter(Collider other)
	{
		if(!isServer)
			return;

		if(other.gameObject.tag == "Player")
		{
			other.gameObject.transform.localScale += new Vector3(2,2,2);
		}

		Destroy (gameObject);
		NetworkServer.Destroy (gameObject);
	}
}