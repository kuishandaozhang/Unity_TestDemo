using UnityEngine;
using System.Collections;
using LitJson;

public class Hero
{
    public string name;
    public int age;
    /*
    public Hero(string name, int age)
    {
        this.name = name;
        this.age = age;
    }*/
}

public class Heros
{
    public Hero[] heros;
}

public class JsonPlugin : MonoBehaviour
{
	void Start () 
    {
        //Func1();
        Func2();
	}

    void Func1()
    {
        
        //创建
        Hero hero1 = new Hero();
        Hero hero2 = new Hero();
        //Hero hero1 = new Hero("超人", 40);
        //Hero hero2 = new Hero("闪电侠", 40);

        
        hero1.name = "超人";
        hero1.age = 40;
        hero2.name = "闪电侠";
        hero2.age = 40;
        
        Heros heros = new Heros();
        heros.heros = new Hero[] { hero1, hero2 };

        string jsonStr = JsonMapper.ToJson(heros);
        print(jsonStr);
        //解析 {"heros":[{"name":"超人","age":40},{"name":"闪电侠","age":40}]}
        //string jsongStrTest = "{'heros':[{'name':'\u8D85\u4EBA','age':90},{'name':'\u8759\u8760\u4Fa0','age':80}]}";
        Heros hs = JsonMapper.ToObject<Heros>(jsonStr);
        //Debug.Log(hs.heros[0].name);
        /*
        string jsonStr2 = JsonMapper.ToJson(hs);
        print(jsonStr2);
        */

        /*                {"heros":[{"name":"超人","age":40},{"name":"闪电侠","age":40}]}
        string jsonStr = "{"heros":[{"name":"超人","power":90},{"name":"闪电侠","power":80}]}";
        JsonData heroJd= JsonMapper.ToObject(jsonStr);
        JsonData heros = heroJd["heros"];
        foreach (JsonData hero in heros)
        {
            Debug.Log(hero["name"].ToString());
            Debug.Log((int)hero["power"]);
        }
        */
    }

    void Func2()
    {
        //{"name":"超人", "power":"90"}
        /*
        JsonData jd = new JsonData();
        jd["name"] = "超人";
        jd["power"] = 90;
        print(jd.ToJson());
        */
        /*
        //{"heros":[{"name":"超人", "power":90}, {"name":"闪电侠", "power":80}]}
        JsonData hero1 = new JsonData();
        hero1["name"] = "超人";
        hero1["power"] = 90;
        JsonData hero2 = new JsonData();
        hero2["name"] = "闪电侠";
        hero2["power"] = 80;
        JsonData herosArray = new JsonData();
        herosArray.Add(hero1);
        herosArray.Add(hero2);
        JsonData heros = new JsonData();
        heros["heros"] = herosArray;

        string herosStr = JsonMapper.ToJson(heros);
        print(herosStr);
        */

        string jsonStr = "{'heros':[{'name':'超人','power':90},{'name':'闪电侠','power':80}]}";
        JsonData heroJd = JsonMapper.ToObject(jsonStr);
        JsonData heros = heroJd["heros"];
        heros.SetJsonType(JsonType.Array);
        Debug.Log(heros[0]["name"]);
        Debug.Log(heros[0]["power"]);
        Debug.Log(heros[1]["name"]);
        Debug.Log(heros[1]["power"]);
        /*
        foreach (JsonData hero in heros)
        {
            
            Debug.Log(hero["name"]);
            Debug.Log(hero["power"]);
            
        }*/
        
    }
}
