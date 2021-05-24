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
    public int level = 0;
    [SerializeField]
    private GameObject[] _models_amateur;
    [SerializeField]
    private GameObject[] _models_Expert;
    private string Directory_path = "iotPre";
    private string userName="";

    public GameObject Models_amateur()
    {
        return _models_amateur[level];
    }

    public GameObject Models_Expert()
    {
        return _models_Expert[level];
    }

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

    public void Set_Level(int level)
    {
        if (mode == Mode.Amateur)
        {
            if (_models_amateur.Length > level)
            {
                this.level = level;
            }
        }
        else if (mode == Mode.Expert)
        {
            if (_models_Expert.Length > level)
            {
                this.level = level;
            }
        }
    }

    public string[] Get_CSVname()
    {
        List<string> list = new List<string>();

        foreach (var t in timers)
        {
            list.Add(t.name);
        }

        return list.ToArray();
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

    public void Save_CSV(string name )
    {
        var names = Get_CSVname();
        var datas = Get_CSVdata();

        var dc = DebugController.inst;
        dc.Debug_Log(string.Format("ファイルパス: {0}", Application.persistentDataPath));

        SafeCreateDirectory(Application.persistentDataPath + "/" + Directory_path);
        dc.Debug_Log("ディレクトリをチェック");

        sw = new StreamWriter(Application.persistentDataPath + "/" + Directory_path+"/"+userName+"_Data.csv", true, Encoding.UTF8);
        dc.Debug_Log("ファイルオープン");

        string s = "";
        for (int i = 0; i < timers.Count; i++)
        {
            string s1 = timers[i].name + "," + timers[i].GetTime().ToString() + "," + timers[i].timeLine + "\n";

            s += s1;
        }

        sw.WriteLine(s);
        dc.Debug_Log("ファイル書き込み");

        sw.Close();
        dc.Debug_Log("ファイルクローズ");
    }

    public void Input_Name(string s)
    {
        Debug.Log("名前セット");
        userName = s;
    }

    public void Save_CSV()
    {
        Save_CSV(userName);
    }
}

public sealed class SurveyTimer
{
    public string name;
    public float timeLine = 0;
    public float startTime = -1;
    public float stopTime = 0; 

    public SurveyTimer(string name, float startTime)
    {
        StartTimer(name, startTime);
    }

    public void StartTimer(string name, float startTime)
    {
        timeLine = startTime;
        this.name = name;
        this.startTime = Time.time;
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