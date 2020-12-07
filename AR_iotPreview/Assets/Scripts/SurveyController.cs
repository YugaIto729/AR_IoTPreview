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
        
        
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = app.timers[0].GetRunTime().ToString();
    }

    public void HintBotton(int level)
    {
        var ap = AppController.instance;

        Debug.Log(level);
        ap.Set_Level(level);

        ap.timers.Add(new SurveyTimer(ap.mode.ToString() + "_" + level.ToString(), ap.timers[0].GetRunTime()));

        //AppController.instance.timers[app.timers.Count - 1].PutTime();
        SceneManager.LoadScene("Main");
    }

    public void ExitBotton()
    {
        AppController.instance.timers[0].PutTime();
        AppController.instance.Save_CSV();
        SceneManager.LoadScene("Start");
    }
}
