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
	public string soundFolderPath = "Sounds"; // references the path to the sounds folder in Resources folder

	[ClientRpc]
	public void RpcInstantiateOnClient(string sound) 
	{
		// Have to send audio clip as a string param. Client RPC doesn't allow for certain param types, only string, int, float, bool.

		// Find the audio clip
		AudioClip theSound = Resources.Load(soundFolderPath + "/SoundWalk") as AudioClip;

		// Instantiate sound on clients and self
		GameObject instance = (GameObject)Instantiate(soundObject, transform.position, transform.rotation);

		// Play the specified sound clip
		instance.GetComponent<AudioSource> ().PlayOneShot(theSound);
	}

	[Command]
	public void CmdInstantiateOnServer(string sound) 
	{
		// have to call RPC function from Command(server) if the being called from client, cannot run RPC functions from client.
		RpcInstantiateOnClient (sound);
	}
}
