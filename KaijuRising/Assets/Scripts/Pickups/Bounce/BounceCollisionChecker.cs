using UnityEngine;
using System.Collections;

public class BounceCollisionChecker : MonoBehaviour {

	/*
	 * Because of the way pickups have been setup, this script must be used.
	 * Bounce.cs is on a pickup which is childed to the player
	 * It needs to know when the player has collided.
	 * So when the Bounce.cs pickup is activated, it adds this script to the player to check for collisions.
	 * When a collision is detected, it tells the Bounce pickup & provides collision info.
	 * The bounce pickup then calculates the bounce vector, and applies that new velocity to the player's rigidbody component.
	 * 
	 * NOTE: This script should not be added anywhere. Bounce.cs will automatically use this.
	 */ 

	public Bounce bouncePickupReference;

	public void assignPickupReference(GameObject reference)
	{
		bouncePickupReference = reference.GetComponent<Bounce>();
	}

	private void OnCollisionEnter(Collision other)
	{
		bouncePickupReference.OnPlayerCollison(other);
	}
}