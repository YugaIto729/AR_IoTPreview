using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SurveyController : MonoBehaviour
{
    [SerializeField]
    private Text timeText;

    AppController app;

    // Start is called before the first frame update
    void Start()
    {
        app = AppController.instance;
        app.timers.Add(new SurveyTimer());
        
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = app.timers[0].GetRunTime().ToString();
    }

    public void HintBotton()
    {
        AppController.instance.timers.Add(new SurveyTimer());

        SceneManager.LoadScene("Main");
    }

    public void ExitBotton()
    {
        app.timers[0].PutTime();
        AppController.instance.Save_CSV();
        SceneManager.LoadScene("Start");
    }
}
