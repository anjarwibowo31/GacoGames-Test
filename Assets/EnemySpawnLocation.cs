using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLocation : MonoBehaviour
{
    [SerializeField] private float gizmoRadius = .3f;
    [SerializeField] private Color gizmoColor = Color.white;

    private List<Transform> spawnLocationList = new();

    private void Start()
    {
        foreach (Transform t in transform)
        {
            spawnLocationList.Add(t);
        }

        GameplaySystem.Instance.GetSpawnLocation += GameplaySystem_GetSpawnLocation;
    }

    private void GameplaySystem_GetSpawnLocation(int enemyCount)
    {
        GameplaySystem.SpawnLocationList.Clear();

        List<Transform> tempValidList = new();

        foreach (Transform t in spawnLocationList)
        {
            bool validLocation;
            validLocation = t.GetComponent<CheckCollision>().IsValidLocation;

            if (validLocation)
            {
                tempValidList.Add(t);
            }
        }

        GameplaySystem.SpawnLocationList = GetRandomObjects(tempValidList, enemyCount);
    }

    private List<Transform> GetRandomObjects(List<Transform> originalList, int enemyCount)
    {
        List<Transform> resultList = new();
        List<Transform> tempList = new(originalList);

        for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            resultList.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }

        return resultList;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(t.position, gizmoRadius);
        }
    }
}
