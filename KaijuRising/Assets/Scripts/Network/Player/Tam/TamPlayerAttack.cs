using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TamPlayerAttack : NetworkBehaviour 
{	
	public string[] tags;
	public float attackRadius;
	public GameObject attackCenter;
	public float attackDamage;
	public float specialDamage;
	private TamPlayerScore playerScore;
	private int attackCounter;
	
	[Command]
	private void Cmd_detectObjects(Vector3 center, GameObject player, float damage)
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
		Cmd_detectObjects(attackCenter.transform.position, gameObject, attackDamage);
	}
	
	public void specialAttack()
	{
		Cmd_detectObjects(attackCenter.transform.position, gameObject, specialDamage);
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
}