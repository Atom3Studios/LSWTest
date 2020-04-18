using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLevelController : MonoBehaviour
{
    public InventoryController StoreInventory;

    public InventoryItem[] StoreItems;

    void Start()
    {
        //Populate some items for sale in the store
        //This would obviously need to come from either a file, a database, etc
        PopulateStore();
    }

    private void PopulateStore()
    {
        StoreInventory.SetInventory(StoreItems);
    }


}
