using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class KaijuSounds : NetworkBehaviour {

	public AudioSource source;

	[ClientRpc]
	private void RpcPlayOnClients() 
	{
		source.Play ();
		source.loop = true;
	} 

	[Command]
	public void CmdPlayOnServer() 
	{
		RpcPlayOnClients ();
	}

	[ClientRpc]
	private void RpcStopOnClients() 
	{
		source.loop = false;
		StartCoroutine(volumeFader());

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
