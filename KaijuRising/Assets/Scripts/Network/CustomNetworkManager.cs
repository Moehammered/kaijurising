using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager {

	/*
	 * Custom network manager, for the purpose of being able to mass-add spawnable prefabs.
	 * The most important reason for this is that every single building must have its fragments synced.
	 * But the default network manager in the inspector can only take in a spawnable prefab one at a time. 
	 * This would be too time consuming. 
	 * 
	 * To sum it up... This class has an array that extends the functionality of Network Manager's registered spawnable prefab section
	 * by allowing a developer to drag multiple prefabs in at a time.
	 */ 

	public GameObject[] spawnablePrefabs;
	private const string key = "ChosenKaiju"; // The PlayerPreference key which holds the chosen kaiju.
	[Header("Kaijus")]
	public GameObject rexKaiju = null;
	public GameObject yumKaax = null;

	// Accessed by the selectCanvas UI buttons. Sets the chosen kaiju into PlayerPrefs.
	public void chooseKaiju(string kaijuName)
	{
		PlayerPrefs.SetString (key, kaijuName);
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public override void OnStartServer ()
	{
		// The server registers the function 'customAddPlayer' and associates it with an ID.
		// When the server hears the particular ID, the function is called.
		// When the function is called, a class of type MessageBase is sent to the server in addition to other info
		// Such as the connection ID and so on.
		NetworkServer.RegisterHandler(9001, customAddPlayer);
	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		// Stops this base function from running, 
		// an overrided OnClientSceneChanged runs similar behaviour but makes sure a custom chosen kaiju is spawned
	}

	public override void OnClientSceneChanged (NetworkConnection conn)
	{
		//base.OnClientSceneChanged (conn);

		ClientScene.Ready(conn);
		
		for(int i = 0; i < spawnablePrefabs.Length; i++)
		{
			ClientScene.RegisterPrefab(spawnablePrefabs[i]);
		}
		
		CustomAddPlayerMessage message = new CustomAddPlayerMessage();
		message.chosenKaiju = PlayerPrefs.GetString(key);
		ClientScene.readyConnection.Send(9001, message);
	}

	// Registered function that is called by the client on connection.
	private void customAddPlayer(NetworkMessage networkMessage)
	{
		string chosenKaiju = networkMessage.ReadMessage<CustomAddPlayerMessage>().chosenKaiju;
		GameObject player = null;
		print (chosenKaiju);
		switch(chosenKaiju)
		{
		case "REX":
			player = (GameObject)Instantiate (rexKaiju, Vector3.zero, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(networkMessage.conn, player, 0);
			break;
		case "YUMKAAX":
			player = (GameObject)Instantiate (yumKaax, Vector3.zero, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(networkMessage.conn, player, 0);
			break;
		}
	}
}