using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerScore : NetworkBehaviour {

	public int score = 0;
	public int scoreIncrease;

	// This function was to test if the variable synced. Once ready, remove this as Cmd_increaseScore is called from other classes.
	private void Update()
	{
		if(isLocalPlayer)
		{
			if(Input.GetKeyDown (KeyCode.W))
			{
				Cmd_increaseScore();
			}
		}
	}
	
	[Command]
	public void Cmd_increaseScore()
	{
		score += scoreIncrease;
		Rpc_increaseScore();
	}

	[ClientRpc]
	private void Rpc_increaseScore()
	{
		score += scoreIncrease;
	}
}