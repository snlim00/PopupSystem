using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserData
{
    public static UserData S = null;

    public float bgmVolume;
    public float sfxVolume;
    public bool pushNotice;

    public float noteSpeed;
}

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager S = null;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
        {
            SaveUserData();
        }
    }

    private void Awake()
    {
        S = this;
        UserData.S = new UserData();

        ReadUserData();

        DontDestroyOnLoad(this.gameObject);
    }

    public void ReadUserData()
    {
        string json = File.ReadAllText(Application.dataPath + "/UserData.json");
        UserData.S = JsonUtility.FromJson<UserData>(json);
        Debug.Log(UserData.S.bgmVolume);
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(UserData.S);
        string path = Application.dataPath + "/UserData.json";

        File.WriteAllText(path, json);
        Debug.Log(path);
    }
}