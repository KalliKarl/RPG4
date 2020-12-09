using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public int space = 20;

    public List<Item> items = new List<Item>();
    public bool Add(Item item)
    {
        //Debug.Log("ADD Fonksiyonu");
        if (item.isDefaultItem != true){
            // Debug.Log("OK Not Default Item  its ");
            if (true) {
                if (items.Count >= space) {
                    //Debug.Log("ERROR ! Not enough room.");
                    return false;
                }
                else {
                    //Debug.Log("OK item Added = " + item);
                    items.Add(item);
                    if (onItemChangedCallBack != null)
                        onItemChangedCallBack.Invoke();

                    StaticMethods.refreshStack();
                }
            }
            
        }
        //Debug.Log("Default ITEM!!");
        return true;
    }

    public void Remove(Item item){
        Debug.Log("OK item removed = " + item);

        items.Remove(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();


        StaticMethods.refreshStack();
    }
}
