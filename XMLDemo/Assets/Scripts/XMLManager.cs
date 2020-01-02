using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;   //*
using System.IO;    //*

public class XMLManager : MonoBehaviour {

    //①[unity编辑器模式]
    private string XMLPath_In_Eidtor = "Assets/XMLData/XMLDemo.xml";

    //②[PC模式]        在[unity编辑器模式]的基础上修改读取XML文档的路径即可
    //private string XMLPath_In_PC = Application.dataPath + "/XMLData/XMLDemo.xml";

    //三[APK模式]
    private string XMLPath_In_APK = Application.persistentDataPath + "/XMLDemo.xml";
    //XML文档内容
    private string contents = "<RootNode><ChildNode><SunNode>4</SunNode><SunNode1>4</SunNode1></ChildNode><ChildNode1><SunNode2>4</SunNode2><SunNode3>4</SunNode3></ChildNode1><ChildNode2><SunNode4>1</SunNode4><SunNode5>15</SunNode5></ChildNode2></RootNode>";
    //静态数据
    private string staticXMLContents;

    void Start () {
        //①[unity编辑器模式]
        //Test_UnityEditorMode();

        //②[PC模式]
        //Test_PCMode();

        //三[APK模式]
        //Test_APKMode();
    }

    /// <summary>
    /// 测试数据[APK模式]
    /// </summary>
    private void Test_APKMode()
    {
        //静态配置数据  路径加载[Editor]-->内容加载[APK]
        staticXMLContents = Resources.Load("XMLStaticDate").ToString();//静态数据存储在Resources文件夹下 staticXML:xml文档的内容
        //存档文件  使用方式不变，在[unity编辑器模式]的基础上修改读取XML文档的路径即可
        if (!File.Exists(XMLPath_In_APK))//如果还没有该文件，新建一个
        {
            File.WriteAllText(XMLPath_In_APK, contents);
        }

        //测试开始
        string xmlData;

        //-----------------------------静态配置数据---------------------------------
        //读取所有信息(含格式)
        xmlData = ReadXMLFile_AllInfo_In_Editor(staticXMLContents, "APK");
        //读取单个节点信息
        xmlData = ReadXMLFile_NodeInfo_In_Editor(staticXMLContents, "RootNode/ChildNode", "APK");
        xmlData = ReadXMLFile_NodeInfo_In_Editor(staticXMLContents, "SunNode3", "RootNode", "APK");

        //-----------------------------存档文件数据---------------------------------
        //修改路径：         XMLPath_In_Eidtor --> XMLPath_In_APK


        Debug.Log(xmlData);
    }

    /// <summary>
    /// 测试数据[PC模式]
    /// </summary>
    private void Test_PCMode()
    {
        //在[unity编辑器模式]的基础上修改读取XML文档的路径即可
        //修改路径：
        //XMLPath_In_Eidtor --> XMLPath_In_PC
    }

    /// <summary>
    /// 测试数据[unity编辑器模式]
    /// </summary>
    private void Test_UnityEditorMode()
    {
        string xmlData;

        //----------------------[unity编辑器模式]-------------------
        //读取所有信息(含格式)
        xmlData = ReadXMLFile_AllInfo_In_Editor(XMLPath_In_Eidtor);
        //读取单个节点信息
        xmlData = ReadXMLFile_NodeInfo_In_Editor(XMLPath_In_Eidtor, "RootNode/ChildNode");
        xmlData = ReadXMLFile_NodeInfo_In_Editor(XMLPath_In_Eidtor, "SunNode3", "RootNode");
        //写入单个节点信息
        WriteXMLFile_NodeInfo_In_Editor(XMLPath_In_Eidtor, "RootNode", "SunNode2", "this is a new value");
        xmlData = ReadXMLFile_NodeInfo_In_Editor(XMLPath_In_Eidtor, "SunNode2", "RootNode");
        //写入所有节点信息
        string tempWrite_AllInfo_Value = "4|4|4|4";
        xmlData = WriteXMLFile_AllInfo_In_Editor(XMLPath_In_Eidtor, "RootNode", tempWrite_AllInfo_Value);

        Debug.Log(xmlData);
    }

    #region [unity编辑器模式] & [PC模式] & [APK模式]

