using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BaseAnimations : NetworkBehaviour 
{	
	public NetworkAnimator netAnim;
	protected IEnumerator animCorutine;
	private float attackTimer;
	
	public override void PreStartClient ()
	{
		base.PreStartClient ();
		netAnim.SetParameterAutoSend(0, true);
	}
	
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		netAnim.SetParameterAutoSend(0, true);
	}
	
	private void Start()
	{
	
	}
	
	protected void setAnimatorParameters(string name, bool state)
	{
		netAnim.animator.SetBool(name, state);
	}
	
	protected void timedAnimations(string name, float duration)
	{
		animCorutine = setTimedAnimation(name, duration);
		StartCoroutine(animCorutine);
	}
	
	protected void attackTimedAnimations(string name, float duration)
	{
		attackTimer = duration;
		animCorutine = setTimedAnimation(name);
		StartCoroutine(animCorutine);
	}
	
	public void increaseAttackTimer(float amount)
	{
		amount = Mathf.Abs(amount);
		attackTimer += amount;
	}
	
	public float attackTime()
	{
		return attackTimer;
	}
	
	private IEnumerator setTimedAnimation(string name, float duration)
	{
		float timer = duration;
		setAnimatorParameters(name, true);
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		setAnimatorParameters(name, false);
	}
	
	private IEnumerator setTimedAnimation(string name)
	{
		setAnimatorParameters(name, true);
		while(attackTimer > 0)
		{
			attackTimer -= Time.deltaTime;
			yield return null;
		}
		setAnimatorParameters(name, false);
	}
}