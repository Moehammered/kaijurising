using UnityEngine;
using System.Collections;

public class KaijuAttackSound : MonoBehaviour {

	public KaijuSounds sounds;
	public KaijuAnimations anim;
	private int value = 1;
	
	void OnTriggerEnter(Collider col) 
	{
		if(col.gameObject.tag == "50m" || col.gameObject.tag == "100m" || col.gameObject.tag == "150m" || col.gameObject.tag == "kappa") 
		{
			if(anim.isAttacking()) 
			{
				chooseSound();
			}
		}
	}

	public void chooseSound() 
	{
		if(value % 2 == 0) 
		{
			sounds.CmdPlayOnServer("groundslam1");
			value += 1;
		}
		else if(value % 2 > 0) 
		{
			sounds.CmdPlayOnServer("groundslam2");
			value += 1;
		}
	}
}
