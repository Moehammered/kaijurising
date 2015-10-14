using UnityEngine;
using System.Collections;

public class FalsolSpecialAttack : MonoBehaviour 
{
	//private float originalDuration;
	//public float duration;
	public bool spAttackActive;
	public GameObject fireObject;
	public PcControls pcControlsScript;

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


		if (spAttackActive == true) 
		{

			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				finishSpAttack ();
			}

			//pcControlsScript.enabled = false;
			/*duration -= Time.deltaTime;
			if(duration < 0)
			{
				finishSpAttack();
				spAttackActive = false;
				duration = originalDuration;
			}
			*/
		}
		else if (Input.GetKeyDown (KeyCode.Space)) 
		{
			startSpAttack ();
		}
	}

	private void startSpAttack ()
	{
		fireObject.SetActive(true);
		spAttackActive = true;
	}
	
	private void finishSpAttack()
	{
		spAttackActive = false;
		fireObject.SetActive (false);
		//pcControlsScript.enabled = true;
	}

}
