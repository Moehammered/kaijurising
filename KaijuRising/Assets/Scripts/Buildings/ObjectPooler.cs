using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public GameObject pooledObject;
	public int sizeOfList;

	public List<GameObject> objectPool = new List<GameObject>();

	void Start() 
	{
		objectPool.Add (pooledObject);
	}

	public void addToPool() 
	{
		for(int i = 0; i > sizeOfList; i++) 
		{
			objectPool.Add(pooledObject);
		}
	}
}
