using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public bool IsValidLocation { get; set; }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            IsValidLocation = true;
        }
        else
        {
            IsValidLocation = false;
        }
    }
}