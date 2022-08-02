using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// Local file save for Spaces class
public class SaveData : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static SaveData _instance;
    public static SaveData Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "saveData";
                _instance = _container.AddComponent(typeof(SaveData)) as SaveData;
            }
            return _instance;
        }
    }

    public string GameDataFileName = "GameData.json";

    public Spaces _gameData;
    public Spaces gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("불러오기");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<Spaces>(FromJsonData);
        }
        else
        {
            Debug.Log("새파일");
            _gameData = new Spaces();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("데이터 저장");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
