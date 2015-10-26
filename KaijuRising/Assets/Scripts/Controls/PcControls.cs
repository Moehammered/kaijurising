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
	public TamPlayerAttack playerAttack;
	public KeyBindings keyBindings;
	public float mouseSpeed;
	public float rotationSpeed;

	// need InstantiateSound reference to play sounds
	public Camera playerCam;
	public KaijuSounds sound;
	public bool hasSecondAttack;
	private bool isTurning;
	private int attackCount;
	
	private void FixedUpdate()
	{
		if (isLocalPlayer) 
		{
			keyboardInput();
			mouseInput();
		}
	}

	public void keyboardInput()
	{
		if (!playerAnimations.isTakingDamage())
		{
			direction = Vector3.zero;
			if (!playerAnimations.isAttacking())
			{
				attackCount = 0;
				if (Input.GetKey (keyBindings.forward)) 
				{
					Vector3 lookDirection = (transform.position - playerCam.transform.position);
					lookDirection.y = transform.forward.y;
					transform.LookAt (transform.position + lookDirection.normalized);
					direction += lookDirection.normalized;
					playerAnimations.playWalk();
					direction += transform.forward;	
				}
				else if (Input.GetKey (keyBindings.back)) 
				{
					playerAnimations.playBackwardsWalk();
					direction += -transform.forward;
				} 
				 
				if (Input.GetKey (keyBindings.right))
				{
					playerAnimations.playWalk();
					transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime,0));
					isTurning = true;
					//direction += transform.right;
				} 
				else if (Input.GetKey (keyBindings.left)) 
				{
					playerAnimations.playWalk();
					transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime,0));
					isTurning = true;
					//direction += -transform.right;
				}
				else
				{
					isTurning = false;
				}
				
				if (direction != Vector3.zero)
				{
					move(direction,speed);
				}
				else if (isTurning == false)
				{
					playerAnimations.stopWalk();
				}
				mouseInput();
				
				if (Input.GetKeyDown(keyBindings.attack))
				{
					//attack increase attack count;
					playerAttack.normalAttack();
					playerAnimations.stopWalk();
					playerAnimations.playPrimaryAttack();
					attackCount++;
				}
			}
			else if (attackCount < 2)
			{
				if (Input.GetKeyDown(keyBindings.attack))
				{
					if (hasSecondAttack)
					{
						playerAnimations.playSecondAttack(true);
					}
					else
					{
						playerAnimations.playBackwardsPrimaryAttack();
					}
					playerAttack.normalAttack();
					attackCount++;
				}
			}
		}
		// Start and stop sounds are specific to key down and up.
	}
	
	public void mouseInput()
	{
		float mouseX = Input.GetAxis("Mouse X");
		
		//mainCamera.transform.RotateAround(transform.position,new Vector3(0,1,0), mouseX);
		playerCam.transform.RotateAround(transform.position, Vector3.up, mouseX * mouseSpeed * Time.deltaTime);
		//transform.Rotate(new Vector3(0, mouseX * mouseSpeed * Time.deltaTime, 0));
	}
	

}