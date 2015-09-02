using UnityEngine;
using System.Collections;

public class PlayerCameraControl : MonoBehaviour {

	/*
	 * This goes onto the player's camera. It handles rotating around the player.
	 */ 

	public float accelerometer;
	private float rotateAmount;
	public Transform player;
	
	private void Update()
	{
		/*
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			rotateAmount = 3;
			transform.RotateAround(player.position, Vector3.up, rotateAmount);
		}

		if(Input.GetKey (KeyCode.RightArrow))
		{
			rotateAmount = -3;
			transform.RotateAround(player.position, Vector3.up, rotateAmount);
		}
*/
		accelerometer = Input.acceleration.x;

		if(Mathf.Abs (accelerometer) > 0.35f)
		{
			transform.RotateAround (player.position, Vector3.up, -accelerometer);
		}

	}
}