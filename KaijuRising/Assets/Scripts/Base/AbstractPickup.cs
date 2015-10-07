using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class AbstractPickup : NetworkBehaviour {

	// Use this for initialization
    public virtual void Start()
    {
        GetComponent<Collider>().isTrigger = true; //In case it is forgotten to be set
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

    protected virtual void OnTriggerEnter(Collider col)
    {
//		if(!isServer)
//			return;

        if(col.tag == "Player")
        {
            activatePickup(col.gameObject);
        }
    }

    public abstract void activatePickup(GameObject target);
}