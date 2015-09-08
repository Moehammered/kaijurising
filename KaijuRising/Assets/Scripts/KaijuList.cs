using UnityEngine;
using System.Collections;

public class KaijuList : MonoBehaviour {

	/*
	 * Goes onto a prefab 'Kaiju List' that remains in the assets folder. 
	 * It holds the list of all Kaiju's in the game.
	 * It is mandatory that the list is kept up to date. 
	 * Otherwise, any kaiju's not on the list won't spawn if selected.
	 */ 

	public GameObject[] kaijuObjects;

	public GameObject[] getKaijuObjects()
	{
		return kaijuObjects;
	}
}