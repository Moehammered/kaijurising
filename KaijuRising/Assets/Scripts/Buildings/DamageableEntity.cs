using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class DamageableEntity : NetworkBehaviour {
	
    public float health;
    public bool isDead;
	private string checkingTag;

	public ModifyHealthDelegate onModifyHealth;
	//public ModifyHealthDelegate onModifyDeath;

	public ModifyDeathDelegate onModifyDeath;
    public delegate void ModifyHealthDelegate();
	public delegate void ModifyDeathDelegate();

    protected virtual void Start()
    {
		if(isServer)
		{
			nameChecker(gameObject.tag);
			if(health <= 0)
			{
				isDead = true;
				onModifyDeath();
			}
		}
    }

	public void modifyHealth(float amount)
    {
        health += amount;
        if(onModifyHealth != null)
        {
            onModifyHealth();
        }
        else
        {
            Debug.LogWarning(this + ": OnModifyHealth Delegate is not assigned!");
        }
        //we need a callback function to run when an entity takes damage
        if(health <= 0)
        {
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
