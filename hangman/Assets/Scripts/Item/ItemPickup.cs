﻿using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        bool itemWasPickedUp = false;
        itemWasPickedUp = Inventory.instance.Push(item);

        if (itemWasPickedUp)
        {
            Destroy(gameObject);
            Debug.Log("Picking up " + item.name);
        }


    }
}
