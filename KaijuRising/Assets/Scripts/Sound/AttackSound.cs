using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AttackSound :  NetworkBehaviour {

	public GameObject soundObject;

	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) 
		{
			attackInput ();
		}
	}

	[ClientRpc]
	public void RpcInstantiateOnServer() 
	{
		GameObject instance = (GameObject)Instantiate(soundObject, transform.position, transform.rotation);
	}

	[Command]
	public void CmdInstantiateOnServer() 
	{
		RpcInstantiateOnServer ();
		if (!isServer) 
		{
			GameObject instance = (GameObject)Instantiate(soundObject, transform.position, transform.rotation);
		}
	}

	private void attackInput() 
	{
		if (Input.GetKey (KeyCode.Q)) 
		{
			CmdInstantiateOnServer();
		}
	}
}
