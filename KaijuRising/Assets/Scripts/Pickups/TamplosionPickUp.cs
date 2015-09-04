using UnityEngine;
using System.Collections;

public class TamplosionPickUp : TimedPickup 
{
	public GameObject explosiveGameObject;
	public Vector3 ballCreationOffset;
	public float ballRadius;
	public LayerMask layer;
	public float gatherSpeed;
	public float explosionSpeed;
	public Collider[] objectInRange; // private
	private Vector3 originPosition;

	
	public override void activatePickup (GameObject target)
	{
		base.activatePickup(target);
		
		//sets the point where the rubble/colliders gather
		originPosition = new Vector3(targetObject.transform.position.x + ballCreationOffset.x, targetObject.transform.position.y + ballCreationOffset.y, targetObject.transform.position.z + ballCreationOffset.z);
		//finds all the objects within a given radius
		objectInRange = Physics.OverlapSphere(originPosition, ballRadius, layer);
		target.AddComponent<Bounce>();
	}
	
	protected override void runActiveEffect ()
	{		
		for (int i = 0; i < objectInRange.Length; i++)
		{
			//gets the rigidbody of colliders
			Rigidbody objectInRangeRgb = objectInRange[i].GetComponent<Rigidbody>();
			//checks if their is a rigidbody
			if (objectInRangeRgb  == null)
			{
				continue;
			}
			//finds direction of all the 
			Vector3 dirToOrigin = originPosition - objectInRange[i].transform.position;
			dirToOrigin.Normalize();
			objectInRangeRgb.AddForce(dirToOrigin * gatherSpeed);
			objectInRangeRgb.useGravity = false;
			if (objectInRangeRgb.transform.position.y > (originPosition.y + 2f))
			{
				objectInRangeRgb.position = new Vector3(objectInRangeRgb.position.x, originPosition.y + 2f, objectInRangeRgb.position.z);
			}
		}
	}
	
	protected override void finishEffect ()
	{
		for (int i = 0; i < objectInRange.Length; i++)
		{
			Rigidbody objectInRangeRgb = objectInRange[i].GetComponent<Rigidbody>();
			if (objectInRangeRgb  == null)
			{
				continue;
			}
			Vector3 dirToOrigin = objectInRange[i].transform.position - originPosition;
			dirToOrigin.Normalize();
			objectInRangeRgb.AddForce(dirToOrigin * explosionSpeed, ForceMode.Impulse);
			objectInRangeRgb.useGravity = true;
		}
		//Instantiate(explosiveGameObject, originPosition, Quaternion.identity);
	}
}
