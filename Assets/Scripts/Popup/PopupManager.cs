using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public static PopupManager S = null;

    public List<Popup> popups = new List<Popup>();

    private Button blank;

    public bool isEnable = false;

    private void Awake()
    {
        S = this;

        blank = transform.GetChild(0).GetComponent<Button>();
        blank.onClick.AddListener(CloseAllPopup);
    }

    private void CloseAllPopup()
    {
        foreach(Popup popup in popups)
        {
            if(popup.isEnable == true)
            {
                popup.ClosePopup();
            }
        }
    }


    public void OnPopupsEnableChange()
    {
        int enablePopupCount = 0;

        for (int i = 0; i < popups.Count; ++i)
        {
            if(popups[i].isEnable == true)
            {
                enablePopupCount += 1;
                //Debug.Log(popups[i].gameObject.name);
            }
        }

        //Debug.Log("enablePopupCount: " + enablePopupCount);

        if (enablePopupCount == 0)
        {
            DisablePopupCanvas();
        }
        else
        {
            EnablePopupCanvas();
        }
    }

    private void EnablePopupCanvas()
    {
        //gameObject.SetActive(true);
        blank.gameObject.SetActive(true);

        isEnable = true;
    }

    private void DisablePopupCanvas()
    {
        //gameObject.SetActive(false);
        blank.gameObject.SetActive(false);

        isEnable = false;
        //Debug.Log("disable");
    }
}
