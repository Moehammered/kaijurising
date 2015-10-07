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

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		base.OnClientConnect (conn);

		for(int i = 0; i < spawnablePrefabs.Length; i++)
		{
			ClientScene.RegisterPrefab(spawnablePrefabs[i]);
		}
	}
}