using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class TestSerilizeCtrl : MonoBehaviour
{
    void Start()
    {
        //SerilizeTest();
        //DeserilizerTest();
        //BinarySerTest();
        // BinaryDeSerTest();
        //ReadTestAssets();

        //AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/attack");
        //GameObject obj = GameObject.Instantiate(assetBundle.LoadAsset<GameObject>("attack"));
        //GameObject obj = GameObject.Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameDate/Prefabs/Attack.prefab"));
       TestLoadAB();
    }

    void TestLoadAB()
    {
        //TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/AssetBundleConfig.bytes");//在streamingasset下边读取是有问题的
        AssetBundle configAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath+ "/assetbundleconfig") ;
        TextAsset textAsset = configAB.LoadAsset<TextAsset>("AssetBundleConfig");
        MemoryStream stream = new MemoryStream(textAsset.bytes);
        BinaryFormatter bf = new BinaryFormatter();
        AssetBundleConfig testSerilize = (AssetBundleConfig)bf.Deserialize(stream);
        stream.Close();
        string path = "Assets/GameData/Prefabs/Attack.prefab";
        uint src = CRC32.GetCRC32(path);
        ABBase abBase = null;
        for (int i = 0; i < testSerilize.ABList.Count; i++)
        {
            if (testSerilize.ABList[i].Crc == src)
            {
                abBase = testSerilize.ABList[i];
            }

        }
        for (int i = 0; i < abBase.ABDependce.Count; i++)
        {
            AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + abBase.ABDependce[i]);
        }

        AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + abBase.ABName);
        GameObject obj = GameObject.Instantiate(assetBundle.LoadAsset<GameObject>(abBase.AssetName));

        // return testSerilize;
    }

    //void ReadTestAssets()
    //{
    //    AssetsSerilize assets = UnityEditor.AssetDatabase.LoadAssetAtPath<AssetsSerilize>("Assets/Scrips/SerilizeTest/TestAssets.asset");
    //    Debug.Log(assets.Id);
    //    Debug.Log(assets.name);
    //    foreach (var item in assets.TestList)
    //    {
    //        Debug.Log(item);
    //    }

    //}


    /// <summary>
    /// xml的序列化
    /// </summary>
    void SerilizeTest()
    {
        TestSerilize testSerilize = new TestSerilize();
        testSerilize.Id = 1;
        testSerilize.Name = "xml序列化测试";
        testSerilize.List = new List<int>();
        testSerilize.List.Add(1);
        testSerilize.List.Add(2);
        XmlSerlize(testSerilize);
    }
    void XmlSerlize(TestSerilize testSerilize)
    {
        FileStream fileStream = new FileStream(Application.dataPath + "/SerilizeTest/test.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        StreamWriter sw = new StreamWriter(fileStream, System.Text.Encoding.UTF8);
        XmlSerializer xml = new XmlSerializer(testSerilize.GetType());
        xml.Serialize(sw, testSerilize);
        sw.Close();

    }

    /// <summary>
    /// xml的反序列化
    /// </summary>
    void DeserilizerTest()
    {
        TestSerilize testSerilize = XmlDeserilize();
        Debug.Log(testSerilize.Id);
        Debug.Log(testSerilize.Name);
        foreach (var item in testSerilize.List)
        {
            Debug.Log(item);
        }
    }

    

    TestSerilize XmlDeserilize()
    {
        FileStream fs = new FileStream(Application.dataPath+ "/SerilizeTest/test.xml", FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
        XmlSerializer xs = new XmlSerializer(typeof(TestSerilize));
        TestSerilize testSerilize = (TestSerilize)xs.Deserialize(fs);
        fs.Close();
        return testSerilize; 

    }
    /// <summary>
    /// 二进制的序列化
    /// </summary>
    void BinarySerTest()
    {
        TestSerilize testSerilize = new TestSerilize();
        testSerilize.Id = 1;
        testSerilize.Name = "二进制测试";
        testSerilize.List = new List<int>();
        testSerilize.List.Add(6);
        testSerilize.List.Add(9);
        XmlSerlize(testSerilize);
        BinarySerilize(testSerilize);
    }

    void BinarySerilize(TestSerilize testSerilize)
    {
        FileStream fs = new FileStream(Application.dataPath+ "/SerilizeTest/test.bytes", FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs,testSerilize);
        fs.Close();
    }

    /// <summary>
    /// 二进制的反序列化
    /// </summary>
    //void BinaryDeSerTest()
    //{
    //    TestSerilize testSerilize = BinaryDeSerilize();
    //    Debug.Log(testSerilize.Id);
    //    Debug.Log(testSerilize.Name);
    //    foreach (var item in testSerilize.List)
    //    {
    //        Debug.Log(item);
    //    }
    //}
    //TestSerilize BinaryDeSerilize()
    //{
    //    TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/SerilizeTest/test.bytes");
    //    MemoryStream stream = new MemoryStream(textAsset.bytes);
    //    BinaryFormatter bf = new BinaryFormatter();
    //    TestSerilize testSerilize = (TestSerilize)bf.Deserialize(stream);
    //    stream.Close();

    //    return testSerilize;
    //}
}
