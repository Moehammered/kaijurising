using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public enum ATTACK_KAIJU_TYPE
{
	YUM_KAAX,
	FALSOL,
	VOKROUH,
	TRIKARENOS
};

public class TamPlayerAttack : NetworkBehaviour 
{	
	public ATTACK_KAIJU_TYPE KAIJU_TYPE;
	public string[] tags;
	public float attackRadius;
	public GameObject attackCenter;
	public float attackDamage;
	public float specialDamage;
	private TamPlayerScore playerScore;
	private int attackCounter;
	public float specialTimer;
	private bool canSpecial = true;
	public PulseButton pulse;
	[Header("YumKaax")]
	public GameObject tendrils;
	public float tendrilRadius;
	public float tendrilDelay;
	[Header("Falsol")]
	public FalsolSpecialAttack fireParticle;
	public GameObject falsolProjectile;
	public Transform breathCenter;
	public float duration;
	public float delay;
	public float fireRate;
	[Header("Vorkouh")]
	public float specialRadius;
	
	[Command]
	private void Cmd_detectObjects(Vector3 center, float radius, float damage)
	{
		Collider[] colliders = Physics.OverlapSphere(center, radius);
		for(int i=0; i<colliders.Length; i++)
		{
			if(colliders != null)
			{
				for(int z=0; z< tags.Length; z++)
				{
					if(colliders[i].gameObject.tag == tags[z])
					{
						dealDamageTowardsBuildings(colliders[i].gameObject);
					}
				}
				if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag == "Player")
				{
					colliders[i].gameObject.GetComponent<TamPlayerHealth>().modifyHealth(-damage, GetComponent<TamPlayerScore>().getPlayerNumber());
				}
			}
		}	
	}
		
	private void Start()
	{
		playerScore = GetComponent<TamPlayerScore>();
	}
	
	public void normalAttack()
	{
		Cmd_detectObjects(attackCenter.transform.position, attackRadius, attackDamage);
	}
	
	public void specialAttack()
	{
		if (canSpecial)
		{
			switch(KAIJU_TYPE)
			{
				case ATTACK_KAIJU_TYPE.YUM_KAAX:
					Cmd_detectPlayers(specialDamage, tendrilRadius);
					break;
				case ATTACK_KAIJU_TYPE.FALSOL:
					Cmd_falsolSpecial();
					break;
				case ATTACK_KAIJU_TYPE.VOKROUH:
					Cmd_detectObjects(attackCenter.transform.position, attackRadius + 10, specialDamage);
					//Cmd_detectMultiplePlayers(specialRadius, specialDamage);
					break;
				case ATTACK_KAIJU_TYPE.TRIKARENOS:
					Cmd_detectObjects(attackCenter.transform.position, attackRadius + 10, specialDamage);
					break;
				default:
					Cmd_detectObjects(attackCenter.transform.position, attackRadius, specialDamage);
					break;
			}
			StartCoroutine(specialCoolDown());
		}
	}
	
	public void timedNormalAttack(float duration)
	{
		StartCoroutine(timedAttack(duration));
	}
	
	private IEnumerator timedAttack(float duration)
	{
		float timer = duration;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		normalAttack();
	}
	
	private void dealDamageTowardsBuildings(GameObject collidedObject)
	{
		Entity building = collidedObject.GetComponent<Entity>();
		if(building)
		{
			building.takeDamage(attackDamage);
			building = null;
		}
	}
	
	//YUMKAAX
	[Command]
	private void Cmd_detectPlayers(float damage, float specialRadius)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, specialRadius);
		for(int i=0; i<colliders.Length; i++)
		{
			if(colliders != null)
			{
				if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag == "Player")
				{
					StartCoroutine(delaySpawn(tendrilDelay, specialDamage, colliders[i].gameObject));
					break;
				}
			}
		}	
	}
	
	//vok
	[Command]
	private void Cmd_detectMultiplePlayers(float damage, float specialRadius)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, specialRadius);
		for(int i=0; i<colliders.Length; i++)
		{
			if(colliders != null)
			{
				if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag == "Player")
				{
					colliders[i].gameObject.GetComponent<TamPlayerHealth>().modifyHealth(-damage, GetComponent<TamPlayerScore>().getPlayerNumber());
				}
			}
		}
	}
	
	
	private IEnumerator delaySpawn(float delay, float damage, GameObject enemy)
	{
		while(delay > 0)
		{
			delay -= Time.deltaTime;
			yield return null;
		}
		enemy.GetComponent<TamPlayerHealth>().modifyHealth(-damage, GetComponent<TamPlayerScore>().getPlayerNumber());
		GameObject tendril_GO = (GameObject)Instantiate(tendrils, enemy.transform.position, Quaternion.identity);
		NetworkServer.Spawn(tendril_GO);
	}
	
	//FALSOL
	[Command]
	private void Cmd_falsolSpecial()
	{
		StartCoroutine(fireBreath(duration, fireRate, delay));
	}
	
	private IEnumerator fireBreath(float duration, float fireRate, float delay)
	{
		float timer = duration;
		float currentFireRate = 0;
		while(timer > 0)
		{
			if (delay > 0)
			{
				delay -= Time.deltaTime;
			}
			else
			{
				fireParticle.SpAttackSate(true);
				timer -= Time.deltaTime;
				if (currentFireRate > 0)
				{
					currentFireRate -= Time.deltaTime;
				}
				else
				{
					GameObject fireBreath_GO = (GameObject)Instantiate(falsolProjectile, breathCenter.position, transform.rotation);
					FireBreath fireBreathComp = fireBreath_GO.GetComponent<FireBreath>();
					fireBreathComp.damage = specialDamage;
					fireBreathComp.direction = transform.forward;
					fireBreathComp.attackingPlayer = GetComponent<TamPlayerScore>().getPlayerNumber();
					NetworkServer.Spawn(fireBreath_GO);
					currentFireRate = fireRate;
				}
			}
			yield return null;
		}
		fireParticle.SpAttackSate(false);
	}
	
	private IEnumerator specialCoolDown()
	{
		float timer = specialTimer;
		pulse.specialReady = false;
		canSpecial = false;
		while(timer > 0)
		{
			timer -= Time.deltaTime;
			yield return null;
		}
		canSpecial = true;
		pulse.specialReady = true;
	}
	
	public bool checkCanSpecial()
	{
		return canSpecial;
	}
}