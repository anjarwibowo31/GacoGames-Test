using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        Vector3 lookAtPos = new Vector3(transform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z);
        transform.LookAt(lookAtPos);
    }
}
