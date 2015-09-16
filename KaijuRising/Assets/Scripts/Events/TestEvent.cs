using UnityEngine;
using System.Collections;

public class TestEvent : BaseEvent
{
	public void Start()
	{
		activateEvent();
	}

	protected override void activateEvent()
	{
		print("Event Activated");
	}
}