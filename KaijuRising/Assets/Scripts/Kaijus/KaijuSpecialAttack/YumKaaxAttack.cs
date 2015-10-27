using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class YumKaaxAttack : TamPlayerAttack 
{
	public GameObject tendrils;
	public float tendrilRadius;
	
//	public override void specialAttack ()
//	{
//		GameObject tendril_GO = (GameObject)Instantiate(tendrils, transform.position, Quaternion.identity);
//		NetworkServer.Spawn(tendril_GO);
//		//Cmd_detectPlayers(specialDamage, tendrilRadius);
//	}
	
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
