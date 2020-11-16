using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    public static AppController instance;

    public enum Mode
    {
        Amateur, Expert
    }

    public Mode mode;
    public GameObject[] models_amateur;
    public GameObject[] models_Expert;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public sealed class SurveyTimer
{
    public float startTime = -1;
    public float[] stopTime = { 0,0,0,0,0,0,0,0,0,0}; //10個

    public void StartTimer()
    {
        startTime = Time.time;
    }
    
    public void PutTime(int id)
    {
        stopTime[id] = Time.time;
    }

    public void ResetTimer()
    {
        startTime = -1;
    }

    public float GetTime(int id)
    {
        if (startTime != -1)
        { 
            return stopTime[id] - startTime;
        }

        return -1;
    }
}