using UnityEngine;
using System.Collections;

public class rangedAttack : MonoBehaviour {
	
	public GameObject projectile;
	public float speed, fireRate;
	public float duration;

	private void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.W))
		{
			attack();
		}
	}
	
	private void attack()
	{
		if(duration > fireRate)
		{
			GameObject bullet = (GameObject)Instantiate(projectile,transform.position + transform.forward,Quaternion.identity);
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * speed, ForceMode.Impulse); 
			rb = null;
			duration = 0;
		}
		else
		{
			duration += Time.deltaTime;
		}
	}
}
