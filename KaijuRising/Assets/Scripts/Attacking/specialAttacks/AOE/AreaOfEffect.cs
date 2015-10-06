using UnityEngine;
using System.Collections;

public class AreaOfEffect : MonoBehaviour {

	public float damageValue, radius;
	public string[] tags;

	private void Update()
	{
		activateAttack();
	}

	private void Start()
	{
		transform.localScale = new Vector3(radius,1f,radius);
	}

	private void activateAttack()
	{
		storePlayers(transform.position, radius);
		Destroy(gameObject);
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
