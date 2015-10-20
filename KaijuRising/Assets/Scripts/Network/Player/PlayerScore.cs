using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerScore : NetworkBehaviour {
	
	public int score = 0;
	public int scoreIncrease = 10;
	
	public int endScoreAmount = 100;
	public int endKillAmount = 5;

	public Text playerScore;
	
	[Command]
	public void Cmd_increaseScore()
	{
		playerScore.text = score.ToString();
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