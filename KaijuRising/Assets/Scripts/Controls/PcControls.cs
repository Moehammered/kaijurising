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
	public KeyCode primaryAttack;
	public KeyCode specialAttack;
}

public class PcControls : AbstractMover
{
	public KaijuAnimations playerAnimations;
	public TamPlayerAttack playerAttack;
	public KeyBindings keyBindings;
	public float mouseSpeed;
	public float rotationSpeed;

	// need InstantiateSound reference to play sounds
	public GameObject playerCam;
	public bool hasSecondAttack;
	private bool isTurning;
	private int attackCount;
	public bool isFalsol = false;
	public KaijuSounds sounds;
	public float attackDelay; 
	private float mouseSens = 20f;
	
	private void Start()
	{
		if (isLocalPlayer)
		{
			mouseSens = PlayerPrefs.GetFloat("MouseSensivity", 20f);
		}
	}
	
	private void Update()
	{
		if (isLocalPlayer) 
		{
			keyboardInput();
		}
	}

	private void LateUpdate()
	{
		if(!isLocalPlayer)
			return;

		mouseInput();
	}

	public void keyboardInput()
	{
		if (!playerAnimations.isTakingDamage() && !playerAnimations.isSpecial())
		{
			direction = Vector3.zero;
			if (!playerAnimations.isAttacking())
			{

				// add lookdirections, calculat at end.
				attackCount = 0;
				if (Input.GetKey (keyBindings.forward)) 
				{
					Vector3 lookDirection = (transform.position - playerCam.transform.GetChild (0).transform.position);
					lookDirection.y = transform.forward.y;
					direction += lookDirection.normalized;
					playerAnimations.playWalk();
				}
				if (Input.GetKey (keyBindings.back)) 
				{
					Vector3 lookDirection = -(transform.position - playerCam.transform.GetChild (0).transform.position);
					lookDirection.y = transform.forward.y;
					direction += lookDirection.normalized;
					playerAnimations.playBackwardsWalk();
				} 
				 
				if (Input.GetKey (keyBindings.right))
				{
					Vector3 lookDirection = playerCam.transform.right;
					lookDirection.y = transform.forward.y;
					direction += lookDirection.normalized;
					playerAnimations.playWalk();
				} 
				if (Input.GetKey (keyBindings.left)) 
				{
					Vector3 lookDirection = -playerCam.transform.right;
					lookDirection.y = transform.forward.y; 
					direction += lookDirection.normalized;
					playerAnimations.playWalk();
				}
				else
				{
					isTurning = false;
				}
				
				if (direction != Vector3.zero)
				{
					Quaternion requiredRotation = Quaternion.LookRotation ((transform.position + direction) - transform.position);
					transform.rotation = Quaternion.Slerp(transform.rotation, requiredRotation, Time.deltaTime * 2.5f); 
					move(direction,speed);
				}
				else if (isTurning == false)
				{
					playerAnimations.stopWalk();
				}
				mouseInput();
				
				if (Input.GetKeyDown(keyBindings.primaryAttack))
				{
					//attack increase attack count;
					playerAttack.normalAttack();
					playerAnimations.stopWalk();
					sounds.CmdPlayOnServer ("swooshSound1");
					playerAnimations.playPrimaryAttack();
					attackCount++;
				}
			}
			else if (attackCount < 2 && !isFalsol)
			{
				if (Input.GetKeyDown(keyBindings.primaryAttack))
				{
					float timed = 0;
					if (hasSecondAttack)
					{
						StartCoroutine(secondAttackDelay());
						timed = playerAnimations.playSecondAttack(true);
					}
					else
					{
						StartCoroutine(secondAttackDelay());
						timed = playerAnimations.playBackwardsPrimaryAttack();
					}
					playerAttack.timedNormalAttack(timed);
					attackCount++;
				}
			}
		}
		
		if (Input.GetKeyDown(keyBindings.specialAttack) && playerAttack.checkCanSpecial() == true)
		{
			playerAnimations.playSpecial();
			playerAttack.specialAttack();
		}
		// Start and stop sounds are specific to key down and up.
	}

	private IEnumerator secondAttackDelay() 
	{
		float time = attackDelay;
		while(time > 0) 
		{
			time -= Time.deltaTime;
			yield return null;
		}
		sounds.CmdPlayOnServer ("swooshSound2");
	}

	public void mouseInput()
	{
		float mouseX = Input.GetAxis("Mouse X");
		
		//mainCamera.transform.RotateAround(transform.position,new Vector3(0,1,0), mouseX);
		playerCam.transform.RotateAround(transform.position, Vector3.up, mouseX * mouseSens * Time.deltaTime);
		//transform.Rotate(new Vector3(0, mouseX * mouseSpeed * Time.deltaTime, 0));
	}
	

}