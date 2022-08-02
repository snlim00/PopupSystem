using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingPopup : Popup
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Toggle pushNotice;

    [SerializeField] private Button creditBtn;

    protected override void Init()
    {
        //�� �������� ���� ���� �������� �� �ݿ��ϱ�.

        bgmSlider.onValueChanged.AddListener(BGMSliderChangeValue);
        sfxSlider.onValueChanged.AddListener(SFXSliderChangeValue);
        pushNotice.onValueChanged.AddListener(PushNoticeValueChange);
        creditBtn.onClick.AddListener(CreditBtnClick);
    }

    protected override void _Start()
    {
        base._Start();

        SceneChanger.S.settingPopup = this;

        bgmSlider.value = SoundManager.S.bgmVolume;
        sfxSlider.value = SoundManager.S.sfxVolume;
    }


    #region onValueChanged �Լ���.
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
        //value�� ���� �����Ϳ� �����ϱ�
    }

    private void CreditBtnClick()
    {
        //ũ���� ����
    }
    #endregion
}