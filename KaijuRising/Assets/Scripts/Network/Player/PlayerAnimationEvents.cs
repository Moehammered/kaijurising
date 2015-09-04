using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAnimationEvents : NetworkBehaviour 
{

	/*
	 * Must go onto player object which has an Animator component.
	 */ 

	public GameObject dustImpact;
	public GameObject fractureImpact;

	private void Start()
	{

	}

	[Command]
	public void CmdendOfSpecialAttack()
	{
		GameObject instance = null;
		instance = (GameObject)Instantiate (dustImpact, transform.position + new Vector3(0,-40,0), dustImpact.transform.rotation);
		NetworkServer.Spawn (instance);
		Destroy (instance, instance.GetComponent<ParticleSystem>().duration + 0.5f);
		instance = (GameObject)Instantiate (fractureImpact, transform.position + new Vector3(0,-40,0), transform.rotation);
		instance.transform.rotation = Quaternion.Euler(90,0,0);
		NetworkServer.Spawn (instance);
		Destroy (instance, 5f);
	}
}
