using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeSystem : MonoBehaviour
{
    public bool isPause = false;

    private float time = 0;
    public float day = 0;

    private float dayPerSec = 86400f; //하루를 초로 나타낸 것
    private float dayTimeScale = 480f;
    private float nightTimeScale = 1440f;

    public UnityEvent onDay;
    public UnityEvent onNight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Timer()
    {
        while(true)
        {
            if (isPause == true)
                yield return null;

            while(time < 28800)
            {
                if (isPause == true)
                    yield return null;

                time += Time.deltaTime * dayTimeScale;
                yield return null;
            }
            onNight.Invoke();

            while(time < dayPerSec)
            {
                if (isPause == true)
                    yield return null;

                time += Time.deltaTime * nightTimeScale;
                yield return null;
            }
            day += 1;
            onDay.Invoke();
        }
    }
}
