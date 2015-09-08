using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BlankPlayerSetup : NetworkBehaviour {

	/*
	 * The default player that spawns on connection.
	 * It then automatically checks what kaiju the player chose to spawn and replaces itself with that kaiju.
	 */ 

	public KaijuList kaijuList;
	public GameObject[] spawnablePrefabs;

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		print (GameObject.Find ("Player Spawner").GetComponent<PlayerSpawner>().kaiju.name);
		Cmd_spawnKaiju (GameObject.Find ("Player Spawner").GetComponent<PlayerSpawner>().kaiju.name);
	}

	// For some reason, the Command function does not transfer a GameObject parameter, so I can only send a string.
	[Command]
	private void Cmd_spawnKaiju(string chosenKaijuName)
	{
		GameObject[] kaijus = kaijuList.getKaijuObjects();
		GameObject chosenKaijuObject = null;
		print (chosenKaijuName);

		for(int i = 0; i < kaijus.Length; i++)
		{
			if(kaijus[i].name.Equals (chosenKaijuName))
			{
				chosenKaijuObject = kaijus[i];
				break;
			}
		}

		GameObject instantiatedKaiju = (GameObject)Instantiate (chosenKaijuObject, Vector3.zero, Quaternion.identity);
		NetworkServer.Spawn (instantiatedKaiju);
		NetworkServer.ReplacePlayerForConnection (connectionToClient, instantiatedKaiju, 0);
		NetworkServer.Destroy (gameObject);
	}
}