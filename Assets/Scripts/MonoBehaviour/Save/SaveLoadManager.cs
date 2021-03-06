﻿using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;
    public int num;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        LoadData();
    }

    public void OnDisable()
    {
        SaveData();
    }

    // SAVE DATA --------------------------------
    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerInfo.dat");

        Test test = new Test();
        test.num = num;
        
        bf.Serialize(file, test);
        file.Close();
    }

    // LOAD DATA --------------------------------
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
            Test test = (Test)bf.Deserialize(file);
            file.Close();

            num = test.num;
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //        SaveData();

    //    else if (Input.GetKeyDown(KeyCode.L))
    //        LoadData();
    //}
}

[Serializable]
class Test
{
    public int num;
}

