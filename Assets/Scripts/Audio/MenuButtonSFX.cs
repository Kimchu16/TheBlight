using UnityEngine;
using Audio; 

public class MenuButtonSFX : MonoBehaviour
{
    [SerializeField] private SFXType sfxType;

    public void PlaySound()
    {
        AudioManager.Instance.PlaySFX(sfxType);
    }
}
