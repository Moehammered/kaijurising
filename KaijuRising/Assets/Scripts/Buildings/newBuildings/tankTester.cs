using UnityEngine;
using System.Collections;

public class tankTester : MonoBehaviour {

	public GameObject objectToTank;
	public int offset;
	public int amountToSpawn;
	public int row;

	private void Start()
	{
		for(int i = 0; i < amountToSpawn; i++)
		{
			for(int z = 0; z < row; z++) 
			{
				Instantiate(objectToTank,spawnPos(i,z),Quaternion.identity);
			}
		}
	}


	private Vector3 spawnPos(float x, float z)
	{
		return new Vector3(x * offset,0f,z * offset);
	}
}
