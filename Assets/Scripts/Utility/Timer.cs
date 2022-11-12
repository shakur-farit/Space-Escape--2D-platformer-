using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TimerFormats
{
    Hours,
    Minutes
}

public enum SecondsFormats
{
    Whole,
    TenthDecimal,
    HundrethDecimal
}
public class Timer : MonoBehaviour
{
    [Header("Component")]
    public Text timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Timer Format Settings")]
    [Tooltip("Has we Timer Format or not. If we has Seconds Timer Format will be disabled")]
    public bool hasTimerFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    [Header("Seconds Format Settings")]
    [Tooltip("Has we Seconds Timer Format or not.")]
    public bool hasSecondsFormat;
    public SecondsFormats secondsTimeFormat;
    private Dictionary<SecondsFormats, string> secondsFormats = new Dictionary<SecondsFormats, string>();

    private int seconds;
    private int minutes;
    private int hours;

    string timerMinutesString;
    string timerHoursString;

    private void Start()
    {
        timeFormats.Add(TimerFormats.Minutes, timerMinutesString);
        timeFormats.Add(TimerFormats.Hours, timerHoursString);

        secondsFormats.Add(SecondsFormats.Whole, "0");
        secondsFormats.Add(SecondsFormats.TenthDecimal, "0.0");
        secondsFormats.Add(SecondsFormats.HundrethDecimal, "0.00");
    }

    void Update()
    {
        hours = Mathf.FloorToInt((currentTime/3600f)%24f);
        minutes = Mathf.FloorToInt((currentTime/60f)%60f);
        seconds = Mathf.FloorToInt(currentTime%60f);
        
        timerMinutesString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerHoursString = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);

        if(currentTime >= 3600)
        {
            format = TimerFormats.Hours;
        }

        if (countDown)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
        }
       
        if(currentTime < 10)
        {
            hasTimerFormat = false;
            hasSecondsFormat = true;
            secondsTimeFormat = SecondsFormats.TenthDecimal;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        if (hasTimerFormat)
        {
            hasSecondsFormat = false;
            if (format == TimerFormats.Minutes)
            {
                timerText.text = timerMinutesString;
            }
            else
            {
                timerText.text = timerHoursString;
            }
        }
        else if (hasSecondsFormat)
        {
            hasTimerFormat = false;
            timerText.text = currentTime.ToString(secondsFormats[secondsTimeFormat]);
        }
        else
            timerText.text = currentTime.ToString();
    }

}
