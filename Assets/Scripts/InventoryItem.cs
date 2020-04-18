using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,IPointerExitHandler
{
    public bool stacksUp = false;
    public int stackQuantity = 1;
    public string itemName;
    public Sprite itemImage;
    public int sellsFor, buysFor;


    public static InventoryItem CurrentDragItem;
    public static bool ItemsWereSwapped = false;
    public static Transform OriginalTransform;
    private static TooltipController _tooltipCtrl;

    private InventoryController _parentInventory;
    private Transform _dragCanvasTransform;
    private CanvasGroup _canvasGroup;

    public InventoryController ParentInventory { get => _parentInventory; set => _parentInventory = value; }

    public void Start()
    {
        if (_tooltipCtrl == null) _tooltipCtrl=FindObjectOfType<TooltipController>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _dragCanvasTransform = GameObject.Find("TopCanvas").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CurrentDragItem = this;
        _canvasGroup.blocksRaycasts = false;
        OriginalTransform = transform.parent;
        transform.parent = _dragCanvasTransform;
    }

    public void setParentInventory(InventoryController InventoryController)
    {
        ParentInventory = InventoryController;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        //If the transform parent isn't a slot item then the drag
        //didn't drop the itemn anywhere valid
        //TODO:Request Destroy item
        if (transform.parent == _dragCanvasTransform)
        {
            transform.parent = OriginalTransform;
            transform.localPosition = Vector3.zero;
        }
        //item was droped on top of another item and so the items were swaped.
        else if(ItemsWereSwapped) { 
            CurrentDragItem.transform.parent = OriginalTransform;
            CurrentDragItem.transform.localPosition = Vector3.zero;
        }
        
        OriginalTransform = null;
        CurrentDragItem = null;
        ItemsWereSwapped = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipCtrl.SetTooltipText(itemName+Environment.NewLine+"Buy Price:"+buysFor+"  Sell Value:"+sellsFor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       _tooltipCtrl.SetTooltipText("");
    }
}
