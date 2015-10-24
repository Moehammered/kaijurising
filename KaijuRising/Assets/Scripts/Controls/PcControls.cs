using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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
	public KaijuAnimations playerAnimations;

	public KeyBindings keyBindings;
	public float mouseSpeed;

	// need InstantiateSound reference to play sounds
	public KaijuSounds sound;

	void Update()
	{
		if (isLocalPlayer) 
		{
			keyboardInput();
			mouseInput();
		}
	}

	public void keyboardInput()
	{
		direction = Vector3.zero;
		if (!playerAnimations.isAttacking())
		{
			if (Input.GetKey (keyBindings.forward)) 
			{
				direction += transform.forward;	
			}
			else if (Input.GetKey (keyBindings.back)) 
			{
				direction += -transform.forward;
			} 
			 
			if (Input.GetKey (keyBindings.right))
			{
				direction += transform.right;
			} 
			else if (Input.GetKey (keyBindings.left)) 
			{
				direction += -transform.right;
			}
			
			if (direction != Vector3.zero)
			{
				playerAnimations.playWalk();
				move(direction,speed);
			}
			else
			{
				playerAnimations.stopWalk();
			}
		}
		// Start and stop sounds are specific to key down and up.
		if (Input.GetKeyDown (keyBindings.forward)) 
		{
			sound.CmdPlayOnServer();
		}
		else if(Input.GetKeyDown (keyBindings.back)) 
		{
			sound.CmdPlayOnServer();
		} 
		else if(Input.GetKeyDown (keyBindings.left)) 
		{
			sound.CmdPlayOnServer();
		}
		else if(Input.GetKeyDown (keyBindings.right)) 
		{
			sound.CmdPlayOnServer();
		} 
		if (Input.GetKeyUp (keyBindings.forward)) 
		{
			sound.CmdStopOnServer();
		}
		else if(Input.GetKeyUp (keyBindings.back)) 
		{
			sound.CmdStopOnServer();
		}
		else if(Input.GetKeyUp (keyBindings.left)) 
		{
			sound.CmdStopOnServer();
		}
		else if(Input.GetKeyUp (keyBindings.right)) 
		{
			sound.CmdStopOnServer();
		}
	}
	
	public void mouseInput()
	{
		float mouseX = Input.GetAxis("Mouse X");
		
		//mainCamera.transform.RotateAround(transform.position,new Vector3(0,1,0), mouseX);
		transform.Rotate(new Vector3(0,mouseX,0));
	}
	

}