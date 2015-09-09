using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BlankPlayerSetup : NetworkBehaviour {

	/*
	 * The default player that spawns on connection.
	 * It then automatically checks what kaiju the player chose to spawn and replaces itself with that kaiju.
	 */ 
	public PlayerSelection playerSelection;
	public GameObject[] spawnablePrefabs;

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		spawnKaiju();
	}
	
	private void spawnKaiju()
	{
		GameObject chosenKaijuObject = null;
		chosenKaijuObject = GameObject.Find("Canvas").GetComponent<PlayerSelection>().kaiju;
		GameObject instantiatedKaiju = (GameObject)Instantiate (chosenKaijuObject, Vector3.zero, Quaternion.identity);
		Destroy (GameObject.Find("Canvas"));
		NetworkServer.Spawn (instantiatedKaiju);
		NetworkServer.ReplacePlayerForConnection (connectionToClient, instantiatedKaiju, 0);
		NetworkServer.Destroy (gameObject);
		
	}
}