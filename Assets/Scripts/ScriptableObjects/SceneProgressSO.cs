using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/SceneProgress")]
public class SceneProgressSO : ScriptableObject, ISave{
    public int lastScene = 1;
    public bool hasSeenIntroCutscene = false;

    [System.Serializable]
    private struct SaveData{
        public int lastScene;
        public bool hasSeenIntroCutscene;
    }

    // Saves data locally to a json file
    public void Save(){
        SaveData data = new SaveData{
            lastScene = this.lastScene,
            hasSeenIntroCutscene = this.hasSeenIntroCutscene,
        };
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(GetPath(), json);
    }

    // If exists, it reads a json file to load in saved data
    public void Load(){
        if (System.IO.File.Exists(GetPath())){
            string json = System.IO.File.ReadAllText(GetPath());
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            this.lastScene = data.lastScene;
            this.hasSeenIntroCutscene = data.hasSeenIntroCutscene;
        }else{ // Reset in-memory values if no file found
            this.lastScene = 0;
            this.hasSeenIntroCutscene = false;
            }
    }

    private string GetPath() => Application.persistentDataPath + "/scene_progress.json";
}