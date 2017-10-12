using UnityEngine;

public class DoorTrigger : Interactable
{
    private Key keyItem;

    public override void Interact()
    {
        base.Interact();

        Inventory inventory = Inventory.instance;

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].GetType() == typeof(Key))
            {
                Debug.Log("Yeee booiiii");
                keyItem = (Key)inventory.items[i];
                inventory.Pop(inventory.items[i]);
            }
        }

        if (keyItem != null)
        {
            Debug.Log("The door opens with a click.");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("The door needs a key.");
        }
    }

}
