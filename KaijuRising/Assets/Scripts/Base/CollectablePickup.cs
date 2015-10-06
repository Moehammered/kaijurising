using UnityEngine;
using System.Collections;

public class CollectablePickup : AbstractPickup {

    protected override void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //see if the player has an inventory?
            PickupInventory inv = col.gameObject.GetComponent<PickupInventory>();
            if(inv) //does it exist?
            {
                //store it
                storePickup(inv); //store this pickup into the pickup inventory
            }
            else //otherwise treat this pickup like a normal one!
            {
                base.OnTriggerEnter(col);
            }
        }
    }

    protected void storePickup(PickupInventory inventory)
    {
        bool stored = inventory.storePickup(this);

        if (stored)
        {
            //hide the pickup now under the player.
            Collider col = gameObject.GetComponent<Collider>();
            if (col)
            {
                col.enabled = false; //disable collisions so it won't fire off again
            }

            Renderer rend = gameObject.GetComponent<Renderer>();
            if (rend)
            {
                rend.enabled = false; //hide the mesh
            }

            transform.parent = inventory.gameObject.transform; //make it a child of the player.
        }
    }

    public override void activatePickup(GameObject target)
    {
        //does nothing, rely on things inheriting this to do it
    }
}
