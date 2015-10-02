using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyBuilding : NetworkBehaviour {
	/*
	 * Goes onto a normal undestroyed building.
	 */ 

	[Header ("Conditions that will Destroy")]
	public	bool		explodeIndividually;
	public	KeyCode		redButton = KeyCode.X;
	public	string		kaijuTag = "Player";
	private Collision	kaijuCollision; // saved collision info to push fragments away from whatever touched it

	[Header ("Destruction")]
	public GameObject[] fragments; // The seperate fragments that make up the building.
	public Vector3 spawnOffset; // Default 0,0,0. Use if spawned fragments do not fully line up with unbroken building.

	private void explodeObject ()
	{
		if(!isServer) // If not server, do not continue.
		return;
	
		Destroy (gameObject); // Destroy normal unfractured building.
		NetworkServer.Destroy (gameObject);
		
		for(int i = 0; i < fragments.Length; i++) // Spawn all the fragments.
		{
			GameObject go = Instantiate (fragments[i], transform.position + fragments[i].transform.position + spawnOffset, fragments[i].transform.rotation) as GameObject;
			NetworkServer.Spawn (go);
			if (kaijuCollision != null)
			{
				go.GetComponent<Rigidbody>().AddForce (kaijuCollision.contacts[0].normal * 5, ForceMode.Impulse); // Adds force in the direction the player was moving.
			}
		}
	}


	// Conditions that cause explosion. Can make your own
	private void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == kaijuTag) 
		{
			kaijuCollision = other;
			explodeObject ();
		}
	}

	private void OnCollisionExit (Collision other)
	{
		if (other.gameObject.tag == kaijuTag) 
		{
			kaijuCollision = null;
		}
	}
	
	private void Update ()
	{
		debug ();
	}

	private void debug ()
	{
		if (explodeIndividually == true) 
		{
			explodeObject ();
		}
		
		if (Input.GetKeyDown (redButton)) 
		{
			explodeObject ();
		}
	}

}