using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FireBreath : TimedObjects 
{
	public float speed;
	public int attackingPlayer;
	public float damage;
	public Vector3 direction;
	
	protected override void objectRunning ()
	{
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}
	
	private void OnTriggerEnter(Collider colObj)
	{
		if (colObj.gameObject.tag == "Player")
		{
			colObj.gameObject.GetComponent<TamPlayerHealth>().modifyHealth(-damage, attackingPlayer);
		}
	}
}
