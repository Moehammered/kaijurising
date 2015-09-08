using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	/*
	 * This object holds the kaiju that the player selected and is transferred from the offline scene to the online scene.
	 * In the online scene, BlankPlayerSetup will grab whatever kaiju this object contaisn and spawn it.
	 */ 

	public GameObject kaiju;

	private void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	public void saveKaiju(GameObject selectedKaiju)
	{
		kaiju = selectedKaiju;
	}
}