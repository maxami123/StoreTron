using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    //public TaskHandler timer;
    public TextMeshProUGUI timerUI;
    public float startTime;
    private float clock;
    private TimeSpan time;
    // Start is called before the first frame update
    void Start()
    {
        clock = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        clock -= Time.deltaTime;

        time = TimeSpan.FromSeconds(clock);
        timerUI.text = time.Minutes.ToString("D2") + ":" + time.Seconds.ToString("D2");
        //Debug.Log(time.Minutes.ToString("D2") + ":" + time.Seconds.ToString("D2"));
    }
}
