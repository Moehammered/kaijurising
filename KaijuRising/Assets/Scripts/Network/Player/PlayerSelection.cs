using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSelection : MonoBehaviour {

	/*
	 * This script Changes the spawnable player prefab.
	 * This script should go onto the canvas in the Offline scene.
	 * Link the OnClick Function to the related kaiju button.
	 */ 
	[HideInInspector]
	public GameObject kaiju;
	public GameObject rexKaiju;
	public GameObject yumKaaxKaiju;
	
	private void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}
	
	public void yumKaax()
	{
		kaiju = yumKaaxKaiju;
	}
	
	public void rex()
	{
		kaiju = rexKaiju;
	}
}