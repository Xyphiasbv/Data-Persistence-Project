using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public InputField Name;
    public string PlayerName;

    private void Awake() {
        
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);        
        LoadName();  
    }

    [System.Serializable]
    class SaveData
    {
        //public InputField Name;
        public string PlayerName;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        if(Name != null)
        {
        data.PlayerName = Name.text;
        PlayerName = Name.text;
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if(Name != null)
            {
            Name.text = data.PlayerName;
            }
            PlayerName = data.PlayerName;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
