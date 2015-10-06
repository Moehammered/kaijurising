using UnityEngine;
using System.Collections;

public class PlayerJump : AbstractMover {

	public KeyCode jumpKey = KeyCode.Space;

	private void Update()
	{
		checkKeys();
	}

	private void checkKeys()
	{
		if(Input.GetKeyDown (jumpKey))
		{
			rigidbody.AddForce (Vector3.up * speed, ForceMode.Impulse);
		}
	}
}