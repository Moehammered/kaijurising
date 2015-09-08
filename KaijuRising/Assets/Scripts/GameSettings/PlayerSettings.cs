using UnityEngine;
using System.Collections;

public class PlayerSettings : MonoBehaviour {

	public GameObject[] kaijus = new GameObject[10];		//Drag and drop any Kaiju into this array to play with it.
	public GameObject currentKaiju;
	
	//Ensure that the gameObject this script is attatched to has the Tag "GameSettings"
	
	private void Awake()
	{
		DontDestroyOnLoad(this);		//This function allows us to keep a Gameobject with all of its current values from being destroyed when changing scene.
	}
	
	//Once you create a button within the UI for each Kaiju option. You need to create a function for those buttons to run.
	//Once we use delegates we can create a function for each kaiju that we add to the game and literally drag and drop a prefab.
	
	public void Player1()		
	{
		currentKaiju = kaijus[0];
		Application.LoadLevel(1);
	}
	
	public void Player2()
	{
		currentKaiju = kaijus[1];
		Application.LoadLevel(1);
	}
	
	public void Player3()
	{
		currentKaiju = kaijus[2];
		Application.LoadLevel(1);
	}
	
	// For each of these functions we retrieved the Prefab from the array of characters corresponding to the button pressed and set it to the currentKaiju.
	// The GetPlayerSettings script in the next scene will retrieve this data and change the character model accordingly.
}
