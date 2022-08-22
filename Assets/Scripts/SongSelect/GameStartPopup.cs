using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStartPopup : Popup
{
    #region UI 요소들
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

    #region 해당 악곡의 정보
    private string levelName;
    private List<string> difficulties = new List<string>();

    private int selectedDifIndex = 0;
    #endregion

    public float selectedSpeed = 1f; //해당 값은 유저 데이터의 speed값으로 변경되어야 함

    private const float minSpeed = 0.1f;
    private const float maxSpeed = 1f;

    public void OpenGameStartPopup(in MusicListObject musicData)
    {
        levelName = musicData.levelName;
        difficulties = musicData.difficulties;

        jacket.sprite = musicData.jacket.sprite;
        musicNameText.text = musicData.musicNameText.text;
        artistText.text = musicData.artist.text;
        //유저 데이터에서 설정된 속도 값을 가져와서 적용시키기.

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
