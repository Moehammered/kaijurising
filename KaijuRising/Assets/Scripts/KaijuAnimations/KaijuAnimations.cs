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
	
	public void playPrimaryAttack(bool isSecond)
	{
		if (isSecond)
		{
			playAttack(1);
		}
		else
		{
			playPrimaryAttack();
		}
	}
	
	public void playSecondAttack(bool isSecond)
	{
		if (isSecond)
		{
			playAttack(2);
		}
		else
		{
			playSecondAttack();
		}
	}
	
	public void playBackwardsPrimaryAttack()
	{
		playAttack(-1);
	}
	
	public void playBackwardsSecondAttack()
	{
		playAttack(-2);
	}
	
	private void playAttack(int attackingType)
	{
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
		netAnim.animator.SetInteger(walkType, 1);
		setAnimatorParameters(walk, true);
	}
	
	public void playBackwardsWalk()
	{
		netAnim.animator.SetInteger(walkType, -1);
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