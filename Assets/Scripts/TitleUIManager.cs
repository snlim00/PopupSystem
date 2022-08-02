using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text lastPlayedSong;

    // Start is called before the first frame update
    void Start()
    {
        SetLastPlayedSong();
    }

    private void SetLastPlayedSong()
    {
        //마지막으로 플레이한 곡의 제목을 가져와 적용하는 코드
    }
}
