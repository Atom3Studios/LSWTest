using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private InventoryController _parentInventory;

    public InventoryController ParentInventory { get => _parentInventory; set => _parentInventory = value; }

    public void OnDrop(PointerEventData eventData)
    {
        //Item was moved to a diferent inventory. 
        if(InventoryItem.CurrentDragItem.ParentInventory != ParentInventory)
        {
            //Slot is empty which means item was either sold or bought
            if (transform.childCount == 0)
            {
                ConfirmationDialogController window = FindObjectOfType<ConfirmationDialogController>();
                ConfirmationDialogData dialogData = new ConfirmationDialogData();

                //Buying item
                if(ParentInventory.UserOwned)
                {
                    dialogData.Message=String.Format("Are you sure you want to buy this item for {0}?", InventoryItem.CurrentDragItem.buysFor);
                    dialogData.Price = -InventoryItem.CurrentDragItem.buysFor;
                }
                //selling item
                else
                {
                    dialogData.Message = String.Format("Are you sure you want to sell this item for {0}?", InventoryItem.CurrentDragItem.sellsFor);
                    dialogData.Price = InventoryItem.CurrentDragItem.sellsFor;
                }
                dialogData.OriginalTransform = InventoryItem.OriginalTransform;
                dialogData.OriginalInventory = InventoryItem.CurrentDragItem.ParentInventory;
                dialogData.ItemToMove = InventoryItem.CurrentDragItem;


                window.OpenDialog(dialogData);
                
                //item is temporarily placed in the new inventory.
                //if the player does not confirm the transaction this is reversed
                InventoryItem.CurrentDragItem.transform.parent = transform;
                InventoryItem.CurrentDragItem.transform.localPosition = Vector3.zero;
                InventoryItem.CurrentDragItem.ParentInventory = ParentInventory;
            }
            //you can't swap items between inventories
        }
        //Inside the same bag items can be swapped around
        else
        {
            InventoryItem.CurrentDragItem.transform.parent = transform;
            InventoryItem.CurrentDragItem.transform.localPosition = Vector3.zero;

            if (transform.childCount > 1)
            {
                InventoryItem.CurrentDragItem = transform.GetChild(0).GetComponent<InventoryItem>();
                InventoryItem.ItemsWereSwapped = true;
            }
        }
    }

}
