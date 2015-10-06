using UnityEngine;
using System.Collections;

public class DamageableEntity : MonoBehaviour {

    public float health = 200f;
    public bool isDead;

	public ModifyHealthDelegate onModifyHealth;
	//public ModifyHealthDelegate onModifyDeath;

	public ModifyDeathDelegate onModifyDeath;
    public delegate void ModifyHealthDelegate();
	public delegate void ModifyDeathDelegate();

    protected virtual void Start()
    {
        if(health <= 0)
        {
            isDead = true;
			onModifyDeath();
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

    public void kill()
    {
        health = 0;
        isDead = true;
		Destroy(gameObject);
        //callback function to run when an entity dies!
    }
}
