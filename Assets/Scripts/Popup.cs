using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Popup : MonoBehaviour
{
    private bool _isEnable = false;
    public bool isEnable
    {
        get
        {
            return _isEnable;
        }
        set
        {
            _isEnable = value;
            PopupManager.S.OnPopupsEnableChange();
        }
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        
    }

    private void Start()
    {
        _Start();
    }

    protected virtual void _Start()
    {
        PopupManager.S.popups.Add(this);

        ClosePopup();
    }

    public virtual void OpenPopup()
    {
        isEnable = true;

        gameObject.SetActive(true);
    }

    public virtual void ClosePopup()
    {
        Debug.Log("ClosePopup");
        isEnable = false;

        gameObject.SetActive(false);
    }

    public bool TogglePopup()
    {
        isEnable = !isEnable;

        if(isEnable == true)
        {
            OpenPopup();
        }
        else
        {
            ClosePopup();
        }

        return isEnable;
    }
}
