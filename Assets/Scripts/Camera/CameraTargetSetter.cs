using UnityEngine;
using Unity.Cinemachine;

public class CameraTargetSetter : MonoBehaviour
{
    private CinemachineCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();

        Invoke(nameof(AssignPlayer), 0.1f);
    }

    void AssignPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            vcam.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("CameraTargetSetter: Could not find player to follow.");
        }
    }
}
