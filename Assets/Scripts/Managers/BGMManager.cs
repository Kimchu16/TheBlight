using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;
public class BGMManager : MonoBehaviour
{
    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Im on scene:"+sceneIndex);

        switch (sceneIndex)
        {
            case 3:
                AudioManager.Instance.PlayBGM(BGMType.TutBGM);
                break;
            case 4: //lvl 1
                AudioManager.Instance.PlayBGM(BGMType.Level1);
                break;
            case 5: //lvl2
                AudioManager.Instance.PlayBGM(BGMType.Level2);
                break;
            case 6: //lvl3
                AudioManager.Instance.PlayBGM(BGMType.Level3);
                break;
            default:
                AudioManager.Instance.PlayBGM(BGMType.MainBGM);
                break;
        }
    }

}
