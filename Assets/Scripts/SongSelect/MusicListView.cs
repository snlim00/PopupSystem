using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicListView : MonoBehaviour
{
    [SerializeField] private GameObject musicListObjPref;

    private List<MusicListObject> musicList = new List<MusicListObject>();
    private MusicListObject firstMusic;
    private MusicListObject lastMusic;

    [SerializeField] private GameObject center;

    public bool canScroll = true;

    public bool isScroll = false;

    public MusicListObject selectedMusic = null;

    private float touchStartTime;
    private float touchStartMousePos;
    private float lastMousePos = 0;

    private bool isSliding = false;
    private float slideSpeed;

    private bool isMoveing = false;
    private Coroutine corMoveToMusic;

    [SerializeField] private Button[] playButton;

    [SerializeField] private GameStartPopup gameStartPopup;

    void Awake()
    {
        InstantiateAllMusicListObject();

        foreach (var play in playButton)
        {
            play.onClick.AddListener(delegate { gameStartPopup.OpenGameStartPopup(selectedMusic); });
        }
    }

    private void Start()
    {
        SelectSong(musicList.Count / 2, false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectSong(3);
        }
        Scroll();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SelectSong(0);
        }
    }

    #region 스크롤 및 곡 선택
    private void Scroll()
    {
        if (PopupManager.S.isEnable == true)
        {
            //StopSlide();
            SetMousePos(out lastMousePos);
            return;
        }

        //BlockBoundary();

        if(Input.GetMouseButtonUp(0))
        {
            isScroll = false;

            if(isSliding == true)
            {
                StopSlide();

                if (canScroll == true)
                    StartCoroutine(nameof(Slide));

                else
                    Debug.Log("Cannot scroll");
            }
            else if(lastMousePos != Input.mousePosition.y)
            {
                if (canScroll == true)
                    StartCoroutine(nameof(Slide));

                else
                    Debug.Log("Cannot scroll");
            }
            else
            {
                SelectSong(FindNearMusic());
            }

            //SelectSong(FindNearMusic());
        }
        else if(Input.GetMouseButtonDown(0))
        {
            isScroll = true;

            StopMoveToMusic();

            touchStartTime = Time.time;
            touchStartMousePos = Input.mousePosition.y;

            lastMousePos = Input.mousePosition.y;
        }

        float moveDistance;
        if (isScroll == true && canScroll == true)
        {
            if (canScroll == false)
            {
                Debug.Log("Cannot scroll");
                return;
            }

            moveDistance = Input.mousePosition.y - lastMousePos;

            this.transform.Translate(Vector2.up * moveDistance);

            BlockBoundary();

            SetMousePos(out lastMousePos);

            //Debug.Log("scroll");
        }
    }

    private void SetMousePos(out float mousePos)
    {
        mousePos = Input.mousePosition.y;
    }

    private bool BlockBoundary()
    {
        if(firstMusic.transform.position.y < center.transform.position.y)
        {
            SelectSong(0, false);

            return true;
        }
        else if(lastMusic.transform.position.y > center.transform.position.y)
        {
            SelectSong(musicList.Count - 1, false);

            return true;
        }
        else
        {
            return false;
        }
    }

    private void StopSlide()
    {
        StopCoroutine(nameof(Slide));

        //Debug.Log("stop slide");

        isSliding = false;
    }

    private int FindNearMusic()
    {
        float curDir;
        float minDir = Mathf.Abs(firstMusic.transform.position.y - center.transform.position.y);
        int minDirIndex = 0;

        for(int i = 1; i < musicList.Count; ++i)
        {
            curDir = Mathf.Abs(musicList[i].transform.position.y - center.transform.position.y);

            if(curDir < minDir)
            {
                minDir = curDir;
                minDirIndex = i;
            }
        }

        return minDirIndex;
    }

    public void MoveToMusic(int index, float duration)
    {
        //float dis = center.transform.position.y - musicList[index].transform.position.y;

        //Debug.Log(dis);

        //transform.Translate(Vector2.up * dis);

        StopMoveToMusic();

        corMoveToMusic = StartCoroutine(_MoveToMusic(index, duration));
    }

    public void StopMoveToMusic()
    {
        if (isMoveing == true)
        {
            isMoveing = false;
            StopCoroutine(corMoveToMusic);
        }
    }

    private IEnumerator _MoveToMusic(int index, float duration)
    {
        isMoveing = true;
        canScroll = false;

        Vector2 startPos = transform.position;
        Vector2 targetPos = transform.position + (Vector3.up * (center.transform.position.y - musicList[index].transform.position.y));

        float t = 0;

        while(t <= 1)
        {
            t += Time.deltaTime / duration;

            transform.position = Vector2.Lerp(startPos, targetPos, Utility.LerpValue(t, 1));

            yield return null;
        }

        isMoveing = false;
        canScroll = true;
    }

    private void SelectSong(int index, bool doSmoothMove = true)
    {
        StopSlide();

        if(doSmoothMove == true)
        {
            MoveToMusic(index, 0.1f);
        }
        else
        {
            float dis = center.transform.position.y - musicList[index].transform.position.y;

            //Debug.Log(dis);

            transform.Translate(Vector2.up * dis);
        }


        if (selectedMusic != null)
            musicList[selectedMusic.index].Dishighlight();

        musicList[index].Highlight();

        SetSelectedMusic(index);
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        float scrollTime = Time.time - touchStartTime;
        float scrollDis = Input.mousePosition.y - touchStartMousePos;

        float scrollSpeed = scrollDis / 50 / scrollTime ;

        float duration = Mathf.Abs(scrollDis / scrollTime / 10);

        float t = 0;

        //Debug.Log(scrollSpeed);

        while(t <= 1 && Mathf.Abs(scrollSpeed) > 1)
        {
            if (canScroll == false)
            {
                StopSlide();
                yield break;
            }

            t += Time.fixedDeltaTime / duration;

            scrollSpeed *= 0.94f;

            //Debug.Log(scrollSpeed);

            transform.Translate(0, scrollSpeed, 0);

            if (BlockBoundary() == true)
            {
                Debug.Log("block");
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        isSliding = false;
        SelectSong(FindNearMusic());
    }

    private void SetSelectedMusic(int index)
    {
        selectedMusic = musicList[index];
    }
    #endregion

    #region 곡 오브젝트 초기화
    private void InstantiateAllMusicListObject()
    {
        MusicListObject.musicListView = this;

        for (int i = 0; i < 10; ++i)
        {
            MusicListObject obj = InstantiateMusicListObject();

            obj.index = i;

            obj.transform.localPosition = Vector2.zero;

            obj.difficulties.Add("Normal");
            obj.difficulties.Add("Hard");
        }

        firstMusic = musicList[0];
        lastMusic = musicList[musicList.Count - 1];
    }

    private MusicListObject InstantiateMusicListObject()
    {
        MusicListObject musicListObj = Instantiate(this.musicListObjPref).GetComponent<MusicListObject>();

        musicListObj.transform.SetParent(this.transform);

        musicListObj.transform.localScale = Vector3.one;

        musicList.Add(musicListObj);

        return musicListObj;
    }
    #endregion

    private void StartButtonOnClick()
    {
        
    }

    
        

}
