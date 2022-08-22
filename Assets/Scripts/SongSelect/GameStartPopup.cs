using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStartPopup : Popup
{
    #region UI ��ҵ�
    [SerializeField] private Image jacket;
    [SerializeField] private TMP_Text musicNameText;
    [SerializeField] private TMP_Text artistText;

    [SerializeField] private TMP_Text speedValue;
    [SerializeField] private Button spdLeftButton;
    [SerializeField] private Button spdRightButton;

    [SerializeField] private TMP_Text difficultyValue;
    [SerializeField] private Button difLeftButton;
    [SerializeField] private Button difRightButton;
    #endregion

    #region �ش� �ǰ��� ����
    private string levelName;
    private List<string> difficulties = new List<string>();

    private int selectedDifIndex = 0;
    #endregion

    public float selectedSpeed = 1f; //�ش� ���� ���� �������� speed������ ����Ǿ�� ��

    private const float minSpeed = 0.1f;
    private const float maxSpeed = 1f;

    public void OpenGameStartPopup(in MusicListObject musicData)
    {
        levelName = musicData.levelName;
        difficulties = musicData.difficulties;

        jacket.sprite = musicData.jacket.sprite;
        musicNameText.text = musicData.musicNameText.text;
        artistText.text = musicData.artist.text;
        //���� �����Ϳ��� ������ �ӵ� ���� �����ͼ� �����Ű��.

        base.OpenPopup();
    }

    protected override void _Awake()
    {
        base._Awake();

        InitSpeedValue();
    }

    private void InitSpeedValue()
    {
        speedValue.text = selectedSpeed.ToString();
    }
}
