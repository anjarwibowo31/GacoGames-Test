using UnityEngine;

public class MoveableEntity : MonoBehaviour
{
    protected bool IsMoveable { get; set; }

    protected virtual void Start()
    {
        GameManager.OnLoadingStartEvent += GameManager_OnLoadingStartEvent;
        GameManager.OnLoadingEndEvent += GameManager_OnLoadingEndEvent;
    }

    protected void GameManager_OnLoadingStartEvent()
    {
        IsMoveable = false;
    }

    protected void GameManager_OnLoadingEndEvent()
    {
        IsMoveable = true;
    }
}