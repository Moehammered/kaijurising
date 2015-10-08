using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	/*
	 * Goes onto player prefab, does some essential things on start such as
	 * enabling the camera client-side ONLY, stopping potential conflicts with other clients.
	 * Setting up the camera, canvas & joystick controls.
	 */ 
	
	public GameObject canvas;
	public GameObject playerCameraParent;
	public GameObject untiltedCameraRepresentation;
	public GameObject audioListener;

	public override void OnStartLocalPlayer()
	{
		canvas.SetActive (true);
		GameObject joystickButton = GameObject.FindGameObjectWithTag("Joystick Origin");
		//print (joystickButton.name);
		//joystickButton.GetComponent<VirtualJoystick>().getPlayer(gameObject);
		canvas.transform.SetParent (null, true);
		//playerCameraParent.transform.SetParent (null, true);
		playerCameraParent.SetActive (true);
		untiltedCameraRepresentation.SetActive (true);
		untiltedCameraRepresentation.transform.SetParent (null, true);
		GameObject.FindGameObjectWithTag("MainMenuCam").SetActive (false);
		audioListener.SetActive (true);
	}
}