using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/GameState")]
public class GameStateSO : ScriptableObject{
    public SceneProgressSO sceneProgress;

    // Takes all saveable objects (that use ISave) and saves/loads them
    public void SaveAll(){
        ISave[] saveables = { sceneProgress };
        foreach (var s in saveables){
            s.Save();
        }
    }

    public void LoadAll(){
        ISave[] saveables = { sceneProgress };
        foreach (var s in saveables){
            s.Load();
        }
    }
}