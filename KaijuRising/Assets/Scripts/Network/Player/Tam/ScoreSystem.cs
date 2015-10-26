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

		Rpc_updateClientValue(scoreSystem[playerIndex].playerScore, playerIndex);

		//Added a print statement for end game condition ~ Sean
		
		if(scoreSystem[playerIndex].playerScore >= 200)
		{
			print ("Player: " + (playerIndex + 1) + " has won the game");
			gameEndText.text = "Player: " + (playerIndex + 1) + " has won the game";
			
			StartCoroutine(restartWorld());
			
		}
	}

	private IEnumerator restartWorld()
	{
		yield return new WaitForSeconds(3f);
		Rpc_disconnect();
		yield return new WaitForSeconds(5f);
		GameObject.Find ("Custom Network Manager").GetComponent<CustomNetworkManager>().ServerChangeScene("OnlineScene");
	}

	[ClientRpc]
	private void Rpc_disconnect()
	{
		Destroy(GameObject.Find ("Custom Network Manager"));
		GameObject.Find ("Custom Network Manager").GetComponent<CustomNetworkManager>().client.connection.Disconnect();
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
