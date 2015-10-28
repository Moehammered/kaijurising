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
	
	private BUILDING_TYPE differentBuilding;
	public int amountOfStates;
	
	[HideInInspector]
	public Entity grabingBuildingInfo;
	[HideInInspector]
	public float highest,average,lowest;
	[HideInInspector]
	public Material[] materialSelected;
	
	private void Awake()
	{
		materialSelected = new Material[amountOfStates];
		selector(findBuildingType(differentBuilding, gameObject.tag));
		grabingBuildingInfo = GetComponent<Entity>();
	}
	
	private void selector(BUILDING_TYPE findBuilding)
	{
		for(int i=0; i< amountOfStates; i++)
		{
			//materialSelected[i] = newMesh[i];
		}
	}
	
	private BUILDING_TYPE findBuildingType(BUILDING_TYPE buildingChanged, string checkingEntity)
	{
		switch(checkingEntity)
		{
		case "50m":
			average = 40f;
			lowest = 20f;
			return BUILDING_TYPE.small;
		case "100m":
			average = 60f;
			lowest = 20f;
			return BUILDING_TYPE.medium;
		case "150m":
			average = 90f;
			lowest = 40f;
			return BUILDING_TYPE.large;
		case "Building":
			return BUILDING_TYPE.aqaduct;
		}
		return buildingChanged;
	}
	
	public void updatedTexture(Renderer currentRender,Material newTexture) // this is called in 'Destruction States'
	{
		currentRender.material = newTexture;
	}
}
