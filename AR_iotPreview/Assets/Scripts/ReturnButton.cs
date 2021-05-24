using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    AppController app;

    // Start is called before the first frame update
    void Start()
    {
        app = AppController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        app.timers[app.timers.Count - 1].PutTime();
        SceneManager.LoadScene("Survey");
    }
}
