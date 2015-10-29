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

public class DestructionStates : TextureSwapper {
	
	[SyncVar]
	public int changeState;
	[SyncVar]
	public bool toggle = false;
	//[SyncVar]
	private BUILDING_STATE currentState;
	public Material[] textureStateOne,textureStateTwo,textureStateThree;
	private ParticleSystem myP;

	private void Start()
	{

		if(isServer && !isLocalPlayer)
		{
			grabingBuildingInfo.onModifyHealth += server;
			runSystem(textureStateOne);
		}
		else
		{
			grabingBuildingInfo.onModifyHealth += client;
			runSystem(textureStateOne);
		}
		
		/*
		if(isClient && isLocalPlayer)
		{
			print ("werks");
		}
		*/
		myP = gameObject.GetComponent<ParticleSystem>();
		
	}
	
	private void Update()
	{
		if(toggle)
		{
			if(isClient && !isServer)
			{
				grabingBuildingInfo.onModifyHealth();
			}
			toggle =! toggle;
		}
	}
	
	private void server()
	{
		intergerState(setHealthState(grabingBuildingInfo.health));
		toggle = true;
		//toggle = true;
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
			checkState(textureStateOne);
			//changeTexture(stateChanger);
			break;
			
		case 1:	//This represents the State_One
			currentState = BUILDING_STATE.STATE_ONE;
			checkState(textureStateTwo);
			//changeTexture(stateChanger);
			playRubble();
			break;
			
		case 2:	//This represents the State_Two
			currentState = BUILDING_STATE.STATE_TWO;
			checkState(textureStateThree);
			//changeTexture(stateChanger);
			playRubble();
			break;
		}
		return currentState;
	}
	
	private int setHealthState(float health)
	{
		if(health > average)
		{
			changeState = 0;
		}
		if(health <= average)
		{
			changeState = 1;
		}
		if(health <= lowest)
		{
			changeState = 2;
		}
		return changeState;
	}

	private void playRubble()
	{
		myP.Play();
	}
}
