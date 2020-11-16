using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_A()
    {
        AppController.instance.mode = AppController.Mode.Amateur;
        LoadScene("Main");
    }

    public void Click_E()
    {
        AppController.instance.mode = AppController.Mode.Expert;
        LoadScene("Main");
        //LoadScene("AugmentedImage");
    }

    public void LoadScene(string s_name)
    {
        SceneManager.LoadScene(s_name);
    }
}
