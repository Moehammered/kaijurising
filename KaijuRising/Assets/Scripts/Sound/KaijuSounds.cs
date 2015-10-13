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
		source.Stop();
		source.loop = false;
	} 
	
	[Command]
	public void CmdStopOnServer() 
	{
		RpcStopOnClients ();
	}
}
