using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public struct Score
{
	public string playerName;
	public GameObject player;
	public int playerScore;
	public Text[] playerTxt;
}

public class ScoreSystem : NetworkBehaviour 
{
	//public Text[] playerText = new Text[2];
	public Text gameEndText;
	public Score[] scoreSystem = new Score[4];
	// Use this for initialization

	void Start () 
	{
		
	}
	
	private void Update()
	{
	}
	
	public void updateValue(int amountIncrease, int playerIndex)
	{
		scoreSystem[playerIndex].playerScore += amountIncrease;
		for (int i = 0; i < scoreSystem.Length; i++)
		{
			if (scoreSystem[i].player == null)
			{
				break;
			}
			if (scoreSystem[i].playerTxt[playerIndex] != null)
			{
				scoreSystem[i].playerTxt[playerIndex].text = "Player " + (playerIndex + 1) + " " + scoreSystem[playerIndex].playerScore;;
			}
			else
			{
				break;
			}
		}

		//Added a print statement for end game condition ~ Sean
//		if(scoreSystem[playerIndex].playerScore >= 80)
//		{
//			print ("Player: " + (playerIndex + 1) + " has won the game");
//			gameEndText.text = "Player: " + (playerIndex + 1) + " has won the game";
//			StartCoroutine(restartWorld());
//		}

		Rpc_updateClientValue(scoreSystem[playerIndex].playerScore, playerIndex);
	}

	public void endGame()
	{
		// find the highest scoring player, add conditions for a tie.

		StartCoroutine(restartWorld());

		int winningIndex = 0;

		for(int i = 1; i < scoreSystem.Length; i++)
		{
			if(scoreSystem[i].playerScore > scoreSystem[winningIndex].playerScore)
			{
				winningIndex = i;
			}
			else if(scoreSystem[i].playerScore == scoreSystem[winningIndex].playerScore)
			{
				print ("There was a tie!");
				gameEndText.text = "There was a tie!";
				return;
			}
		}

		print ("Player: " + (winningIndex + 1) + " has won the game");
		gameEndText.text = "Player: " + (winningIndex + 1) + " has won the game";

	}

	private IEnumerator restartWorld()
	{
		yield return new WaitForSeconds(2f);
		Rpc_disconnect();
		yield return new WaitForSeconds(2f);
		GameObject.Find ("Custom Network Manager").GetComponent<CustomNetworkManager>().restartGame();
	}

	[ClientRpc]
	private void Rpc_disconnect()
	{
		//GameObject.Find ("Custom Network Manager").GetComponent<CustomNetworkManager>().client.connection.Disconnect();
		Destroy(GameObject.Find ("Custom Network Manager"));
		ClientScene.RemovePlayer(0);
		ClientScene.readyConnection.Disconnect();
	}
	
	[ClientRpc]
	private void Rpc_updateClientValue(int value, int playerIndex)
	{
		scoreSystem[playerIndex].playerScore = value;

//		if(scoreSystem[playerIndex].playerScore >= 80)
//		{
//			gameEndText.text = "Player: " + (playerIndex + 1) + " has won the game";
//		}

		for (int i = 0; i < scoreSystem.Length; i++)
		{
			if (scoreSystem[i].player == null)
			{
				break;
			}
			if (scoreSystem[i].playerTxt[playerIndex] != null)
			{
				scoreSystem[i].playerTxt[playerIndex].text = "Player " + (playerIndex + 1) + " "  + scoreSystem[playerIndex].playerScore;
			}
			else
			{
				break;
			}
		}
	}
}
