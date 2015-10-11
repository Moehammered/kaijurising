using UnityEngine;
using System.Collections;
public enum BUILDING_STATE
{
	STATE_ZERO,
	STATE_ONE,
	STATE_TWO,
	STATE_THREE
};
public class DestructionStates : MonoBehaviour {

	private BUILDING_STATE currentState;
	public int changeState;
	public float health,highest,average,lowest;

	private void Update()
	{
		//checkBuildingHealth();
		intergerState(setHealthState(health));
	}

	private BUILDING_STATE intergerState(int stateChanger)
	{
		switch (stateChanger)
		{
			case 0: //This represents the State_Zero
				print ("nothing");
				currentState = BUILDING_STATE.STATE_ZERO;
				break;
				//return BUILDING_STATE .STATE_ZERO;

			case 1:	//This represents the State_One
				print ("has taken some damage");
				currentState = BUILDING_STATE.STATE_ONE;
				break;
				//return BUILDING_STATE.STATE_ONE;

			case 2:	//This represents the State_Two
				print ("is pretty beaten up");
				currentState = BUILDING_STATE.STATE_TWO;
				break;
				//return BUILDING_STATE.STATE_TWO;

			case 3:	//This represents the State_Three
				print ("is close to death atm");
				currentState = BUILDING_STATE.STATE_THREE;
				break;
				//return BUILDING_STATE.STATE_THREE;
		}
		return currentState;
	}

	private int setHealthState(float health)
	{
		if(health > highest)
		{
			changeState = 0;
		}
		if(health < highest)
		{
			changeState = 1;
		}
		if(health < average)
		{
			changeState = 2;
		}
		if(health < lowest)
		{
			changeState = 3;
		}
		return changeState;
	}
}
