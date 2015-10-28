#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public enum TOOLSTATE
{
	RANDOMIZE,
	SPAWN_ACTUAL_PREFAB,
	STORE
};
[ExecuteInEditMode]
public class buildingSwapper : MonoBehaviour {

	public TOOLSTATE tool;
	public string nameArray, preFabName;
	public GameObject storeInGameObject;
	public GameObject prefabNeeded;
	public GameObject[] newBuildings;
	public bool toggle;
	public List<Transform> buildingPlace;

	private void Update()
	{
		if(toggle)
		{
			checker();
			toggle =! toggle;
		}
	}

	private void checker()
	{
		buildingPlace = new List<Transform>();
		GameObject[] buildingFinder = GameObject.FindGameObjectsWithTag(nameArray);
		if(tool != TOOLSTATE.SPAWN_ACTUAL_PREFAB)
		{
			for(int i=0; i < buildingFinder.Length; i++)
			{	
				buildingPlace.Add (buildingFinder[i].transform as Transform);
				if(tool == TOOLSTATE.RANDOMIZE)
				{
					if(buildingPlace[i].parent == null)
					{
						//Instantiate(newBuildings[randomBuildingSpawner()], buildingPlace[i].position, Quaternion.identity);
						//DestroyImmediate(buildingPlace[i].gameObject);
					}
					else
					{
						buildingPlace[i].parent = null;
					}
				}
				else if(tool == TOOLSTATE.STORE)
				{
					buildingPlace[i].parent = storeInGameObject.transform;
				}
			}
		}
		GameObject[] objectFinder = GameObject.FindGameObjectsWithTag(nameArray);

		if(tool == TOOLSTATE.SPAWN_ACTUAL_PREFAB)
		{
			for(int i =0; i < objectFinder.Length; i++)
			{
				buildingPlace.Add (objectFinder[i].transform as Transform);
				if(buildingPlace[i].name.Contains(preFabName))
			  	{
					print ("runs");
					GameObject obj = PrefabUtility.InstantiatePrefab(prefabNeeded) as GameObject;
					obj.transform.position = buildingPlace[i].transform.position;
					obj.transform.rotation = buildingPlace[i].transform.rotation;
					DestroyImmediate(buildingPlace[i].gameObject);
				}
			}
		}

	}
	private void spawnPrefabs()
	{
	}

	private int randomBuildingSpawner()
	{
		return Random.Range(0,newBuildings.Length);
	}
	
}
#endif