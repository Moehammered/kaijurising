using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FalsolSpecialAttack : NetworkBehaviour
{
	public bool spAttackActive;
	public GameObject fireObject;
	
	// Use this for initialization
	void Start () 
	{	
		//originalDuration = duration;
		spAttackActive = false;
		fireObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void SpAttackSate(bool state)
	{
		fireObject.SetActive(state);
		spAttackActive = state;
		Rpc_SpAttackState(state);
	}
	
	[ClientRpc]
	public void Rpc_SpAttackState(bool state)
	{
		fireObject.SetActive(state);
		spAttackActive = state;
	}
}
