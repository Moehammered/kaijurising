using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[System.Serializable]
public struct sounds 
{
	public string attack;
	public string walk;
	public string buildingDestroy;
}

public class InstanciateSound :  NetworkBehaviour {

	public GameObject soundObject;
	public sounds sounds;
	public string soundFolderPath = "Sounds"; // references the path to the sounds folder in Resources folder
	public string soundFolderPrefabs = "Prefabs";

	public bool hasPlayed;
	public float time = 1;	

	void Start() 
	{
		sounds.buildingDestroy = "building-destruction.mp3";
		soundObject = Resources.Load (soundFolderPath + "/" + soundFolderPrefabs + "/DestroyableSound") as GameObject;
	}

	[ClientRpc] 
	public void RpcInstantiateOnClient(string sound) // Have to send audio clip as a string param. 
	{												 // Client RPC doesn't allow for certain param types, only string, int, float, bool.
		// Find the audio clip
		AudioClip theSound = Resources.Load(soundFolderPath + "/" + sound) as AudioClip;

		// Instantiate sound on clients and self
		GameObject instance = (GameObject)Instantiate(soundObject, transform.position, transform.rotation);

		// Play the specified sound clip
		instance.GetComponent<AudioSource> ().clip = theSound;
	}

	public void instantiateOnServer(string sound) 
	{
		// have to call RPC function from Command(server) if the being called from client, cannot run RPC functions from client.
		AudioClip theSound = Resources.Load(soundFolderPath + "/" + sound) as AudioClip;
		print (Resources.Load(soundFolderPath + "/" + sound));
		GameObject instance = (GameObject)Instantiate(soundObject, transform.position, transform.rotation);

		instance.GetComponent<AudioSource> ().clip = theSound;
		instance.GetComponent<AudioSource> ().Play ();
	}
}
