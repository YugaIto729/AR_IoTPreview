using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    public static DebugController inst;

    [SerializeField]
    private Text text;
    private string buffText;

    private bool displayed = false;

    [SerializeField]
    private GameObject canves;
    
    private void Awake()
    {
        inst = this;

        //text = GetComponentInChildren<Text>();
        buffText = text.text;

        DontDestroyOnLoad(gameObject);

    }

    public void Update()
    {
        text.text = buffText;
    }

    public void Debug_Log(string s)
    {
        buffText += s + "\n";
    }

    public void LogButton()
    {
        if (displayed) //表示されている
        {
            displayed = false;
            canves.SetActive(false);
        }
        else
        {
            displayed = true;
            canves.SetActive(true);
        }

    }

}
