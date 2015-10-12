using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum MODE
{
	FIND_BUILDINGS,
	CHANGE_BUILDING_INFO,
	CHECK_FOR_FAKE_BUILDING

};
[System.Serializable]
public struct changeableNames
{
	public string fiftyMetreNewName,hundredMetreNewName,hundredfiftyMetreNewName,unKnownNewName,aqaductsNewName;
}
[ExecuteInEditMode]
public class addStuffToBuildings : MonoBehaviour {

	//Controls to switch between finding buildings and changing info
	public MODE change;
	//Put the tag name of the desired Building / GameObject
	public string buildingTag;
	public bool run = false;
	public changeableNames changeBuildingInformation;
	private Material tempPlacement;
	//for these material variables here, please place the correct material for each building
	//what this will do will be used to check buildings/gameObject and will class them based on a certain
	//type they are
	public Material fiftyMMaterial,hundredMMaterial,hundredFiftyMMaterial, noMaterialColour, aqaductsMaterial;

	private Color fiftyMColour, hundredMColour, hundredFiftyMColour, noColour, aqaductsColour;
	public GameObject[] checkingIfBuilding;
	public List<GameObject> smallestBuildings, secondLargest, tallestBuilding, unColoured, aquaduct;


	public DestroyBuilding[] findAllBuildings;
	public GameObject[] checkForFakeBuildings;

	private void Update()
	{
		if(run)
		{
			switch(change)
			{
				case MODE.CHANGE_BUILDING_INFO:
					changeBuildingInfo();
					break;
				case MODE.FIND_BUILDINGS:
					checkAllBuildings();
					break;
				case MODE.CHECK_FOR_FAKE_BUILDING:
					checkForFakeBuilding();
					break;
			}
			run =! run;
		}
	}

	private void checkForFakeBuilding()
	{
		findAllBuildings = GameObject.FindObjectsOfType<DestroyBuilding>() as DestroyBuilding[];
		for(int z=0; z<findAllBuildings.Length; z++)
		{
			findAllBuildings[z].tag = "Building";
			//findAllBuildings[z].gameObject.AddComponent<DestructionStates>();
		}

		checkForFakeBuildings = GameObject.FindGameObjectsWithTag(buildingTag);
		for(int i=0; i< checkForFakeBuildings.Length; i++)
		{
			if(checkForFakeBuildings[i].gameObject.GetComponent<Renderer>() == null)
			{
				DestroyImmediate(checkForFakeBuildings[i].GetComponent<DestroyBuilding>());
				DestroyImmediate(checkForFakeBuildings[i].GetComponent<Entity>());
				checkForFakeBuildings[i].tag = "Untagged";
			}
		}
	
	}

	private void changeBuildingInfo()
	{
		//gives the buildings inside the list new properties, including Tag + Name

		for(int q=0; q< smallestBuildings.Count; q++)
		{
			smallestBuildings[q].gameObject.name = changeBuildingInformation.fiftyMetreNewName;
			//smallestBuildings[q].gameObject.tag = changeBuildingInformation.fiftyMetreNewName;
		}
		for(int q=0; q< secondLargest.Count; q++)
		{
			//secondLargest[q].gameObject.name = changeBuildingInformation.hundredMetreNewName;
			secondLargest[q].gameObject.tag = changeBuildingInformation.hundredMetreNewName;;
		}
		for(int q=0; q< tallestBuilding.Count; q++)
		{
			//tallestBuilding[q].gameObject.name = changeBuildingInformation.hundredfiftyMetreNewName;
			tallestBuilding[q].gameObject.tag = changeBuildingInformation.hundredfiftyMetreNewName;

		}
		for(int q=0; q< unColoured.Count; q++)
		{
			//unColoured[q].gameObject.name = changeBuildingInformation.unKnownNewName;
			unColoured[q].gameObject.tag = changeBuildingInformation.unKnownNewName;

		}
		for(int q=0; q< aquaduct.Count; q++)
		{
		}
	}

	private void checkAllBuildings()
	{
		checkingIfBuilding = GameObject.FindGameObjectsWithTag(buildingTag);
	
		fiftyMColour = fiftyMMaterial.color;
		hundredMColour = hundredMMaterial.color;
		hundredFiftyMColour = hundredFiftyMMaterial.color;
		noColour = noMaterialColour.color;
		aqaductsColour = aqaductsMaterial.color;

		smallestBuildings = new List<GameObject>();
		secondLargest = new List<GameObject>();
		tallestBuilding = new List<GameObject>();
		unColoured = new List<GameObject>();
		aquaduct = new List<GameObject>();

		for(int i=0; i< checkingIfBuilding.Length; i++)
		{
			tempPlacement = checkingIfBuilding[i].GetComponent<Renderer>().sharedMaterial;
	
			if(tempPlacement != null)
			{
				if(tempPlacement.color == fiftyMMaterial.color) 
				{
					smallestBuildings.Add (checkingIfBuilding[i].gameObject);
				}
				
				if(tempPlacement.color == hundredMMaterial.color) 
				{
					secondLargest.Add (checkingIfBuilding[i].gameObject);
				}
				
				if(tempPlacement.color == hundredFiftyMMaterial.color) 
				{
					tallestBuilding.Add (checkingIfBuilding[i].gameObject);
				}

				if(tempPlacement.color == noMaterialColour.color) 
				{
					unColoured.Add (checkingIfBuilding[i].gameObject);
				}
				
				if(tempPlacement.color == aqaductsMaterial.color) 
				{
					aquaduct.Add (checkingIfBuilding[i].gameObject);
				}

			}
		}
	}
	
}