using UnityEngine;
using System.Collections;
public enum KAIJU_SPECIAL_ATTACK
{
	MANTRA,		//Francesco
	KEMONO,		//Ben
	MACEDON,	//Tim
	YUM_KAAX,	//Lora
	VOKROUH,	//Aryion
	TRIKARENOS,	//Ray
	GOZU,		//Louie
	FALSOL,		//Yuki
	NO_KAIJU
};
public class specialAttackController : MonoBehaviour {

	public Rigidbody aoeAttack;
	public KAIJU_SPECIAL_ATTACK specialState;
	public GameObject aoe;


	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			Instantiate(aoe,transform.position, Quaternion.identity);
		}
		//determinState();
	}



	private void determinState()
	{
		selectedKaiju(identifyKaiju(gameObject.name));
	}


	private KAIJU_SPECIAL_ATTACK identifyKaiju(string name)
	{
		switch (name.ToLower())
		{
			case "mantra":
				return KAIJU_SPECIAL_ATTACK.MANTRA;

			case "kemono":
				return KAIJU_SPECIAL_ATTACK.KEMONO;

			case "macedon":
				return KAIJU_SPECIAL_ATTACK.MACEDON;

			case "yum_kaax":
				return KAIJU_SPECIAL_ATTACK.YUM_KAAX;

			case "vokrouh":
				return  KAIJU_SPECIAL_ATTACK.VOKROUH;

			case "trikarenos(clone)":
				return KAIJU_SPECIAL_ATTACK.TRIKARENOS;

			case "gozu":
				return KAIJU_SPECIAL_ATTACK.GOZU;

			case "falsol":
				return KAIJU_SPECIAL_ATTACK.FALSOL;

			default :
				return KAIJU_SPECIAL_ATTACK.NO_KAIJU;
		}

	}

	private void selectedKaiju(KAIJU_SPECIAL_ATTACK kaiju)
	{
		switch(kaiju)
		{
			case KAIJU_SPECIAL_ATTACK.MANTRA:
				break;

			case KAIJU_SPECIAL_ATTACK.KEMONO:
				break;

			case KAIJU_SPECIAL_ATTACK.MACEDON:
				break;

			case KAIJU_SPECIAL_ATTACK.YUM_KAAX:
				break;

			case KAIJU_SPECIAL_ATTACK.VOKROUH:
				break;

			case KAIJU_SPECIAL_ATTACK.TRIKARENOS:
				break;

			case KAIJU_SPECIAL_ATTACK.GOZU:
				break;

			case KAIJU_SPECIAL_ATTACK.FALSOL:
				break;

			default:
				return;	
		}
	}


	private void selectFunction()
	{
		//KaijuController.specialKaijuType += activateAOE;
	}

	public void activateAOE()
	{
		Instantiate(aoeAttack,transform.position, Quaternion.identity);
	}

	public void activateYumKaxxAbility()
	{

	}

	public void activateMantraAbility()
	{
	}	
}