using UnityEngine;

public class MoveableEntity : MonoBehaviour
{
    protected bool IsMoveable { get; set; }

    protected virtual void Start()
    {
        GameManager.OnLoadingEvent += GameManager_OnLoadingStartEvent;
        GameManager.OnPlayEventStart += GameManager_OnPlayEventStart;
        GameManager.OnWaveTransitionEvent += GameManager_OnWaveTransitionEvent;
    }

    protected void GameManager_OnWaveTransitionEvent()
    {
        IsMoveable = false;
    }

    protected void GameManager_OnLoadingStartEvent()
    {
        IsMoveable = false;
    }

    protected void GameManager_OnPlayEventStart()
    {
        IsMoveable = true;
    }
}