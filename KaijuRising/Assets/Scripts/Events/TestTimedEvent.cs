using UnityEngine;
using System.Collections;

public class TestTimedEvent : TimedEvent 
{
	public void Start()
	{
		activateEvent();
	}

	protected override void runEventEffects()
	{
		print("Event is active");
	}

	protected override void finishEvent()
	{
		print("Event has ended");
	}

	protected override void activateEvent()
	{
		base.activateEvent();
		print("event activated");
	}
}
