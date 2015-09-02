using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	/*
	 * Goes onto player. When server detects building destroyed, increments destroyed count on player.
	 * If buildings destroyed equals the required amount, the server then knows to create a powerup.
	 */ 

	public int buildingsDestroyed; // Amount destroyed so far
	public int buildingsToDestroy; // Quest goal
	public GameObject[] powerUps; // List of possible pickups to spawn
	public bool questEnabled; // Shows if there is an active quest
	public Transform spawnPoint; // A position for the pickup to spawn at.

	private void Start()
	{
		spawnPoint = GameObject.FindGameObjectWithTag("Pickup Point").transform;
	}

	public bool addDestroyedAmount() // When called, it increments the destroyed amount and checks if quest completed.
	{
		buildingsDestroyed++;

		if(buildingsDestroyed == buildingsToDestroy)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public GameObject getPickup(int i) // Gives the pickup object.
	{
		return powerUps[i];
	}

	public bool questState() // Checks to see if quest is enabled.
	{
		return questEnabled;
	}
}