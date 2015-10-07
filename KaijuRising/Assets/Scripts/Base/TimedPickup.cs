using UnityEngine;
using System.Collections;

public class TimedPickup : CollectablePickup {

	public float duration = 1f;
	protected GameObject targetObject; // Store a reference to the object that uses this pickup.
	protected bool isActive = false; // Says whether or not the pickup should take effect.

	protected override void Update ()
	{
		if(isActive)
		{
			runActiveEffect ();
			duration -= Time.deltaTime; // Countdown duration towards 0.

			if(duration < 0)
			{
				// return anything changed back to normal.
				finishEffect();
				// Destroy itself
				Destroy (gameObject);
			}
		}
	}

	public override void activatePickup (GameObject target)
	{
		targetObject = target; // Store the object that hit this pickup.
		isActive = true; // Mark the pickup should take effect
		// hide the mesh
		Renderer pickupRenderer = GetComponent<Renderer>();
		if(pickupRenderer != null)
		{
			pickupRenderer.enabled = false;
		}

		// Disable the collider
		Collider collider = GetComponent<Collider>();
		if(collider != null)
		{
			collider.enabled = false;
		}

		// Child this pickup to the target to keep track of it.
		transform.parent = target.transform;
	
	}

	protected virtual void runActiveEffect()
	{
		//print ("Timed pickup is active");
	}

	protected virtual void finishEffect()
	{
		//print ("TimedPickup is no longer active, returning target back to normal.");
	}
}