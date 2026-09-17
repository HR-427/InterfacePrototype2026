using UnityEngine;

public class CameraStabilizer : MonoBehaviour
{
    private Quaternion startLocalRotation;

    void Start()
    {
        startLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        transform.localRotation = startLocalRotation;
    }
}