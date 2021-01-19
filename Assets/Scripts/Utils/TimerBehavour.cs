using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavour : MonoBehaviour
{
    private Text timer;

    private float CurrentTime = 0;

    private bool startTimer = false;

    private void Awake()
    {
        timer = GetComponent<Text>();
        CurrentTime = Time.time;
    }

    private void OnEnable()
    {
        StartTime();
    }

    private void OnDisable()
    {
        StopTime();
    }

    // Update is called once per frame
    void Update()
    {

        if (startTimer) {

            float guiTime = Time.time - CurrentTime;


            float minutes = guiTime / 60;
            float seconds = guiTime % 60;

            timer.text = String.Format("{0:00}:{1:00}", minutes, seconds);

        }
      
    }

    public String GetGameTime() {
        return timer.text;
    }

    public void StartTime() {

        startTimer = true;
    }

    public void StopTime()
    {

        startTimer = false;
    }

   
}
