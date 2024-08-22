using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string namee;
    public int TheBestScore;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    class SaveData
    {
        public string namee;
        public int TheBestScore;
    }

 public    void SaveName()
    {
        SaveData data = new SaveData();
        data.namee = namee;
        data.TheBestScore = TheBestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/Savefile.json", json);
    }

  public  void LoadName()
    {
        string path = Application.persistentDataPath + "/Savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            namee = data.namee;
            TheBestScore = data.TheBestScore;
        }

    }
}
