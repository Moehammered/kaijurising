using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Speedboost : AbstractPickup {

	/*
	 * This goes onto a pickup object and provides a speed power up.
	 * This is currently permanent, but a timer can be included.
	 */ 

	public override void activatePickup (GameObject target)
	{
		target.GetComponent<JoystickMovement>().speed += 0.5f;
		Destroy (gameObject);
		NetworkServer.Destroy (gameObject);
	}
}