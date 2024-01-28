using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    float defaultTimeScale;
    private void Start()
    {
        defaultTimeScale = Time.timeScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == defaultTimeScale)
        {
            Time.timeScale = 0;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != defaultTimeScale))
        {
            Time.timeScale = defaultTimeScale;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerData.Instance.GetDamage(10);
        }
    }
}
