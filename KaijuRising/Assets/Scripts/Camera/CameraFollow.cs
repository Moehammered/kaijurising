using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// This goes onto an object which copies the position of a desired target (but NOT rotation).
	// The purpose of this script is that the player's camera will be a child of the follow object this script is attached to,
	// rather than a child of the object this script is targeting.

	// The reason for all of this is that the player movement and camera are supposed to be seperate...
	// But if the camera was a child of the player, and the player rotated as it was moving in a direction for example,
	// this would then also affect the camera, which we don't want.

	public Transform target;

	private void Update()
	{
		transform.position = target.position;
	}
}