using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        //DebugController.inst.Debug_Log("【CSV保存】");
        //AppController.instance.Save_CSV("Test");
        //DebugController.inst.Debug_Log("【CSV保存後】");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_A()
    {
        AppController.instance.timers.Clear();
        AppController.instance.timers.Add(new SurveyTimer("BaseTimer", 0));
        AppController.instance.mode = AppController.Mode.Amateur;
        LoadScene("Survey");
    }

    public void Click_E()
    {
        AppController.instance.timers.Clear();
        AppController.instance.timers.Add(new SurveyTimer("BaseTimer", 0));
        AppController.instance.mode = AppController.Mode.Expert;
        LoadScene("Survey");
        //LoadScene("AugmentedImage");
    }

    public void LoadScene(string s_name)
    {
        SceneManager.LoadScene(s_name);
    }
}
