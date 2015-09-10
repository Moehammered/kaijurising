using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BlankPlayerSetup : NetworkBehaviour {

	/*
	 * The default player that spawns on connection.
	 * It then automatically checks what kaiju the player chose to spawn and replaces itself with that kaiju.
	 */ 

	public PlayerSelection playerSelection;
	// Tells the blank player all the possible kaijus that it can become. Must be kept up to date when new kaijus are added
	public GameObject[] spawnablePrefabs;

	// Everything that happens in this function before the Command call is only occurring on the client.
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		// Get the kaiju's name (string) that was selected in the offline scene. This canvas is only visible to the client, the server and other clients have no knowledge of it.
		string chosenKaijuObjectName = GameObject.Find("SelectionCanvas").GetComponent<PlayerSelection>().kaiju.name;
		// Destroy the canvas as we no longer need to select a kaiju
		Destroy (GameObject.Find("SelectionCanvas"));
		// Tell the server the name of our kaiju. (Unfortunately we cannot send an actual GameObject type to a server, limitation of [Command])
		Cmd_spawnKaiju(chosenKaijuObjectName);
	}

	/* The following happens on the server:
	 * The server gets the name of the kaiju from the client
	 * It then goes through the spawnablePrefabs list of the blank player and matches the name to an actual GameObject
	 * When it gets the match, it creates the selected Kaiju and destroys the unnecessary blank player.
	 */ 

	[Command]
	private void Cmd_spawnKaiju(string chosenKaijuObjectName)
	{
		GameObject chosenKaijuObject = null;

		// Unfortunate consequence of not being able to send a GameObject through the parameter is having to use a for loop to match a string to a GameObject on the server-side.
		for(int i = 0; i < spawnablePrefabs.Length; i++)
		{
			if(spawnablePrefabs[i].name.Equals(chosenKaijuObjectName))
			{
				chosenKaijuObject = spawnablePrefabs[i];
				break;
			}
		}

		// All object creation that will be seen across the network must occur on the server.
		GameObject instantiatedKaiju = (GameObject)Instantiate (chosenKaijuObject, Vector3.zero, Quaternion.identity);
		NetworkServer.Spawn (instantiatedKaiju);
		NetworkServer.ReplacePlayerForConnection (connectionToClient, instantiatedKaiju, 0);
		NetworkServer.Destroy (gameObject);
	}
}