using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

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

	public List<Vector3> spawnPool = new List<Vector3> ();

	public Vector3 spawnPosition;

	public GameObject[] spawnablePrefabs;
	private const string key = "ChosenKaiju"; // The PlayerPreference key which holds the chosen kaiju.
	[Header("Kaijus")]
	public GameObject rexKaiju = null;
	public GameObject yumKaaxKaiju = null;
	public GameObject gozuKaiju = null;
	public GameObject fasolKaiju = null;
	public GameObject macedonKaiju = null;
	public GameObject mantraKaiju = null;
	public GameObject vorkoKaiju = null;
	public GameObject kremonoKaiju = null;
	public GameObject trikarenosKaiju = null;
	// Accessed by the selectCanvas UI buttons. Sets the chosen kaiju into PlayerPrefs.
	public void chooseKaiju(string kaijuName)
	{
		PlayerPrefs.SetString (key, kaijuName);
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		addSpawnPositions ();
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

		if(Application.loadedLevelName != "OfflineScene")
		{
			ClientScene.Ready(conn);
			
			for(int i = 0; i < spawnablePrefabs.Length; i++)
			{
				ClientScene.RegisterPrefab(spawnablePrefabs[i]);
			}
			
			CustomAddPlayerMessage message = new CustomAddPlayerMessage();
			message.chosenKaiju = PlayerPrefs.GetString(key);
			ClientScene.readyConnection.Send(9001, message);
		}
		else
		{
			conn.Disconnect();
		}
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
			makeKaijuPlayer (rexKaiju, player, networkMessage);
			break;
		case "YUMKAAX":
			makeKaijuPlayer (yumKaaxKaiju, player, networkMessage);
			break;
		case "GOZU":
			makeKaijuPlayer (gozuKaiju, player, networkMessage);
			break;
		case "TRIKARENOS":
			makeKaijuPlayer (trikarenosKaiju, player, networkMessage);
			break;
		case "FALSOL":
			makeKaijuPlayer (fasolKaiju, player, networkMessage);
			break;
		case "MACEDON":
			makeKaijuPlayer (macedonKaiju, player, networkMessage);
			break;
		case "VORKO":
			makeKaijuPlayer (vorkoKaiju, player, networkMessage);
			break;
		case "KREMONO":
			makeKaijuPlayer (kremonoKaiju, player, networkMessage);
			break;
		case "MANTRA":
			makeKaijuPlayer (mantraKaiju, player, networkMessage);
			break;
		}
	}

	public void addSpawnPositions() 
	{
		spawnPool.Add (new Vector3 (-941, 0, -925));
		spawnPool.Add (new Vector3 (-727, 0, -1005));
	}

	public void getRandomPosition() 
	{
		int randomIndex = Random.Range (0, spawnPool.Count);
		spawnPosition = spawnPool[randomIndex];
		spawnPool.RemoveAt (randomIndex);
	}
	
	private void makeKaijuPlayer (GameObject gmo, GameObject player, NetworkMessage networkMessage)
	{
		getRandomPosition ();
		player = (GameObject)Instantiate (gmo, spawnPosition, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(networkMessage.conn, player, 0);
	}

}