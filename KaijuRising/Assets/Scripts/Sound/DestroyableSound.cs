using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyableSound : NetworkBehaviour {

	public AudioSource audioSource;
	public AudioClip[] audioClips;


	public void playClip(int clipIndex) 
	{
		print ("sound");
		CmdPlaySoundClip(clipIndex);
	}

	[Command]
	private void CmdPlaySoundClip(int clipIndex)
	{
		print ("Command");
		RpcPlaySoundOnClients(clipIndex);
	} 
	
	[ClientRpc]
	private void RpcPlaySoundOnClients(int clipIndex)
	{
		print ("Client");
		audioSource.PlayOneShot(audioClips[clipIndex]);
	}
}
