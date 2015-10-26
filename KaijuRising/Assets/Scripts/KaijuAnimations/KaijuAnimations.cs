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
	public TimedAnimations primaryAttack, specialAttack, takeDamage;
	public string walk = "canWalk";
	public string death = "canDie";
	public string takeDamageName = "TakeDamage";
	
	[Header("Parameters for second primary attacks")]
	public string attackType = "attackType";
	public float secondAttackDuration = 0.5f;
	// Use this for initialization
	private void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	//	debugControls();
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
		
		if (Input.GetKeyDown(KeyCode.Q)) //&& !isTakingDamage())
		{
			playTakeDamage();
		}
		
		if (Input.GetKeyDown(KeyCode.Space) && (!isAttacking() || !isTakingDamage()))
		{
			playAttack();
		}
		
		if (Input.GetKeyDown(KeyCode.A) && (!isAttacking() || !isTakingDamage()))
		{
			playSpecial();
		}
		
		if (Input.GetKeyDown(KeyCode.D))
		{
			playDeath();
		}
		
		if (Input.GetKeyDown(KeyCode.E))
		{
			playIdle();
		}
	}
	
	public void playAttack()
	{
		setAnimatorParameters(primaryAttack.animName, false);
		timedAnimations(primaryAttack.animName, primaryAttack.duration);
	}
	
	public void playAttack(int attackingType)
	{
		setAnimatorParameters(primaryAttack.animName, false);
		netAnim.animator.SetInteger(attackType, attackingType);
		switch(attackingType)
		{
		case 2:
			timedAnimations(primaryAttack.animName, secondAttackDuration);
			break;
		default:
			timedAnimations(primaryAttack.animName, primaryAttack.duration);
			break;
		}
	}
	
	public void playWalk()
	{
		setAnimatorParameters(walk, true);
	}
	
	public void stopWalk()
	{
		setAnimatorParameters(walk, false);
	}
	
	public void playDeath()
	{
		setAnimatorParameters(walk, false);
		setAnimatorParameters(primaryAttack.animName, false);
		setAnimatorParameters(specialAttack.animName, false);
		setAnimatorParameters(takeDamage.animName, false);
		setAnimatorParameters(death, true);
	}
	
	public void stopDeath()
	{
		setAnimatorParameters(death, false);
	}
	
	public void playTakeDamage()
	{
		setAnimatorParameters(walk, false);
		setAnimatorParameters(primaryAttack.animName, false);
		setAnimatorParameters(specialAttack.animName, false);
		setAnimatorParameters(takeDamage.animName, false);
		if (animCorutine != null)
		{
			StopCoroutine(animCorutine);
			netAnim.animator.Play(takeDamageName, 0, 0f);
		}
		timedAnimations(takeDamage.animName, takeDamage.duration);
	}
	
	public void playSpecial()
	{
		timedAnimations(specialAttack.animName, specialAttack.duration);
	}
	
	public void playIdle()
	{
		setAnimatorParameters(walk, false);
		setAnimatorParameters(death, false);
		setAnimatorParameters(primaryAttack.animName, false);
		setAnimatorParameters(specialAttack.animName, false);
		setAnimatorParameters(takeDamage.animName, false);
		if (animCorutine != null)
		{
			StopCoroutine(animCorutine);
		}
	}
	
	public bool isAttacking()
	{
		bool isAttack = (netAnim.animator.GetBool(primaryAttack.animName) || netAnim.animator.GetBool(specialAttack.animName));
		return isAttack;
	}
	
	public bool isTakingDamage()
	{
		bool isTakeDam = netAnim.animator.GetBool(takeDamage.animName);
		return isTakeDam;
	}
}