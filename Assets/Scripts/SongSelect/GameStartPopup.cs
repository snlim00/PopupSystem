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
    [SerializeField] private Button difRightButton;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button gameStart;
    #endregion

    #region �ش� �ǰ��� ����
    private string levelName;
    private List<string> difficulties = new List<string>();

    private int selectedDifIndex = 0;
    #endregion

    private const float minSpeed = 0.1f;
    private const float maxSpeed = 2f;
    private const float speedInterval = 0.1f;

    public void OpenGameStartPopup(in MusicListObject musicData)
    {
        OpenPopup();

        levelName = musicData.levelName;
        difficulties = musicData.difficulties;

        jacket.sprite = musicData.jacket.sprite;
        musicNameText.text = musicData.musicNameText.text;
        artistText.text = musicData.artist.text;
        //���� �����Ϳ��� ������ �ӵ� ���� �����ͼ� �����Ű��.
        Debug.Log("Open Game Start Popup");
    }

    public override void ClosePopup()
    {
        UserDataManager.S.SaveUserData();

        base.ClosePopup();

        Debug.Log("GameStartPopup Close");
    }

    protected override void _Awake()
    {
        base._Awake();

        spdLeftButton.onClick.AddListener(delegate { SetSpeedValue(UserData.S.noteSpeed - speedInterval); });
        spdRightButton.onClick.AddListener(delegate { SetSpeedValue(UserData.S.noteSpeed + speedInterval); });

        difRightButton.onClick.AddListener(ChangeDifficulty);

        closeButton.onClick.AddListener(ClosePopup);
        gameStart.onClick.AddListener(GameStart);

        SetSpeedValue(UserData.S.noteSpeed);
    }

    protected override void _Start()
    {
        base._Start();
    }

    #region UI ��ư �Լ�
    private void SetSpeedValue(float value)
    {
        UserData.S.noteSpeed = value;

        if(UserData.S.noteSpeed > maxSpeed)
        {
            UserData.S.noteSpeed = maxSpeed;
        }
        else if(UserData.S.noteSpeed < minSpeed)
        {
            UserData.S.noteSpeed = minSpeed;
        }

        speedValue.text = UserData.S.noteSpeed.ToString("f1");
    }

    private void ChangeDifficulty()
    {
        if(selectedDifIndex >= difficulties.Count - 1)
        {
            selectedDifIndex = 0;
        }
        else
        {
            selectedDifIndex += 1;
        }

        difficultyValue.text = difficulties[selectedDifIndex];
    }

    private void GameStart()
    {
        //asdf.GameStart(levelName, difficulties[selectedDifIndex]);
    }
    #endregion
}
