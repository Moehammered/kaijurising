using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnvironmentFX : MonoBehaviour {

	public DestroyableSound soundSync;

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			print ("Collider");
			soundSync.playClip(0);
		}
	}

}	
