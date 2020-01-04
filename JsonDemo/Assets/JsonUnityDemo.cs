using UnityEngine;
using System.Collections;
using System;

[Serializable] //unity内置Json在类上边要加上这句 和 System命名空间
public class Person
{
    public string name;
    public int age;

    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

[Serializable]
public class Persons
{
    public Person[] persons;
}

public class JsonUnityDemo : MonoBehaviour
{
    void Start()
    {
        //创建Json    [存储数据]
        Person person1 = new Person("李逍遥", 18);
        Person person2 = new Person("王小虎", 8);
        //person.name = "李逍遥";
        //person.age = 18;

        //string jsonStr = JsonUtility.ToJson(person);

        Persons persons = new Persons();
        persons.persons = new Person[] { person1, person2 };
        string jsonStr2 = JsonUtility.ToJson(persons);
        print(jsonStr2);

        //解析Json    [使用数据]
        Persons ps = JsonUtility.FromJson<Persons>(jsonStr2);
        foreach (Person item in ps.persons)
        {
            print(item.name);
            print(item.age);
        }
    }
}
