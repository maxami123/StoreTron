using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    public TaskHandler timer;
    public TextMeshProUGUI timerUI;

    private TimeSpan time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = TimeSpan.FromSeconds(timer.clock);
        timerUI.text = time.Minutes.ToString("D2") + ":" + time.Seconds.ToString("D2");
        //Debug.Log(time.Minutes.ToString("D2") + ":" + time.Seconds.ToString("D2"));
    }
}
