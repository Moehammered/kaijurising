using UnityEngine;
using System.Collections;

public class TimedEvent :  BaseEvent {
	
	public float duration = 1f;
	protected bool isActive = false;
	
	protected virtual void Update ()
	{
		if(isActive)
		{
			runEventEffects ();
			duration -= Time.deltaTime; // Countdown duration towards 0.
			
			if(duration < 0)
			{
				//Finish Event Effects
				finishEvent();
				isActive = false;
			}
		}
	}
	
	protected override void activateEvent () //Make Sure you Do the base or this wont work!!!!!!
	{
		isActive = true;
	}
	
	protected virtual void runEventEffects()
	{

	}
	
	protected virtual void finishEvent()
	{

	}
}