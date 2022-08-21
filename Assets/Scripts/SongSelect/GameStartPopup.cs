using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStartPopup : Popup
{
    [SerializeField] private Image jacket;
    [SerializeField] private TMP_Text musicNameText;
    [SerializeField] private TMP_Text artistText;

    [SerializeField] private TMP_Text SpeedValue;
    [SerializeField] private Button SpdLeftButton;
    [SerializeField] private Button SpdRightButton;

    [SerializeField] private TMP_Text DifficultyValue;
    [SerializeField] private Button DifLeftButton;
    [SerializeField] private Button DifRightButton;

    private string levelName;
    private List<string> difficulties = new List<string>();

    private int selectedDifIndex = 0;

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
}
