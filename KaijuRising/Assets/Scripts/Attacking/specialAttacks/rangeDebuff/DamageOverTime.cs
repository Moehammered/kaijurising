using UnityEngine;
using System.Collections;

public class DamageOverTime : TimedPickup {
	
	public float damageAmount;
	public int attacksPerIteration;
	private Entity kaiju;

	protected override void runActiveEffect ()
	{	
		kaiju = targetObject.GetComponent<Entity>();
		StartCoroutine(damagePerSecond());
	}

	private IEnumerator damagePerSecond()
	{
		for (int i=0; i<attacksPerIteration; i++)
		{
			kaiju.takeDamage(damageAmount);
		}
		yield return null;
	}

	protected override void finishEffect ()
	{
		StopCoroutine(damagePerSecond());
		kaiju = null;
	}
}
