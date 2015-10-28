using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour {

	public KaijuSounds sounds;
	public KaijuAnimations playerAnimations;
	
	void OnTriggerEnter(Collider col) 
	{
		if (col.gameObject.tag == "Ground" && !playerAnimations.isAttacking() && !playerAnimations.isTakingDamage() && !playerAnimations.isIdle) 
		{
			sounds.CmdPlayOnServer("SoundWalk");
		}
	}
}
