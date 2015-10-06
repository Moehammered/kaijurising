using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class addStuffToBuildings : MonoBehaviour {

	public bool run;
	public DestroyBuilding[] checkingIfBuilding;

	private void Update()
	{
		if(run)
		checkAllBuildings();
	}

	private void checkAllBuildings()
	{
		checkingIfBuilding = GameObject.FindObjectsOfType<DestroyBuilding>() as DestroyBuilding[];//gets all buildings in the scene
		for(int i=0; i< checkingIfBuilding.Length; i++)
		{
			print ("i am currently a building");
			//checkingIfBuilding[i].gameObject.AddComponent<Entity>();// allows you to add any componet to each and every destructable building
		}
	}
	
}

