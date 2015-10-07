using UnityEngine;
using System.Collections;

[System.Serializable]
public struct KeyBindings
{
	public KeyCode forward;
	public KeyCode back;
	public KeyCode left;
	public KeyCode right;
	public KeyCode attack;
}

public class PcControls : AbstractMover
{
	public AbstractPlayerAnimations playerAnimations;

	public KeyBindings keyBindings;
	public float mouseSpeed;
	//public Camera mainCamera;

	public DestroyableSound desSound;

	void Update()
	{
		keyboardInput();
		mouseInput();
		attackInput ();
	}

	public void attackInput() 
	{
		if (Input.GetKey (keyBindings.attack)) 
		{
			desSound.playClip(0);
		}
	}

	public void keyboardInput()
	{
		direction = Vector3.zero;

		if (Input.GetKey (keyBindings.forward)) {
			playerAnimations.playerWalking (true);
			direction = transform.forward;
		} else if (Input.GetKey (keyBindings.back)) {
			playerAnimations.playerWalking (true);
			direction = -transform.forward;
		} else if (Input.GetKey (keyBindings.right)) {
			playerAnimations.playerWalking (true);
			direction = transform.right;
		} else if (Input.GetKey (keyBindings.left)) {
			playerAnimations.playerWalking (true);
			direction = -transform.right;
		}
		else 
		{
			playerAnimations.playerWalking(false);
		}
		move(direction,speed);
	}
	
	public void mouseInput()
	{
		float mouseX = Input.GetAxis("Mouse X");
		
		//mainCamera.transform.RotateAround(transform.position,new Vector3(0,1,0), mouseX);
		transform.Rotate(new Vector3(0,mouseX,0));
	}
	

}