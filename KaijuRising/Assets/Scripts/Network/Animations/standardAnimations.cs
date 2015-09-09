using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class standardAnimations : AbstractPlayerAnimations 
{
	public override void playerAttack ()
	{
		if(!networkAnimator.animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
		{

			int attackNo = Random.Range(0, attacks.Length);
			networkAnimator.SetTrigger (attacks[attackNo]);
		}
	}
	
	public override void playerSpecialAttack ()
	{
		if(!networkAnimator.animator.GetCurrentAnimatorStateInfo(0).IsTag ("attack"))
		{
			networkAnimator.SetTrigger (specialAttack);
		}	
	}
	
	public override void playerWalking (bool isWalking)
	{
		setAnimationBool(walking, isWalking);
	}
}
