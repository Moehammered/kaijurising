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

			Pickup playerPickupReference = other.gameObject.GetComponent<Pickup>(); // Get pickup script from player

			if(playerPickupReference.questEnabled == true) // Make sure a quest is active.
			{
				if(playerPickupReference.addDestroyedAmount() == true) // Increments destroyed amount and checks if quest completed, if completed, spawn random pickup.
				{
					Transform spawnPoint = playerPickupReference.spawnPoint;
					GameObject instance = (GameObject)Instantiate(playerPickupReference.powerUps[Random.Range (0,playerPickupReference.powerUps.Length)], spawnPoint.position, Quaternion.identity);
					NetworkServer.Spawn (instance); // After instantiating on the server, the clients must also get the object too.
				}
			}
		}
	}
}