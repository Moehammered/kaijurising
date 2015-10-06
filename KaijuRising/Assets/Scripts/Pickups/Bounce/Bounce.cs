using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bounce : TimedPickup {

	/*
	 * Goes onto a pickup, lets the player bounce around the environment.
	 */ 
	
	private Rigidbody playerRigidbody;
	public List<Vector3> lastPositions;

	private void FixedUpdate()
	{
		if(isActive)
		{
			calculateVelocity();
		}
	}
	
	private void calculateVelocity()
	{
		/*
		 * For some reason because the player's position is changed with rigidbody.MovePosition, when a collision occurs
		 * the velocity of the rigidbody component returns 0,0,0. Because of this, I have to calculate velocity manually.
		 * I also make sure to not just keep track of the last position, but of the last two.
		 * The reason for this is that the last position may be saved at the moment of collision, which will then
		 * cause issues in calculating a proper velocity, thus affecting the bounce.
		 * So I use the 2nd last saved position to calculate velocity.
		 * I also make sure to delete the list if it goes beyond a size of 2, as I do not need more positions.
		 */ 

		lastPositions.Insert(0, playerRigidbody.transform.position);

		if(lastPositions.Count > 2)
		{
			lastPositions.RemoveAt(2);
		}
	}

	private Vector3 getVelocity()
	{
		return (playerRigidbody.transform.position - lastPositions[1]) / Time.deltaTime;
	}

	public void OnPlayerCollison(Collision other)
	{
		Vector3 velocity = getVelocity();
		Vector3 reflectedVelocity = Vector3.Reflect (velocity, other.contacts[0].normal);
		reflectedVelocity.Normalize();
		playerRigidbody.velocity = reflectedVelocity * 500;
	}

	public override void activatePickup (GameObject target)
	{
		base.activatePickup (target);
		playerRigidbody = target.GetComponent<Rigidbody>();
		playerRigidbody.gameObject.AddComponent<BounceCollisionChecker>().assignPickupReference (gameObject);
		lastPositions = new List<Vector3>();
	}

	protected override void finishEffect ()
	{
		base.finishEffect ();
		Destroy (playerRigidbody.gameObject.GetComponent<BounceCollisionChecker>());
		Destroy (gameObject);
	}
}