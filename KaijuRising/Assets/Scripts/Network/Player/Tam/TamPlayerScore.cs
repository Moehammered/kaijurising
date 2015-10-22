using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TamPlayerScore : NetworkBehaviour 
{
	private ScoreSystem scoreSystem;
	public Text playerName;
	public Canvas scoreCanvas;
	public Text[] playerScoreBoard;
	private int playerNumber;
	
	private void Start()
	{
		scoreSystem = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreSystem>();
		//		for (int i = 0; i < scoreSystem.playerText.Length; i++)
		//		{
		//			if (scoreSystem.playerText[i] == null)
		//			{
		//				scoreSystem.playerText[i] = playerScoreBoard[0];
		//				break;
		//			}
		//		}
		for (int i = 0; i < scoreSystem.scoreSystem.Length; i++)
		{
			if (scoreSystem.scoreSystem[i].player == null)
			{
				//scoreSystem.scoreSystem[i].playerName = "Player " + (i + 1).ToString(); 
				scoreSystem.scoreSystem[i].playerName = "Player " + (i + 1).ToString(); 
				scoreSystem.scoreSystem[i].player = gameObject;
				playerNumber = i;
				scoreSystem.scoreSystem[i].playerTxt = playerScoreBoard;
				initializeScoreBoard();
				playerName.text = "Player: " + (playerNumber + 1);
				break;
			}
		}
	}

	private void initializeScoreBoard()
	{
		for (int i = 0; i < scoreSystem.scoreSystem.Length; i++)
		{
			if (scoreSystem.scoreSystem[i].player != null)
			{
				scoreSystem.scoreSystem[i].player.GetComponent<TamPlayerScore>().increaseTheScore(0);
			}
		}
	}
	
	private void Update()
	{
		if (isLocalPlayer)
		{
			if (Input.GetKey(KeyCode.Tab))
			{
				displayCanvas(true);
			}
			else
			{
				displayCanvas(false);
			} 
			
			//			if (Input.GetKeyDown(KeyCode.Space))
			//			{
			//				//	scoreSystem.updateValue();
			//				Cmd_increaseScore(10);
			//			}
		}
	}

	/// <summary>
	/// Increases the score for the player the script is attached to.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void increaseTheScore(int amount)
	{
		scoreSystem.updateValue(amount, playerNumber);
	}

	/// <summary>
	/// Increases the score of the player associated with the provided ID.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void increaseTheScore(int amount, int targetPlayerNumber)
	{
		scoreSystem.updateValue(amount, targetPlayerNumber);
	}
	
	[Command]
	public void Cmd_increaseScore(int amount)
	{
		scoreSystem.updateValue(amount, playerNumber);
		//	scoreSystem.updateScore(playerNumber, amount);
	}
	
	private void displayCanvas(bool canvasStatus)
	{
		scoreCanvas.gameObject.SetActive(canvasStatus);
	}

	public int getPlayerNumber()
	{
		return playerNumber;
	}
}