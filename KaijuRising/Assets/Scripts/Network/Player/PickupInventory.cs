using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PickupInventory : NetworkBehaviour {

    public AbstractPickup[] pickups;
    public int pickupLimit = 3;

    private void prepareSlots()
    {
        pickups = new AbstractPickup[pickupLimit];
    }

    public bool storePickup(AbstractPickup pickup)
    {
        for (int i = 0; i < pickups.Length; i++)
        {
            if(pickups[i] == null) //if the slot is empty
            {
                //store the pickup in the slot and stop looking for a slot to store it
                pickups[i] = pickup;
                return true; //successfully stored the pickup
            }
        }

        return false; //if it reached here, it means no slot was free, so it could not be stored!
    }

    private void useSlot(int slot)
    {
        if (pickups[slot]) //if the slot is not null
        {
            pickups[slot].activatePickup(gameObject); //use the slot
            pickups[slot] = null; //clear the slot
        }
    }

    private void checkKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            useSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            useSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            useSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            useSlot(3);
        }
    }

	// Use this for initialization
	void Start () {
        prepareSlots();
	}
	
	// Update is called once per frame
	void Update () {
        checkKeys();
	}
}
