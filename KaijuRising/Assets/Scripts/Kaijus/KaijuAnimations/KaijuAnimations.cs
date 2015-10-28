using UnityEngine;
using System.Collections;

[System.Serializable]
public struct TimedAnimations
{
	public string animName;
	public float duration;
}

[System.Serializable]
public struct AttackAnimations
{
	public int attackIndex;
	public float duration;
};

public class KaijuAnimations : BaseAnimations 
{
	//attack, special, death. takedaamge, idle, walk
	public TimedAnimations primaryAttack, specialAttack, takeDamage;
	[Header("Parameters for second primary attacks")]
	public string attackType = "attackType";
	public AttackAnimations backwardsPrimary, backwardsSecondary, secondAttack;
	[Header("names of animation parameters")]
	public string walk = "canWalk";
	public string death = "canDie";
	public string takeDamageName = "TakeDamage";
	public string walkType = "walkType";

	public bool isIdle = true;

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
			playAttack(1);
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
	
	public void playPrimaryAttack()
	{
		netAnim.animator.SetInteger(attackType, 1);
		setAnimatorParameters(primaryAttack.animName, false);
		attackTimedAnimations(primaryAttack.animName, primaryAttack.duration);
	}
	
	public void playSecondAttack()
	{
		netAnim.animator.SetInteger(attackType, 2);
		setAnimatorParameters(primaryAttack.animName, false);
		attackTimedAnimations(primaryAttack.animName, primaryAttack.duration);
	}
	
	public float playPrimaryAttack(bool isSecond)
	{
		if (isSecond)
		{
			return playAttack(1);
		}
		else
		{
			playPrimaryAttack();
		}
		return 0;
	}
	
	public float playSecondAttack(bool isSecond)
	{
		if (isSecond)
		{
			return playAttack(2);
		}
		else
		{
			playSecondAttack();
		}
		return 0;
	}
	
	public float playBackwardsPrimaryAttack()
	{
		return playAttack(-1);
	}
	
	public float playBackwardsSecondAttack()
	{
		return playAttack(-2);
	}
	
	private float playAttack(int attackingType)
	{
		float timer = attackTime();
		StartCoroutine(changeAttackType(attackingType));
		if (attackingType == backwardsPrimary.attackIndex)
		{
			increaseAttackTimer(backwardsPrimary.duration);
		}
		else if (attackingType == backwardsSecondary.attackIndex)
		{
			increaseAttackTimer(backwardsSecondary.duration);
		}
		else if (attackingType == secondAttack.attackIndex)
		{
			increaseAttackTimer(secondAttack.duration);
		}
		else
		{
			increaseAttackTimer(primaryAttack.duration);
		}
		return timer;
	}
	
	private IEnumerator changeAttackType(int attackIndex)
	{
		float timer = attackTime();
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		netAnim.animator.SetInteger(attackType, attackIndex);
	}
	
	public void playWalk()
	{
		isIdle = false;
		netAnim.animator.SetInteger(walkType, 1);
		setAnimatorParameters(walk, true);
	}
	
	public void playBackwardsWalk()
	{
		isIdle = false;
		netAnim.animator.SetInteger(walkType, -1);
		setAnimatorParameters(walk, true);
	}
	
	public void stopWalk()
	{
		isIdle = true;
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
		isIdle = true;
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
		bool isAttack = netAnim.animator.GetBool(primaryAttack.animName);
		return isAttack;
	}
	
	public bool isSpecial()
	{
		return netAnim.animator.GetBool(specialAttack.animName);
	}
	
	public bool isTakingDamage()
	{
		bool isTakeDam = netAnim.animator.GetBool(takeDamage.animName);
		return isTakeDam;
	}
}