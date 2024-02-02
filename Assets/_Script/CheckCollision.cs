using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public bool IsValidLocation { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        IsValidLocation = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsValidLocation = false;
    }
}