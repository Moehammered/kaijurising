using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	public GameObject canvas;
	public GameObject playerCameraParent;
	public GameObject untiltedCameraRepresentation;

	public override void OnStartLocalPlayer()
	{
		canvas.SetActive (true);
		GameObject joystickButton = GameObject.FindGameObjectWithTag("Joystick Origin");
		print (joystickButton.name);
		//joystickButton.GetComponent<VirtualJoystick>().getPlayer(gameObject);
		canvas.transform.SetParent (null, true);
		playerCameraParent.transform.SetParent (null, true);
		playerCameraParent.SetActive (true);
		untiltedCameraRepresentation.SetActive (true);
		untiltedCameraRepresentation.transform.SetParent (null, true);
		GameObject.FindGameObjectWithTag("MainMenuCam").SetActive (false);
	}
}