using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    private Inventory inventory;
    private InventorySlot[] slots;

    private Animator inventoryAnimator;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        inventoryAnimator = inventoryUI.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleShowUI();
        }
    }

    public void ToggleShowUI()
    {
        inventoryAnimator.SetBool("open", !inventoryAnimator.GetBool("open"));
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }
    }
}
