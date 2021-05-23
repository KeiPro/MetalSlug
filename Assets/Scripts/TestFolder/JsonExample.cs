using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using System;


public class JTestClass
{
    public int i;
    public float f;
    public bool b;
    public Vector3 v;
    public string str;
    public int[] iArray;
    public List<int> iList = new List<int>();

    public JTestClass() { }

    public JTestClass(bool isSet)
    {
        if (isSet)
        {
            i = 10;
            f = 99.9f;
            b = true;
            v = new Vector3(10.0f, 20.5f, 31.4f);
            str = "JSON Test String";
            iArray = new int[] { 1, 1, 3, 5, 8, 13, 21, 34, 55 };

            for (int idx = 0; idx < 5; idx++)
            {
                iList.Add(2 * idx);
            }
        }
    }

    public void Print()
    {
        Debug.Log("i = " + i);
        Debug.Log("f = " + f);
        Debug.Log("b = " + b);

        Debug.Log("v = " + v);
        Debug.Log("str = " + str);

        for (int idx = 0; idx < iArray.Length; idx++)
        {
            Debug.Log(string.Format("iArray[{0}] = {1}", idx, iArray[idx]));
        }

        for (int idx = 0; idx < iList.Count; idx++)
        {
            Debug.Log(string.Format("iList[{0}] = {1}", idx, iList[idx]));
        }
    }
}

[Serializable]
public class UpperBody
{
    [SerializeField] string state;
    [SerializeField] List<string> position;

    public UpperBody(string state, List<string> position)
    {
        this.state = state;
        this.position = position;
    }

    public void Print()
    {
        Debug.Log(state);

        foreach (var data in position)
        {
            Debug.Log(data);
        }
    }
}

public class JsonExample : MonoBehaviour
{
    private string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    void Start()
    {
        //JTestClass jtestClass = new JTestClass(true);

        //string jsonData = ObjectToJson(jtestClass);

        string dataPath = Application.dataPath + "/Data";

        //CreateJsonFile(dataPath, "JsonTestFile", jsonData);

        //var jtc2 = LoadJsonFile<JTestClass>(Application.dataPath, "JTestClass");
        //jtc2.Print();

        UpperBody idleState = new UpperBody("Idle", new List<string>() {"0.0", "0.0"});
        UpperBody attackState = new UpperBody("Attack", new List<string>() { "1.0", "1.0" });

        string jsonData = ObjectToJson(idleState);

        CreateJsonFile(dataPath, "IdleState", jsonData);

        var idleDataTest = LoadJsonFile<UpperBody>(dataPath, "IdleState");
        idleDataTest.Print();

        //UpperAndLowerBody upperAndLowerBody = LoadJsonFile<UpperAndLowerBody>(Application.dataPath + "/Data", "UpperBodyDatas");
        //upperAndLowerBody.Print();
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
}
