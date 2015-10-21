using UnityEngine;
using System.Collections;

[System.Serializable]
public struct TimedAnimations
{
	public string animName;
	public float duration;
}

public class KaijuAnimations : BaseAnimations 
{
	//attack, special, death. takedaamge, idle, walk
	public TimedAnimations primaryAttack, takeDamage;
	public string walk = "canWalk";
	
	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		debugControls();
	}
	
	private void debugControls()
	{
		if (Input.GetKey(KeyCode.W))
		{
			playWalk();
		}
		else
		{
			stopWalk();
		}
		
		if (Input.GetKeyDown(KeyCode.T) && (!isAttacking() || !isTakingDamage()))
		{
			playTakeDamage();
		}
		
		if (Input.GetKeyDown(KeyCode.Space) && (!isAttacking() || !isTakingDamage()))
		{
			playAttack();
		}
		
	}
	
	public void playAttack()
	{
		timedAnimations(primaryAttack.animName, primaryAttack.duration);
	}
	
	public void playWalk()
	{
		setAnimatorParameters(walk, true);
	}
	
	public void stopWalk()
	{
		setAnimatorParameters(walk, false);
	}
	
//	public void playDeath()
//	{
//		timedAnimations(death.animName, death.duration);
//	}
	
	public void playTakeDamage()
	{
		playIdle();
		timedAnimations(takeDamage.animName, takeDamage.duration);
	}

	public void playIdle()
	{
		setAnimatorParameters(walk, false);
//		setAnimatorParameters(death.animName, false);
		setAnimatorParameters(primaryAttack.animName, false);
		setAnimatorParameters(takeDamage.animName, false);
	}
	
	public bool isAttacking()
	{
		bool isAttack = netAnim.animator.GetBool(primaryAttack.animName);
		return isAttack;
	}
	
	public bool isTakingDamage()
	{
		bool isTakeDam = netAnim.animator.GetBool(takeDamage.animName);
		return isTakeDam;
	}
}