using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GetPlayerSettings : NetworkBehaviour {
	
	public NetworkManager networkManagerReference;		// This gets a reference to the NetworkManager to tell the Host/Server which Kaiju to spawn depending on the playerSettingsReference.
	private GameObject playerSettingsReference;		//This is the Gameobject that we have transfered from our starting screen to denote the players settings. Eg. which Kaiju they are playing.
	
	private void Awake()
	{
		playerSettingsReference = GameObject.FindGameObjectWithTag("GameSettings");		//This finds the GameObject we have transfered from the startScreen and set is as a variable for easy access.
		networkManagerReference.playerPrefab = playerSettingsReference.GetComponent<PlayerSettings>().currentKaiju;		//This gets the settings from the Gameobject and changes the Player Prefab.
	}
}
