using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    private bool _hidden=true;
    private CanvasGroup _canvasGroup;
    private Text _tooltipText;

    void Start()
    {
        _tooltipText = GetComponentInChildren<Text>();
        _canvasGroup = GetComponent<CanvasGroup>();
        SetHidden(true);
    }


    void Update()
    {
        if(!_hidden)
            transform.position = Input.mousePosition;
    }

    public void SetHidden(bool hidden = true)
    {
        
        this._hidden = hidden;
        if (this._hidden)
        {
            _canvasGroup.alpha=0;
        }
        else
        {
            _canvasGroup.alpha=1;
        }
        
    }

    public void SetTooltipText(string text)
    {
        _tooltipText.text=text;
        SetHidden(text == "");
    }
}
