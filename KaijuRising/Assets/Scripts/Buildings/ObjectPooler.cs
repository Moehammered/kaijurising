using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public GameObject pooledObject;
	public int sizeOfList;

	public List<GameObject> objectPool = new List<GameObject>();

	void Start() 
	{
		loadPrefabs ();
		//addToPool ();
	}

	public void loadPrefabs() 
	{
		for(int i = 0; i < 10; i++) 
		{
			GameObject instance = Instantiate(pooledObject, transform.position, transform.rotation) as GameObject;
		}
	}

	public void addToPool() 
	{
		for(int i = 0; i < sizeOfList; i++) 
		{
			objectPool.Add(pooledObject);
		}
	}
}
