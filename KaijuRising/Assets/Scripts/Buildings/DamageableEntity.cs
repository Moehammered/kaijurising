using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class DamageableEntity : NetworkBehaviour {
	
    public float health;
    public bool isDead;
	private string checkingTag;

	public KaijuSounds sounds;
	public GameObject destSound;
	private int value = 1;


	public ModifyHealthDelegate onModifyHealth;
	//public ModifyHealthDelegate onModifyDeath;

	public ModifyDeathDelegate onModifyDeath;
    public delegate void ModifyHealthDelegate();
	public delegate void ModifyDeathDelegate();

    protected virtual void Start()
    {
		sounds = gameObject.GetComponent<KaijuSounds>();
		destSound = Resources.Load ("Sounds/Kaijus/SFX/DestroyableSound") as GameObject;
		if(isServer)
		{
			nameChecker(gameObject.tag);
			if(health <= 0)
			{
				if(onModifyDeath != null)
				{
					isDead = true;
					onModifyDeath();
				}
			}
		}
    }

	public void chooseSound() 
	{
		if(value % 2 == 0) 
		{
			sounds.CmdPlayOnServer("groundslam1");
			value += 1;
		}
		else if(value % 2 > 0) 
		{
			sounds.CmdPlayOnServer("groundslam2");
			value += 1;
		}
	}

	public void modifyHealth(float amount)
    {
        health += amount;
        if(onModifyHealth != null)
        {
			chooseSound();
            onModifyHealth();
        }
        else
        {
            Debug.LogWarning(this + ": OnModifyHealth Delegate is not assigned!");
        }
        //we need a callback function to run when an entity takes damage
        if(health <= 0)
        {
			GameObject instance = Instantiate (destSound, transform.position, transform.rotation) as GameObject;
			NetworkServer.Spawn (instance);

			if(onModifyDeath != null)
			{
				onModifyDeath();
			}
        }
    }

	private string nameChecker(string checkingEntity)
	{
		switch(checkingEntity)
		{
			case"Player":
				break;
			case"50m":
				health = 50f;
				break;
			case"100m":
				health = 75f;
				break;
			case"150m":
				health = 100f;
				break;
			case"Building":
				break;
		}
		return checkingEntity;
	}

    public void kill()
    {
        health = 0;
        isDead = true;
		Destroy(gameObject);
        //callback function to run when an entity dies!
    }
}
