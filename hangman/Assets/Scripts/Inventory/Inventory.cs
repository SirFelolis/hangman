using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            //            Debug.LogWarning("Too many inventories in scene.");
            return;
        }
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 10;

    public List<Item> items = new List<Item>();


    public bool Push( Item item )
    {
        if (items.Count >= space)
        {
            Debug.LogWarning("Not enough inventory space.");
            return false;
        }
        items.Add(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();

        return true;
    }

    public void Pop( Item item )
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
