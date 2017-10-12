using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    private Item item;


    public void AddItem( Item newItem )
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
