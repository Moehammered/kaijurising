using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour 
{
	/*
	 * Goes onto player prefab, functions are called by UI Attack Buttons.
	 */ 

	public NetworkAnimator anim;
	
	public void standardAttack()
	{
		if(!anim.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
		{
			if(gameObject.name.Contains("Rex"))
			{
				tailSwing();
			}
			else
			{
				anim.SetTrigger("standardAttack");
			}
		}
	}

	public void specialAttack()
	{
		if(!anim.animator.GetCurrentAnimatorStateInfo(0).IsTag ("Attack"))
		{
			anim.SetTrigger ("specialAttack");
		}
	}

	/*
	 * This function has been created specifically for the Rex kaiju.
	 */ 
	private void tailSwing()
	{
		if(Random.Range(0, 2) == 0)
		{
			anim.SetTrigger ("attackLeft");
		}
		else
		{
			anim.SetTrigger ("attackRight");
		}
	}
}