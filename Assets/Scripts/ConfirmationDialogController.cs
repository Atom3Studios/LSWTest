using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Container class for data need to either confirm or cancel a purchase/sale
public class ConfirmationDialogData
{
    public string Message { get; internal set; }
    public InventoryItem ItemToMove { get; internal set; }
    public Transform OriginalTransform { get; internal set; }
    public InventoryController OriginalInventory { get; internal set; }
    public int Price { get; internal set; }

    public ConfirmationDialogData()
    {
    }
}

public class ConfirmationDialogController : MonoBehaviour
{
    
    private CanvasGroup _canvasGroup;
    private ConfirmationDialogData _dialogData;
    
    
    public Image IconImage;
    public Text DialogMessage;

    public ConfirmationDialogData DialogData { get => _dialogData; set => _dialogData = value; }

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>(); HideWindow();
    }

    public void HideWindow()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
    public void ShowWindow()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void OpenDialog(ConfirmationDialogData data)
    {
        ShowWindow();
        DialogData = data;
        IconImage.sprite = data.ItemToMove.itemImage;
        DialogMessage.text = data.Message;

    }
    public void Cancel()
    {
        HideWindow();
        DialogData.ItemToMove.transform.parent = DialogData.OriginalTransform;
        DialogData.ItemToMove.transform.localPosition = Vector3.zero;
        DialogData.ItemToMove.ParentInventory = DialogData.OriginalInventory;
    }

    public void Confirm()
    {
        HideWindow();

        if (!FindObjectOfType<WalletController>().UpdateWallet(DialogData.Price))
        {
            Cancel();
        }
    }
}
