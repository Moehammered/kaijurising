using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BaseAnimations : NetworkBehaviour 
{	
	public NetworkAnimator netAnim;
	
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
	
	protected virtual void setAnimatorParameters(string name, bool state)
	{
		netAnim.animator.SetBool(name, state);
	}
	
	protected virtual void timedAnimations(string name, float duration)
	{
		StartCoroutine(setTimedAnimation(name, duration));
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
}