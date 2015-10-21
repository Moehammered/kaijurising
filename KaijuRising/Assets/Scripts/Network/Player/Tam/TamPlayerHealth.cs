using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TamPlayerHealth : NetworkBehaviour 
{
	public float health;
	public Slider healthUI;
	//private PlayerAnimations playAnim;
	private TamPlayerScore playScore;
	
	private void Start()
	{
		//playAnim = GetComponent<PlayerAnimations>();
		playScore = GetComponent<TamPlayerScore>();
	}
	
	public void modifyHealth(float healthModifier, int attackingPlayerNumber)
	{
		//playAnim.playTakeDamage();
		health += healthModifier;
		if (health <= 0)
		{
			GetComponent<Rigidbody>().isKinematic = true;
			Vector3 spawnPos = new Vector3(0,0,0);
			GetComponent<Rigidbody>().MovePosition (spawnPos);
			StartCoroutine (reenableCollision());
			Rpc_respawnPlayer(spawnPos);

			playScore.increaseTheScore(40, attackingPlayerNumber);
			health = healthUI.maxValue;
		}
		Rpc_updateClientHealth(health);
	}
	
	[ClientRpc]
	private void Rpc_updateClientHealth(float value)
	{
		//run function
		health = value; 
		//tell clients tell server to run to change values
		Cmd_syncVariable(health);
		healthUI.value = health;
	}
	
	[Command]
	private void Cmd_syncVariable(float syncedNumber)
	{
		//updates values on server
		health = syncedNumber; 
		//updates slider on server
		healthUI.value = health;
	}

	[ClientRpc]
	private void Rpc_respawnPlayer(Vector3 spawnPos)
	{
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().MovePosition (spawnPos);
		StartCoroutine (reenableCollision());
	}

	private IEnumerator reenableCollision()
	{
		yield return new WaitForSeconds(1f);
		GetComponent<Rigidbody>().isKinematic = false;
	}
}