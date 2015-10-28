using UnityEngine;
using System.Collections;

public class Entity : DamageableEntity {

	public KaijuSounds sounds;

    protected override void Start()
    {
        base.Start();

    }

    protected void onTakeDamage()
    {
        print("Play Take Damage Animation!");
        //Modify GUI health Bar!
        //Anything else we might need?
    }

    protected void onHeal()
    {
        print("Healing kaiju!");
        //Modify GUI health Bar!
        //Anything else we might need?
    }

    public void heal(float amount)
    {
        amount = Mathf.Abs(amount); //Absolute value of something means positive
        //We know we are healing, so we should assign an 'onHeal' function to 'onModifyHealth'
        onModifyHealth = onHeal;
        modifyHealth(amount);
    }

    public void takeDamage(float amount)
    {
		//onModifyHealth += sounds.CmdPlayOnServer;	
		amount = Mathf.Abs(amount) * -1; //Make it positive and then flip it
		//We know we are damaged, so we should assign an 'onTakeDamage' function to 'onModifyHealth'
		//other.gameObject.GetComponent<TamPlayerScore>().increaseTheScore(10);
		onModifyHealth += onTakeDamage;
		modifyHealth(amount);
    }
}
