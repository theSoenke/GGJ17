using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> levelPrefabs;
    public float partWidth = 1;

    private float lastSpawnedPos;
    private int partsSpawned;
    private readonly List<GameObject> spawnedParts = new List<GameObject>();


    public void SetPlayerPosition(float xPlayerPos)
    {
        if (lastSpawnedPos < xPlayerPos / partWidth)
        {
            float nextSpawnPos = lastSpawnedPos + (partsSpawned + partWidth);
            SpawnNewLevelPart(nextSpawnPos);
        }
        RemoveLevelsPartsOutOfView(xPlayerPos);
    }

    private void RemoveLevelsPartsOutOfView(float xPlayerPos)
    {
        GameObject levelPartToRemove = null;

        foreach (var levelPrefab in spawnedParts)
        {
            if (levelPrefab.transform.position.x < xPlayerPos - partWidth)
            {
                levelPartToRemove = levelPrefab;
            }
        }

        if (levelPartToRemove != null)
        {
            spawnedParts.Remove(levelPartToRemove);
            Destroy(levelPartToRemove);
        }
    }

    private void SpawnNewLevelPart(float xPos)
    {
        lastSpawnedPos = xPos;
        GameObject newLevelPart = GetNewLevelPart();
        var partPos = new Vector3(lastSpawnedPos, 0, 0);
        GameObject spawnedPart = Instantiate(newLevelPart, partPos, newLevelPart.transform.rotation);
        spawnedPart.transform.SetParent(transform);
        spawnedParts.Add(spawnedPart);
        partsSpawned++;
    }

    private GameObject GetNewLevelPart()
    {
        int levelPartNum = Random.Range(0, levelPrefabs.Count);
        return levelPrefabs[levelPartNum];
    }
}
