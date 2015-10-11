using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AreaOfEffect : NetworkBehaviour {

	public float damageValue, radius;
	public string[] tags;
	
	private void Update()
	{
		if(!isLocalPlayer)
		{
			Cmd_activateAttack();
		}
	}

	private void Start()
	{
		transform.localScale = new Vector3(radius,1f,radius);
	}

	[Command]
	private void Cmd_activateAttack()
	{
		storePlayers(transform.position, radius);
		NetworkServer.Destroy(gameObject);
	}

	private void storePlayers(Vector3 center, float radius) 
	{
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		for(int i=0; i<hitColliders.Length; i++)
		{
			if(hitColliders != null)
			{
				for(int z=0; z< tags.Length; z++)
				{
					if(hitColliders[i].gameObject.tag == tags[z])
					{
						dealDamage(hitColliders[i].gameObject);
					}
				}
			}
		}
	}

	private void dealDamage(GameObject collidedObject)
	{
		Entity kaiju = collidedObject.GetComponent<Entity>();
		if(kaiju)
		{
			kaiju.takeDamage(damageValue);
			kaiju = null;
		}
	}
}
