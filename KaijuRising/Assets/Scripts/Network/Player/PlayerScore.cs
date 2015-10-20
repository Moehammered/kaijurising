using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerScore : NetworkBehaviour {
	
	public int score = 0;
	public int scoreIncrease = 10;
	
	public int endScoreAmount = 100;
	public int endKillAmount = 5;
	
	// This function was to test if the variable synced. Once ready, remove this as Cmd_increaseScore is called from other classes.
	private void Update()
	{
		if(isLocalPlayer)
		{
			if(Input.GetKeyDown (KeyCode.W))
			{
				Cmd_increaseScore();
			}
			if(score > endScoreAmount)
			{
				Cmd_endGame();
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
	
	[ClientRpc]
	public void Rpc_endGame()
	{
		if(score > endScoreAmount)
		{
			Cmd_endGame();
		}
	}
	
	[Command]
	public void Cmd_endGame()
	{
		print ("won game");
		//GameObject.FindObjectOfType<CustomNetworkManager>().StopServer();
		//NetworkServer.Reset();
	}
}