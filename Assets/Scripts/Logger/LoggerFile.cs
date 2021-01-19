using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class LoggerFile : MonoBehaviour
{

    [SerializeField]private bool ActiveLog;

    private static LoggerFile _instance;

    readonly string DirectoryPacth = Directory.GetCurrentDirectory() + "\\Logs";

    readonly string FilePath = Directory.GetCurrentDirectory() + "\\Logs\\Chess&CheckerFile.log";

    string ClassName { get; set; }

    string LogLevel { get; set; }

    readonly private static HashSet<string> testLine = new HashSet<string>();

    public static LoggerFile Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("LoggerFile is null");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SettingEnviroment()
    {
        //validar si el file existe 
        // si no existe crear la ruta y el file 
        if (!File.Exists(DirectoryPacth))
        {
           Directory.CreateDirectory(DirectoryPacth);
        }
        //----------------------------------------------------------------------
        // creation file if not exist 
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
        }
        //---------------------------------------------------------------------
    }

    public void DEBUG_LINE(string line)
    {
        //Debug.Log(line);
        if (!ActiveLog) {
            return;
        }

        if (!Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            return;
        }

        SettingEnviroment();

        LogLevel = "DEBUG";

        testLine.Clear();
        // read de current context 
        string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

        using (StreamReader sr = File.OpenText(FilePath))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                testLine.Add(s);
            }
        }

        testLine.Add("Level: " + LogLevel + " Time: " + dateTime + " class : " + ClassName + ".class : " + line);

        // write in file 
        using (StreamWriter fs = File.CreateText(FilePath))
        {
            // Add some text to file   
            foreach (string item in testLine)
            {
                fs.WriteLine(item);
            }
        }
    }

    public void INFO_LINE(string line)
    {

        if (!ActiveLog)
        {
            return;
        }

        Debug.Log(line);

        if (!Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            return;
        }

        SettingEnviroment();

        LogLevel = "INFO";

        testLine.Clear();
        // read de current context 
        string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

        using (StreamReader sr = File.OpenText(FilePath))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                testLine.Add(s);
            }
        }

        testLine.Add("Level: " + LogLevel + " Time: " + dateTime + " class : " + ClassName + ".class : " + line);

        // write in file 
        using (StreamWriter fs = File.CreateText(FilePath))
        {
            // Add some text to file   
            foreach (string item in testLine)
            {
                fs.WriteLine(item);
            }
        }
    }
    
    public void INFO_LINE<TypeOfValue>(string line, TypeOfValue value)
    {

        if (!ActiveLog)
        {
            return;
        }

        Debug.Log(line);

        if (!Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            return;
        }

        SettingEnviroment();

        LogLevel = "INFO";
        ClassName = value.GetType().FullName;

        testLine.Clear();
        // read de current context 
        string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

        using (StreamReader sr = File.OpenText(FilePath))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                testLine.Add(s);
            }
        }

        testLine.Add("Level: " + LogLevel + " Time: " + dateTime + " class : " + ClassName + ".class : " + line);

        // write in file 
        using (StreamWriter fs = File.CreateText(FilePath))
        {
            // Add some text to file   
            foreach (string item in testLine)
            {
                fs.WriteLine(item);
            }
        }
    }

    public void ERROR_LINE(UnityException exception, string line)
    {
        if (!ActiveLog)
        {
            return;
        }

        Debug.LogError(exception.GetBaseException().Message);

        if (!Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            return;
        }

        SettingEnviroment();

        LogLevel = "INFO";

        testLine.Clear();
        // read de current context 
        string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

        using (StreamReader sr = File.OpenText(FilePath))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                testLine.Add(s);
            }
        }

        testLine.Add("Level: " + LogLevel + " Time: " + dateTime + " class : " + ClassName + ".class : " + line.ToUpper());
        testLine.Add("Level: " + LogLevel + " Time: " + dateTime + " class : " + ClassName + ".class : " + exception.Message);

        // write in file 
        using (StreamWriter fs = File.CreateText(FilePath))
        {
            // Add some text to file   
            foreach (string item in testLine)
            {
                fs.WriteLine(item);
            }
        }
    }

}
