using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour 
{

	public NetworkAnimator anim;

	public void tailSwing()
	{

		if(anim.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
		{

		}
		else
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

	public void specialAttack()
	{
		if(anim.animator.GetCurrentAnimatorStateInfo(0).IsTag ("Attack"))
		{

		}
		else
		{
			anim.SetTrigger ("specialAttack");
		}
	}
}