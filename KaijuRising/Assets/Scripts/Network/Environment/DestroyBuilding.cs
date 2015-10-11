using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Entity))]
//Goes onto a normal undestroyed building
public class DestroyBuilding : NetworkBehaviour 
{
	[Header ("Conditions that will Destroy")]
	public	bool		explodeIndividually;
	public	KeyCode		redButton = KeyCode.X;
	public	string		kaijuTag = "Player";
	// saved collision info to push fragments away from whatever touched it
	private Collision	kaijuCollision; 
	private Entity		buildingStatus;
	
	[Header ("Destruction")]
	// The seperate fragments that make up the building.
	public GameObject[] fragments; 
	// Default 0,0,0. Use if spawned fragments do not fully line up with unbroken building.
	public Vector3		spawnOffset; 
	
	[Header ("Effects")]
	public GameObject	rubble;
	public float		rubbleYPosition = -19;
	public GameObject	smokeParticle;
	public GameObject	explosionParticle;
	//Explosion uses the gameobejcts transform position. If this position is not in the centre of the object
	//Then explosion needs to be offset into the centre
	public Vector3		explosionOffset;

	//[Command]
	public void Cmd_explodeObject ()
	{
		//if(!isServer) // If not server, do not continue.
		//return;
		
		Destroy (gameObject); // Destroy normal unfractured building.
		NetworkServer.Destroy (gameObject);
		createGroundObjects ();
		createExplosion ();
		spawnFragments ();
	}
	
	private void spawnFragments ()
	{
		// Spawn all the fragments.
		for(int i = 0; i < fragments.Length; i++) 
		{
			GameObject go = Instantiate (fragments[i], transform.position + fragments[i].transform.position + spawnOffset, fragments[i].transform.rotation) as GameObject;
			NetworkServer.Spawn (go);
			
			// Adds force in the direction the player was moving.
			if (kaijuCollision != null)
			{
				go.GetComponent<Rigidbody>().AddForce (kaijuCollision.contacts[0].normal * 5, ForceMode.Impulse); 
			}
			if(buildingStatus != null)
			{
				go.GetComponent<Rigidbody>().AddForce (Vector3.up * 5, ForceMode.Impulse); // Adds force in the direction the player was moving.
			}
		}
	}
	
	//Make rubble and smoke which are at the ground position
	private void createGroundObjects ()
	{
		Vector3 groundPosition = new Vector3 (transform.position.x, rubbleYPosition, transform.position.z);
		
		if (rubble != null) 
		{
			GameObject rubbleObj = Instantiate (rubble, groundPosition, rubble.transform.rotation) as GameObject;
			NetworkServer.Spawn (rubbleObj);
		}
		if (smokeParticle != null) 
		{
			GameObject smokeObj = Instantiate (smokeParticle, groundPosition, smokeParticle.transform.rotation) as GameObject;
			NetworkServer.Spawn (smokeObj);
		}
	}
	
	private void createExplosion ()
	{
		if (explosionParticle != null)
		{
			GameObject explosionObj = Instantiate (explosionParticle, transform.position + explosionOffset, explosionParticle.transform.rotation) as GameObject;
			NetworkServer.Spawn (explosionObj);
		}
	}
	
	private void onDeath()
	{
		buildingStatus.onModifyDeath += Cmd_explodeObject;
	}
	
	// Conditions that cause explosion. Can make your own
	
	private void Start()
	{
		buildingStatus = GetComponent<Entity>();
		onDeath();
	}

	private void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == kaijuTag)
		{
			kaijuCollision = other;
			Cmd_explodeObject ();
		}
	}

}