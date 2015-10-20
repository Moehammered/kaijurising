using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	public string playerName = "Player";
	private string localPlayerName;
	private int sameNameCount;
	
	/*
	 * Goes onto player prefab, does some essential things on start such as
	 * enabling the camera client-side ONLY, stopping potential conflicts with other clients.
	 * Setting up the camera, canvas & joystick controls.
	 */ 

	public GameObject scoreBoard;
	public GameObject canvas;
	public GameObject playerCameraParent;
	public GameObject untiltedCameraRepresentation;
	public GameObject audioListener;

	public override void OnStartLocalPlayer()
	{
		//Cmd_assignName();
		scoreBoard.transform.SetParent (null, false);
		canvas.SetActive (true);
		GameObject joystickButton = GameObject.FindGameObjectWithTag("Joystick Origin");
		//print (joystickButton.name);
		//joystickButton.GetComponent<VirtualJoystick>().getPlayer(gameObject);
		canvas.transform.SetParent (null, false);
		//playerCameraParent.transform.SetParent (null, true);
		playerCameraParent.SetActive (true);
		untiltedCameraRepresentation.SetActive (true);
		untiltedCameraRepresentation.transform.SetParent (null, true);
		GameObject.FindGameObjectWithTag("MainMenuCam").SetActive (false);
		audioListener.SetActive (true);
		Destroy (GameObject.Find("SelectionCanvas"));
	}
	
	public void sortName()
	{
		if(GameObject.Find(playerName) != null)
		{
			sameNameCount++;
			playerName = (localPlayerName + " " + sameNameCount);
			sortName();
		}
	}
	
	
	[Command]
	public void Cmd_assignName()
	{
		localPlayerName = playerName;
		sortName();
		this.gameObject.name = playerName;
		Rpc_assignName(playerName);
	}
	
	[ClientRpc]
	public void Rpc_assignName(string newName)
	{
		this.gameObject.name = newName;
	}
}