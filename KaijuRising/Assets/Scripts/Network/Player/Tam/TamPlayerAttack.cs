﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public enum ATTACK_KAIJU_TYPE
{
	YUM_KAAX,
	FOLSOL,
	TRIKARENOS
};

public class TamPlayerAttack : NetworkBehaviour 
{	
	public ATTACK_KAIJU_TYPE KAIJU_TYPE;
	public string[] tags;
	public float attackRadius;
	public GameObject attackCenter;
	public float attackDamage;
	public float specialDamage;
	private TamPlayerScore playerScore;
	private int attackCounter;
	[Header("YumKaax")]
	public GameObject tendrils;
	public float tendrilRadius;
	
	[Command]
	private void Cmd_detectObjects(Vector3 center, float damage)
	{
		Collider[] colliders = Physics.OverlapSphere(center, attackRadius);
		for(int i=0; i<colliders.Length; i++)
		{
			if(colliders != null)
			{
				for(int z=0; z< tags.Length; z++)
				{
					if(colliders[i].gameObject.tag == tags[z])
					{
						dealDamageTowardsBuildings(colliders[i].gameObject);
					}
				}
				if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag == "Player")
				{
					colliders[i].gameObject.GetComponent<TamPlayerHealth>().modifyHealth(-damage, GetComponent<TamPlayerScore>().getPlayerNumber());
				}
			}
		}	
	}
		
	private void Start()
	{
		playerScore = GetComponent<TamPlayerScore>();
	}
	
	public void normalAttack()
	{
		Cmd_detectObjects(attackCenter.transform.position, attackDamage);
	}
	
	public virtual void specialAttack()
	{
		switch(KAIJU_TYPE)
		{
			case ATTACK_KAIJU_TYPE.YUM_KAAX:
				Cmd_detectPlayers(specialDamage, tendrilRadius);
				break;
			default:
				break;
		}
	}
	
	public void timedNormalAttack(float duration)
	{
		StartCoroutine(timedAttack(duration));
	}
	
	private IEnumerator timedAttack(float duration)
	{
		float timer = duration;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		normalAttack();
	}
	
	private void dealDamageTowardsBuildings(GameObject collidedObject)
	{
		Entity building = collidedObject.GetComponent<Entity>();
		if(building)
		{
			building.takeDamage(attackDamage);
			building = null;
		}
	}
	
	//YUMKAAX
	[Command]
	private void Cmd_detectPlayers(float damage, float specialRadius)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, specialRadius);
		for(int i=0; i<colliders.Length; i++)
		{
			if(colliders != null)
			{
				if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag == "Player")
				{
					colliders[i].gameObject.GetComponent<TamPlayerHealth>().modifyHealth(-damage, GetComponent<TamPlayerScore>().getPlayerNumber());
					GameObject tendril_GO = (GameObject)Instantiate(tendrils, colliders[i].gameObject.transform.position, Quaternion.identity);
					NetworkServer.Spawn(tendril_GO);
					break;
				}
			}
		}	
	}
}