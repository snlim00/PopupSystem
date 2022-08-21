using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger S = null;

    private SceneEffectManager effectManager;

    private void Awake()
    {
        S = this;

        effectManager = FindObjectOfType<SceneEffectManager>();
    }

    private void Start()
    {
        
    }

    public void MainScene()
    {
        effectManager.ChangeScene(effectManager.Fade, () => { SceneManager.LoadScene(SCENE_NAME.MAIN); });
    }

    public void OpenSettingPopup()
    {
        SettingPopup.S.OpenPopup();
    }    
}