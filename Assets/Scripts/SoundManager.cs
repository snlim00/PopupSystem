using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S = null;

    public float bgmVolume { get; private set; } = 1;
    public float sfxVolume { get; private set; } = 1;

    private AudioSource bgmSource;

    private List<AudioSource> sfxSourceList = new List<AudioSource>();

    private void Awake()
    {
        if(S == null)
        {
            S = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);

            return;
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
    }

    private void Start()
    {
        Debug.Log(UserData.S.sfxVolume);

        //각 볼륨들의 수치에 불러온 유저 데이터 적용하도록 하기.
        sfxVolume = UserData.S.sfxVolume;
        bgmVolume = UserData.S.bgmVolume;
    }

    public AudioSource PlaySFX(AudioClip clip, AudioSource source = null)
    {
        bool disposableSource = source != null ? false : true;

        if(source != null)
        {
            source.Stop();
        }
        else
        {
            source = gameObject.AddComponent<AudioSource>();

            source.playOnAwake = false;
            source.Stop();
        }

        source.clip = clip;

        source.volume = sfxVolume;

        StartCoroutine(SFXDuration(source, disposableSource));

        return source;
    }

    private IEnumerator SFXDuration(AudioSource source, bool disposableSource)
    {
        float duration = source.clip.length;

        sfxSourceList.Add(source);

        source.Play();

        yield return new WaitForSeconds(duration);

        sfxSourceList.Remove(source);

        if(disposableSource == true)
            Destroy(source);
    }

    public void PlayBGM(AudioClip clip, bool isLoop)
    {
        bgmSource.Stop();

        bgmSource.clip = clip;
        bgmSource.loop = isLoop;

        bgmSource.Play();
    }


    public void SetSFXVolume(float value)
    {
        sfxVolume = value;

        for (int i = 0; i < sfxSourceList.Count; ++i)
        {
            sfxSourceList[i].volume = sfxVolume;
        }

        //변경된 값이 유저 데이터에 반영될 수 있도록 하기
        UserData.S.sfxVolume = sfxVolume;
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;

        bgmSource.volume = bgmVolume;

        //변경된 값이 유저 데이터에 반영될 수 있도록 하기
        UserData.S.bgmVolume = bgmVolume;
    }
}