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
        //���������� �÷����� ���� ������ ������ �����ϴ� �ڵ�
    }
}
