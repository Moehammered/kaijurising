using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public enum BUILDING_TYPE
{
	small,
	medium,
	large,
	aqaduct
};

public class BuildingChange : NetworkBehaviour {
	
	public Material[] newMesh;
	private BUILDING_TYPE differentBuilding;
	private int amountOfStates = 4;

	[HideInInspector]
	public Entity grabingBuildingInfo;
	[HideInInspector]
	public float highest,average,lowest;
	[HideInInspector]
	public Material[] materialSelected;

	private void Awake()
	{
		materialSelected = new Material[4];
		selector(findBuildingType(differentBuilding, gameObject.tag));
		grabingBuildingInfo = GetComponent<Entity>();
	}

	private void selector(BUILDING_TYPE findBuilding)
	{
		for(int i=0; i< amountOfStates; i++)
		{
			materialSelected[i] = newMesh[i];
		}
	}
	
	private BUILDING_TYPE findBuildingType(BUILDING_TYPE buildingChanged, string checkingEntity)
	{
		switch(checkingEntity)
		{
			case "50m":
				highest = 40f;
				average = 25f;
				lowest = 5f;
				return BUILDING_TYPE.small;
			case "100m":
				highest = 60f;
				average = 40f;
				lowest = 20f;
				return BUILDING_TYPE.medium;
			case "150m":
				highest = 90f;
				average = 60f;
				lowest = 40f;
				return BUILDING_TYPE.large;
			case "Building":
				return BUILDING_TYPE.aqaduct;
		}
		return buildingChanged;
	}

	public void updatedTexture(Material newTexture) // this is called in 'Destruction States'
	{
		GetComponent<Renderer>().material = newTexture;
	}
}
