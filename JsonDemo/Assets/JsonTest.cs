using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System;
using System.Text.RegularExpressions;

public class JsonTest : MonoBehaviour
{




    void Start()
    {
        /*
        //对中文修改后的测试
        JsonData js = new JsonData();
        js["content"] = "你好";
        string st = js.ToJson();
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        string ss = reg.Replace(st, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        Debug.LogError(ss);
        */
    }

    void Update()
    {
        //1 OnGUI先创建并解析json后
        //2 读取Json数据
        if (Input.GetKeyDown(KeyCode.R))
        {
            FileData.ReadJsonData();
            
        }
        //3 修改Json数据
        if (Input.GetKeyDown(KeyCode.S))
        {
            FileData.Modify();
        }


       
    }



    string json1;
    string json2;
    string json3;

    string _name;
    int lv;
    string job;
    float exp;

    #region Json格式1

  
    //创建json  
    void CteateJson()
    {
        JsonWriter writer = new JsonWriter();
        writer.WriteObjectStart();
        writer.WritePropertyName("name");
        writer.Write("张三");
        writer.WritePropertyName("lv");
        writer.Write(1);
        writer.WritePropertyName("job");
        writer.Write("法师");
        writer.WritePropertyName("exp");
        writer.Write(1.1);
        writer.WriteObjectEnd();

        json1 = writer.ToString();
        Debug.Log(json1);
    
    }
  
    //解析Json  
    void ParsingJson()
    {
        JsonData jsonData = JsonMapper.ToObject(json1);
       
        _name = jsonData["name"].ToString();
        lv = int.Parse(jsonData["lv"].ToString());
        job = jsonData["job"].ToString();
        exp = float.Parse(jsonData["exp"].ToString());
        Debug.Log("name:" + _name);

       
        
        FileData.SaveToJsonFile(jsonData);
    }
    #endregion

    #region Json格式2
    //创建复合Json  
    void CreateCompostieJson()
    {
        JsonWriter writer = new JsonWriter();
        writer.WriteObjectStart();
        writer.WritePropertyName("name");
        writer.Write("李四");
        writer.WritePropertyName("info");
        writer.WriteObjectStart();
        writer.WritePropertyName("lv");
        writer.Write(2);
        writer.WritePropertyName("job");
        writer.Write("战士");
        writer.WritePropertyName("exp");
        writer.Write(2.2);
        writer.WriteObjectEnd();
        writer.WriteObjectEnd();
        
        json2 = writer.ToString();
        Debug.Log("json2:" + json2);

    }

    //解析复合Json  
    void ParsingCompostieJson()
    {
        JsonData jsonData = JsonMapper.ToObject(json2);
        Debug.Log(jsonData["name"].ToString());
        Debug.Log(jsonData["info"]["lv"].ToString());
        FileData.SaveToJsonFile(jsonData);
    }
    #endregion

    #region Json格式3
    //生成Json数组  
    void CreatdJsonArray()
    {
        JsonWriter writer = new JsonWriter();
        writer.WriteArrayStart();
        writer.Write("张三");
        writer.Write(1);
        writer.Write("法师");
        writer.Write(1.1);
        writer.WriteArrayEnd();

        json3 = writer.ToString();
        Debug.Log("json3:" + json3);
        //FileData.Save(json3);

    }

    //解析Json数组  
    void ParsingJsonArray()
    {
        JsonData jsonData = JsonMapper.ToObject(json3);
      Debug.Log(  jsonData[0].ToString());
        Debug.Log(jsonData[1].ToString());

        FileData.SaveToJsonFile(jsonData);

    }

    #endregion

    void OnGUI()
    {
        if (GUILayout.Button("创建Json"))
            CteateJson();
        if (GUILayout.Button("解析Json"))
            ParsingJson();
        if (GUILayout.Button("创建复合Json"))
            CreateCompostieJson();
        if (GUILayout.Button("解析复合Json"))
            ParsingCompostieJson();
        if (GUILayout.Button("创建Json数组"))
            CreatdJsonArray();
        if (GUILayout.Button("解析Json数组"))
            ParsingJsonArray();
    }




}

[Serializable]
class FileData
{

    private static string mFolderName="Test";
    private static string mFileName="mJson.json";
    private static Dictionary<string, string> Dic_Value = new Dictionary<string, string>();
    //根据ID对应的角色信息
    public static Dictionary<int, PlayerInfo> player_dic = new Dictionary<int, PlayerInfo>();

    private static string FileName
    {
        get
        {
            return Path.Combine(FolderName, mFileName);
        }
    }

    private static string FolderName
    {
        get
        {
            //测试目录，方便查看，最好存入persistens永久路径
            return Path.Combine(Application.streamingAssetsPath, mFolderName);
        }
    }

    /// <summary>
    /// 测试修改等级LV
    /// </summary>
    public static void Modify()
    {
        if (data["name"].Equals("李四"))
        {
            Debug.Log("当前lv=" + data["info"]["lv"]);
            int currentLV = 100;
            if ((data["info"]["lv"]).IsInt)
            {
                int oldLV = (int)data["info"]["lv"];
                if (currentLV > oldLV)
                {
                    data["info"]["lv"] = currentLV;
                    FileData.SaveToJsonFile(data);
                }
            }

        }
    }
  static  JsonData data;

    //读取Json数据
    public static void ReadJsonData()
    {
        if (!Directory.Exists(FolderName))
            return;
        if (!File.Exists(FileName)) return;
        
        //文件流读取
            FileStream fs = new FileStream(FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            JsonData values =JsonMapper.ToObject(sr.ReadToEnd());

            data = values;

           //关闭文件流
            if (fs!=null)
              fs.Close();
            
            if (sr != null)
              sr.Close();                   
    }
   
    //存入Json文件
    public static void SaveToJsonFile(JsonData jsonData,params object[] param)
    {

       string str=jsonData.ToJson();
        //json存入中文后会被转义成编码，使用正则显示中文
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        string modifyString = reg.Replace(str, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        Debug.LogError("正则修改后的json内容："+modifyString);
       
        if (!Directory.Exists(FolderName))
        {
            Directory.CreateDirectory(FolderName);
        }
        //文件流写入UTF8格式内容
        FileStream file = new FileStream(FileName, FileMode.Create);
        byte[] bts = System.Text.Encoding.UTF8.GetBytes(modifyString);
        file.Write(bts, 0, bts.Length);
        //关闭文件流
        if (file != null)
        {
            file.Close();
        }
    }

  

    
   
    

    
    
}

[Serializable]
class PlayerInfo
{
    public int id { get;private set; }
    public string name { get; set; }
    public string job { get;private set; }
    public string mission { get; private set; }

    public PlayerInfo(int id,string name,string job,string mission)
    {
        this.id = id;
        this.name = name;
        this.job = job;
        this.mission = mission;

    }

}
