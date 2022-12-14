using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class JsonManager : MonoSingleTon<JsonManager>
{
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField] private JsonData data = null;
    public JsonData Data { get { return data; } }

    public void Awake()
    {
        Load();
        Save();
    }

    public void OnDestroy()
    {
        Save();
    }

    public void OnApplicationQuit()
    {
        Save();
    }

    [ContextMenu("불러오기")]
    public void Load()
    {
        Init();
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME) == true)
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            data = JsonUtility.FromJson<JsonData>(json);
        }
    }
    [ContextMenu("저장")]
    public void Save()
    {
        Init();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    public void Init()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
    }
}