using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyBuilding : NetworkBehaviour {

	/*
	 * Goes onto a normal undestroyed building.
	 */ 

	public GameObject[] fragments; // The seperate fragments that make up the building.
	public Vector3 spawnOffset; // Default 0,0,0. Use if spawned fragments do not fully line up with unbroken building.
	
	private void OnCollisionEnter(Collision other)
	{
		if(!isServer) // If not server, do not continue.
			return;

		if(other.gameObject.tag == "Player")
		{
			Destroy (gameObject); // Destroy normal unfractured building.
			NetworkServer.Destroy (gameObject);
			
			for(int i = 0; i < fragments.Length; i++) // Spawn all the fragments.
			{
				GameObject go = Instantiate (fragments[i], transform.position + fragments[i].transform.position + spawnOffset, fragments[i].transform.rotation) as GameObject;
				NetworkServer.Spawn (go);
				go.GetComponent<Rigidbody>().AddForce (other.contacts[0].normal * 5, ForceMode.Impulse); // Adds force in the direction the player was moving.
			}
		}
	}
}