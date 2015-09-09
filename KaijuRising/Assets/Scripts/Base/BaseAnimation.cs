using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BaseAnimation : NetworkBehaviour {

	public NetworkAnimator networkAnimator;
	
	public override void PreStartClient ()
	{
		base.PreStartClient ();
		networkAnimator.SetParameterAutoSend(0, true);
	}	
	
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		networkAnimator.SetParameterAutoSend(0, true);
	}
	
	public virtual void setAnimationBool(string animationParameter, bool state)
	{
		networkAnimator.animator.SetBool (animationParameter, state);
	}
}
