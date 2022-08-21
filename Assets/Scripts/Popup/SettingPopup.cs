using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingPopup : Popup
{
    public static SettingPopup S = null;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Toggle pushNotice;

    [SerializeField] private Button creditBtn;

    protected override void _Awake()
    {
        S = this;

        //각 설정들의 값에 유저 데이터의 값 반영하기.

        bgmSlider.onValueChanged.AddListener(BGMSliderChangeValue);
        sfxSlider.onValueChanged.AddListener(SFXSliderChangeValue);
        pushNotice.onValueChanged.AddListener(PushNoticeValueChange);
        creditBtn.onClick.AddListener(CreditBtnClick);
    }

    protected override void _Start()
    {
        base._Start();

       // SceneChanger.S.settingPopup = this;

        bgmSlider.value = SoundManager.S.bgmVolume;
        sfxSlider.value = SoundManager.S.sfxVolume;
    }


    #region onValueChanged 함수들.
    private void BGMSliderChangeValue(float value)
    {
        SoundManager.S.SetBGMVolume(value);
    }

    private void SFXSliderChangeValue(float value)
    {
        SoundManager.S.SetSFXVolume(value);
    }

    private void PushNoticeValueChange(bool value)
    {
        //value를 유저 데이터에 적용하기
    }

    private void CreditBtnClick()
    {
        //크레딧 연출
    }
    #endregion
}
