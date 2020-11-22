using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class AppController : MonoBehaviour
{
    public static AppController instance;
    public List<SurveyTimer> timers;

    private StreamWriter sw;

    public enum Mode
    {
        Amateur, Expert
    }

    public Mode mode;
    public GameObject[] models_amateur;
    public GameObject[] models_Expert;
    private string Directory_path = "iotPre";

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timers = new List<SurveyTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float[] Get_CSVdata()
    {
        List<float> list = new List<float>();

        foreach (var t in timers)
        {
            list.Add(t.GetTime());
        }

        return list.ToArray();
    }

    public static DirectoryInfo SafeCreateDirectory(string path)
    {
        //ディレクトリが存在しているかの確認 なければ生成
        if (Directory.Exists(path))
        {
            return null;
        }
        return Directory.CreateDirectory(path);
    }

    public void Save_CSV(float[] datas)
    {
        var dc = DebugController.inst;
        dc.Debug_Log(string.Format("ファイルパス: {0}", Application.persistentDataPath));

        SafeCreateDirectory(Application.persistentDataPath + "/" + Directory_path);
        dc.Debug_Log("ディレクトリをチェック");

        sw = new StreamWriter(Application.persistentDataPath + "/" + Directory_path+"/SaveData.csv", true, Encoding.UTF8);
        dc.Debug_Log("ファイルオープン");
        string s = string.Join(",", datas);

        sw.WriteLine(s);
        dc.Debug_Log("ファイル書き込み");

        sw.Close();
        dc.Debug_Log("ファイルクローズ");
    }

    public void Save_CSV()
    {
        Save_CSV(Get_CSVdata());
    }
}

public sealed class SurveyTimer
{
    public float startTime = -1;
    public float stopTime = 0; 

    public SurveyTimer()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }
    
    public void PutTime()
    {
        stopTime = Time.time;
    }

    public void ResetTimer()
    {
        startTime = -1;
        
    }

    public float GetRunTime()
    {
        return Time.time - startTime;
    }

    public float GetTime()
    {
        if (startTime != -1)
        { 
            return stopTime - startTime;
        }

        return -1;
    }


}