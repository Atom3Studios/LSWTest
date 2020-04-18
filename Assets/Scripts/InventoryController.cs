using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public bool UserOwned = false;
    public InventorySlot SlotPrefab;
    [Range(1,20)]
    public int InventoryWidth = 6,InventoryHeight = 4;
    
    
    void Start()
    {
        
        RectTransform _thisTransform = transform as RectTransform;
        _thisTransform.sizeDelta = new Vector2(InventoryWidth*32+32,InventoryHeight*32+32);
        for(int i = 0; i < InventoryHeight; i++)
        {
            for(int j=0;j<InventoryWidth; j++)
            {
                InventorySlot slot = Instantiate<InventorySlot>(SlotPrefab);
                slot.ParentInventory = this;
                slot.transform.SetParent(transform, false);
            }
        }
    }

    public void SetInventory(InventoryItem[] items)
    {
        for(int i = 0; i < items.Length; i++)
        {
            InventoryItem _item = Instantiate(items[i]);
            _item.setParentInventory(this);
            _item.transform.SetParent(transform.GetChild(i), false);
        }
    }

}
