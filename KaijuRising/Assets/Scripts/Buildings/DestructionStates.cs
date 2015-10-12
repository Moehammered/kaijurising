using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public enum BUILDING_STATE
{
	STATE_ZERO,
	STATE_ONE,
	STATE_TWO,
	STATE_THREE
};
public class DestructionStates : BuildingChange {
	
	[SyncVar]
	public int changeState;
	[SyncVar]
	public bool toggle = false;
	private BUILDING_STATE currentState;

	private void Start()
	{
		if(isServer)
		{
			grabingBuildingInfo.onModifyHealth += server;
		}
		if(isClient && !isServer)
		{
			print ("i am player");
			grabingBuildingInfo.onModifyHealth += client;
		}
	}

	private void Update()
	{
		if(toggle)
		{
			if(isClient && !isServer)
			{
				print("i only run when a client");
				grabingBuildingInfo.onModifyHealth();
			}
			toggle =! toggle;
		}
	}

	private void server()
	{
		intergerState(setHealthState(grabingBuildingInfo.health));
		toggle = true;
	}

	private void client()
	{
		intergerState(changeState);
	}

	private BUILDING_STATE intergerState(int stateChanger)
	{
		switch (stateChanger)
		{
			case 0: //This represents the State_Zero
				currentState = BUILDING_STATE.STATE_ZERO;
				changeTexture(stateChanger);
				break;

			case 1:	//This represents the State_One
				currentState = BUILDING_STATE.STATE_ONE;
				changeTexture(stateChanger);
				break;

			case 2:	//This represents the State_Two
			currentState = BUILDING_STATE.STATE_TWO;
				changeTexture(stateChanger);
				break;

			case 3:	//This represents the State_Three
				currentState = BUILDING_STATE.STATE_THREE;
				changeTexture(stateChanger);
				break;
		}
		return currentState;
	}

	private int setHealthState(float health)
	{
		if(health > highest)
		{
			changeState = 0;
		}
		if(health <= highest)
		{
			changeState = 1;
		}
		if(health <= average)
		{
			changeState = 2;
		}
		if(health <= lowest)
		{
			changeState = 3;
		}
		return changeState;
	}

	public void changeTexture (int state)
	{
		updatedTexture(materialSelected[state]);
	}
}
