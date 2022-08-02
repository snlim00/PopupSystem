using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneEffectManager : MonoBehaviour
{
    [SerializeField] private Image coverObject;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public void ChangeScene(Action<Action> effect, Action changeScene)
    {
        effect(changeScene);
    }

    public void Fade(Action changeScene)
    {
        StartCoroutine(_Fade(changeScene));
    }

    private IEnumerator _Fade(Action changeScene)
    {
        Image cover = InstantiateCoverImage();

        cover.color = Color.black;

        DontDestroyOnLoad(cover);

        float duration = 1;

        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime / duration;

            cover.color = Utility.SetColorAlpha(cover.color, t);

            yield return null;
        }

        changeScene();

        t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime / duration;

            cover.color = Utility.SetColorAlpha(cover.color, 1 - t);

            yield return null;
        }

        Destroy(cover);
    }

    private Image InstantiateCoverImage()
    {
        Image cover = Instantiate(coverObject).GetComponent<Image>();
        cover.transform.SetParent(this.transform);

        cover.transform.position = new Vector2(Screen.width, Screen.height) * 0.5f;
        cover.transform.localScale = Vector2.one * 2;

        return cover;
    }
}
