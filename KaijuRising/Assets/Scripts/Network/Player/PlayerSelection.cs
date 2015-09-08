using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSelection : MonoBehaviour {

	/*
	 * This script should go onto the Kaiju Selection UI buttons in the Offline scene, 
	 * and the OnClick event should call kaijuSelected.
	 * The PlayerSpawner object will be assigned a particular kaiju based on the name of the button pressed.
	 */ 

	public PlayerSpawner playerSpawner;
	public GameObject rexKaiju;
	public GameObject yumKaaxKaiju;

	public void kaijuSelected()
	{
		if(gameObject.name.Contains("Rex"))
		{
			playerSpawner.saveKaiju (rexKaiju);
		}
		else if(gameObject.name.Contains("YumKaax"))
		{
			playerSpawner.saveKaiju (yumKaaxKaiju);
		}
	}
}