    #region read读取
    /// <summary>
    /// 读取XML里的所有信息[unity编辑器模式]
    /// </summary>
    /// <param name="path">文件路径</param>
    public string ReadXMLFile_AllInfo_In_Editor(string path, string mode = "Editor")
    {
        string contents = "";

        //操作类
        XmlDocument doc = new XmlDocument();//xml文件
        //选择模式
        if (mode == "Editor") doc.Load(path);//Editor模式 & PC模式
        else if (mode == "APK") doc.LoadXml(path);//APK模式
        //获取根节点
        XmlNode root = doc.SelectSingleNode("RootNode");

        //按照XML格式保存内容
        contents += "<" + root.Name + ">";
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            contents += "\n\n   <" + node.Name + ">";
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                contents += "\n\n       <" + node.ChildNodes[i].Name + ">";
                contents += node.ChildNodes[i].InnerText;
                contents += "</" + node.ChildNodes[i].Name + ">";
            }
            contents += "\n\n   </" + node.Name + ">";
        }
        contents += "\n\n</" + root.Name + ">";

        //返回内容
        return contents;
    }
    /// <summary>
    /// 读取XML里的指定节点信息通过节点路径[unity编辑器模式]
    /// </summary>
    /// <param name="path">文件路径</param>
    public string ReadXMLFile_NodeInfo_In_Editor(string path, string nodePath = "RootNode", string mode = "Editor")
    {
        string contents = "";

        XmlDocument doc = new XmlDocument();//xml文件
        //选择模式
        if (mode == "Editor") doc.Load(path);//Editor模式 & PC模式
        else if (mode == "APK") doc.LoadXml(path);//APK模式

        XmlNode root = doc.SelectSingleNode(nodePath);
        contents = root.InnerText;

        return contents;//返回内容
    }
    /// <summary>
    /// 读取XML里的指定节点信息[unity编辑器模式]
    /// </summary>
    /// <param name="path">文件路径</param>
    public string ReadXMLFile_NodeInfo_In_Editor(string path, string nodeName, string rootNodeName, string mode = "Editor")
    {
        string contents = "";

        XmlDocument doc = new XmlDocument();//xml文件
        //选择模式
        if (mode == "Editor") doc.Load(path);//Editor模式 & PC模式
        else if (mode == "APK") doc.LoadXml(path);//APK模式

        XmlNode root = doc.SelectSingleNode(rootNodeName);

        //if (nodeName == rootNodeName)//根节点内容
        //{
        //    contents = root.InnerText;
        //    return contents;//返回内容
        //}
        //else
        //{
        //    XmlNodeList nodeList = root.ChildNodes;
        //    foreach (XmlNode node in nodeList)
        //    {
        //        if (node.Name == nodeName)//子节点内容
        //        {
        //            contents = node.InnerText;
        //            return contents;
        //        }
        //        else if (node.ChildNodes.Count > 0)
        //        {
        //            for (int i = 0; i < node.ChildNodes.Count; i++)
        //            {
        //                if (node.ChildNodes[i].Name == nodeName)//孙节点内容
        //                {
        //                    contents = node.ChildNodes[i].InnerText;
        //                    return contents;
        //                }
        //            }
        //        }
        //    }
        //}

        XmlNode tempNode = Help_ReadXMLFile_NodeInfo_In_Editor(root, nodeName);
        contents = tempNode.InnerText;

        return contents;//返回内容
    }
    /// <summary>
    /// 递归判断是否有子节点以及是否匹配
    /// </summary>
    private XmlNode Help_ReadXMLFile_NodeInfo_In_Editor(XmlNode node, string nodeName)
    {
        if(node.Name == nodeName)//自身
        {
            return node;
        }
        else if(node.ChildNodes.Count > 0)//子节点
        {
            foreach (XmlNode item in node)
            {
                XmlNode tempNode = Help_ReadXMLFile_NodeInfo_In_Editor(item, nodeName);
                if (tempNode == null) continue;
                else
                    return tempNode;
            }
        }
        return null;//该节点以及下没有匹配的节点
    }
    #endregion

    #region write写入
    /// <summary>
    /// 写入指定节点信息[unity编辑器模式]
    /// </summary>
    public void WriteXMLFile_NodeInfo_In_Editor(string path, string rootNodeName, string nodeName, string value)
    {
        //获取操作权 和 根节点
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode(rootNodeName);
        //匹配节点
        XmlNode tempNode = Help_ReadXMLFile_NodeInfo_In_Editor(root, nodeName);
        //赋与新值
        tempNode.InnerText = value;
        //更新XML文件
        doc.Save(path);
    }
    /// <summary>
    /// 写入所有节点信息[unity编辑器模式]
    /// </summary>
    /// <param name="value">内容用'|'分割</param>
    public string WriteXMLFile_AllInfo_In_Editor(string path, string rootNodeName, string value)
    {
        //获取操作权 和 根节点
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode(rootNodeName);
        //分割且保存信息
        List<string> valueList = new List<string>();
        string[] valueArray = value.Split('|');
        for (int i = 0; i < valueArray.Length; i++)
        {
            valueList.Add(valueArray[i]);
        }
        //更新叶节点信息，返回已更新的叶节点数
        int newLeafNodeCount;
        int leafNodeCount = Help_WriteXMLFile_AllInfo_In_Editor(root, valueList, out newLeafNodeCount);
        //更新XML文件
        doc.Save(path);
        //返回叶节点数 和 已更新叶节点数
        return "叶节点数:"+ leafNodeCount + "\n已更新叶节点数:" + newLeafNodeCount;
    }
    /// <summary>
    /// 递归判断是否有子节点以及是否匹配
    /// </summary>
    private int Help_WriteXMLFile_AllInfo_In_Editor(XmlNode node, List<string> valueList, out int newLeafNodeCount)
    {
        int leafNodeCount = 0;
        newLeafNodeCount = 0;

        if (node.ChildNodes.Count == 0)//leaf node
        {
            if (valueList.Count > 0)//have new value
            {
                node.InnerText = valueList[0];
                valueList.RemoveAt(0);
                newLeafNodeCount++;//new leaf node count add (info update)
            }
            leafNodeCount++;//leaf node count add
        }
        else//not leaf node
        {
            foreach (XmlNode item in node)
            {
                int tempNewLeafNodeCount = 0;
                int tempLeafNodeCount = Help_WriteXMLFile_AllInfo_In_Editor(item, valueList, out tempNewLeafNodeCount);
                leafNodeCount += tempLeafNodeCount;
                newLeafNodeCount += tempNewLeafNodeCount;
            }
        }

        return leafNodeCount;
    }
    #endregion

    #endregion
}
