using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TimedObjects : NetworkBehaviour 
{
	public float duration = 1;
	
	protected virtual void Start()
	{
		objectActivate();
	}
	
	protected virtual void Update()
	{
		if (duration > 0)
		{
			duration -= Time.deltaTime;
			objectRunning();
		}
		else
		{
			NetworkServer.Destroy(gameObject);
		}
	}
	
	protected virtual void objectRunning ()
	{
		
	}
	
	protected virtual void objectActivate()
	{
	
	}
}
