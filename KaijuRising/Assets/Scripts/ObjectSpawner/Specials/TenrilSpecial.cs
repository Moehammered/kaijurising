using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TenrilSpecial : TimedObjects 
{
	public float speed = 10;
	public float visibilityDelay;
	private bool isVisible = false;
	
	protected override void objectRunning ()
	{
		if (isVisible)
		{
			transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
		}
	}
	
	protected override void objectActivate ()
	{
		StartCoroutine(delaySpawn(visibilityDelay));
	}
	
	private IEnumerator delaySpawn(float delay)
	{
		alterVisibility(false);
		float timer = delay;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		alterVisibility(true);
		isVisible = true;
	}
	
	private void alterVisibility(bool state)
	{
		Renderer[] childRend = transform.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < childRend.Length; i++)
		{
			childRend[i].enabled = state;
		}
	}
}
