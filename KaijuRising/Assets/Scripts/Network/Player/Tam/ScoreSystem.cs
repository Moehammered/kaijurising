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

		if(scoreSystem[playerIndex].playerScore >= 200)
		{
			print ("Player: " + (playerIndex + 1) + " has won the game");
			gameEndText.text = "Player: " + (playerIndex + 1) + " has won the game";
		}


		Rpc_updateClientValue(scoreSystem[playerIndex].playerScore, playerIndex);
	}
	
	[ClientRpc]
	private void Rpc_updateClientValue(int value, int playerIndex)
	{
		scoreSystem[playerIndex].playerScore = value;

		if(scoreSystem[playerIndex].playerScore >= 200)
		{
			gameEndText.text = "Player: " + (playerIndex + 1) + " has won the game";
		}

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
