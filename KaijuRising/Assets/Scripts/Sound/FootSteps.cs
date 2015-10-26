using UnityEngine;
using System.Collections;

public class FootSteps : MonoBehaviour {

	public KaijuSounds sounds;

	void OnTriggerEnter(Collider col) 
	{
		if (col.gameObject.tag == "Ground") 
		{
			sounds.CmdPlayOnServer();
		}
	}
}
