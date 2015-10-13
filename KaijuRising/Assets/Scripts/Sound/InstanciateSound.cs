using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[System.Serializable]
public struct sounds 
{
	public string attack;
	public string walk;
}

public class InstanciateSound :  NetworkBehaviour {

	public GameObject soundObject;
	public sounds sounds;

	[ClientRpc]
	public void RpcInstantiateOnClient(string sound) 
	{
		AudioClip theSound = Resources.Load("SoundWalk") as AudioClip;
		GameObject instance = (GameObject)Instantiate(soundObject, transform.position, transform.rotation);
		instance.GetComponent<AudioSource> ().PlayOneShot(theSound);
	}

	[Command]
	public void CmdInstantiateOnServer(string sound) 
	{
		RpcInstantiateOnClient (sound);
	}
}
