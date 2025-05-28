using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/SceneProgress")]
public class SceneProgressSO : ScriptableObject, ISave{
    public int lastScene = 1;
    public bool hasSeenIntroCutscene = false;
    public bool settingsWasOpen = false;

    [System.Serializable]
    private struct SaveData{
        public int lastScene;
        public bool hasSeenIntroCutscene;
        public bool settingsWasOpen;
    }

    // Saves data locally to a json file
    public void Save(){
        SaveData data = new SaveData{
            lastScene = this.lastScene,
            hasSeenIntroCutscene = this.hasSeenIntroCutscene,
            settingsWasOpen = this.settingsWasOpen
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
            this.settingsWasOpen = data.settingsWasOpen;
        }else{ // Reset in-memory values if no file found
            this.lastScene = 0;
            this.hasSeenIntroCutscene = false;
            this.settingsWasOpen = false;
            }
    }

    private string GetPath() => Application.persistentDataPath + "/scene_progress.json";
}