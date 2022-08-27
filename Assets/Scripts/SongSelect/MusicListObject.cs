using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicListObject : MonoBehaviour
{
    public static MusicListView musicListView;

    private RectTransform rectTransform;

    public int index;

    [SerializeField] private GameObject starPref;

    [SerializeField] private GameObject infos;
    private RectTransform infoRect;

    public Image jacket;
    public Image jacketCover;
    public TMP_Text musicNameText;
    public TMP_Text artist;
    public TMP_Text stage;
    public GameObject difStars; //starPref가 들어갈 부모 오브젝트

    public string levelName;
    public List<string> difficulties = new List<string>();

    public bool isHighlight = false;

    [SerializeField] private Vector2 highlightSize;
    private Vector2 defaultSize;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        defaultSize = rectTransform.sizeDelta;

        infoRect = GetComponent<RectTransform>();

        AddStar(3);
    }

    public void SetInfos(Sprite jacket, string musicName, string artist, string stage, int difficulty)
    {
        this.jacket.sprite = jacket;
        this.musicNameText.text = musicName;
        this.artist.text = artist;
        this.stage.text = stage;

        AddStar(difficulty);
    }

    private void AddStar(int count = 1)
    {
        for (int i = 0; i < count; ++i)
        {
            GameObject star = Instantiate(starPref) as GameObject;

            star.transform.parent = difStars.transform;

            star.transform.localScale = Vector2.one;
        }
    }

    public void Highlight()
    {
        isHighlight = true;

        StartCoroutine(SetJacketSize(new Vector2(highlightSize.y, highlightSize.y), 0.15f));
        StartCoroutine(SetSize(highlightSize, 0.15f));

        difStars.SetActive(true);
        //asdf(0.14f);
    }

    private IEnumerator asdf(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        difStars.SetActive(true);
    }

    private IEnumerator SetJacketSize(Vector2 jacketTargetSize, float duration)
    {
        float t = 0;

        RectTransform jacketRect = jacket.GetComponent<RectTransform>();
        Vector2 jacketStartSize = jacketRect.sizeDelta;

        RectTransform jacketCover = this.jacketCover.GetComponent<RectTransform>();

        while (t < 1)
        {
            t += Time.deltaTime / duration;

            jacketRect.sizeDelta = Vector2.Lerp(jacketStartSize, jacketTargetSize, Utility.LerpValue(t, 1));

            jacketCover.sizeDelta = jacketRect.sizeDelta;

            yield return null;
        }
    }

    public void Dishighlight()
    {
        isHighlight = false;

        StartCoroutine(SetJacketSize(new Vector2(defaultSize.y, defaultSize.y), 0.15f));
        StartCoroutine(SetSize(defaultSize, 0.15f));
        
        difStars.SetActive(false);
    }

    private IEnumerator SetSize(Vector2 targetSize, float duration)
    {
        float t = 0;

        Vector2 startSize = rectTransform.sizeDelta;

        while (t < 1)
        {
            t += Time.deltaTime / duration;

            rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, Utility.LerpValue(t, 1));

            yield return null;
        }
    }

    //public void 
}