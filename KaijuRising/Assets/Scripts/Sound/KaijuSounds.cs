using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class KaijuSounds : MonoBehaviour {

	public AudioSource source;

	[ClientRpc]
	private void RpcPlayOnClients(string clip) 
	{
		AudioClip clipFile = Resources.Load("Sounds/Kaijus/SFX/" + clip) as AudioClip;
		source.PlayOneShot (clipFile);
	} 

	[Command]
	public void CmdPlayOnServer(string clip) 
	{
		RpcPlayOnClients (clip);
	}

	[ClientRpc]
	private void RpcStopOnClients() 
	{
		StartCoroutine(volumeFader());
		source.Stop ();
	} 
	
	[Command]
	public void CmdStopOnServer() 
	{
		RpcStopOnClients ();
	}

	private IEnumerator volumeFader() 
	{
		float originalVolume = source.volume;

		while (source.volume > 0) 
		{
			source.volume -= 0.5f + Time.deltaTime;
			yield return null;
		}
		source.Stop ();
		source.volume = originalVolume;
	}
}
