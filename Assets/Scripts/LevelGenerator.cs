using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> levelPrefabs;
    public float minPlayerSpawnDistance = 1;
    public float prefabWidth = 1;
    public float prefabHeight = 1;
    public float cullingDistance = 1;

    private float lastSpawnedPos;
    private readonly List<GameObject> spawnedParts = new List<GameObject>();
    private Transform player;


    private void Start()
    {
        player = GameController.Instance.Player;
    }

    private void Update()
    {
        float playerPos = player.position.x;
        LoadNewLevelParts(playerPos);
        LevelCulling(playerPos);
    }
    private void LoadNewLevelParts(float xPlayerPos)
    {
        if (lastSpawnedPos - xPlayerPos - minPlayerSpawnDistance < 0)
        {
            float nextSpawnPos = lastSpawnedPos + prefabWidth;
            SpawnNewLevelPart(nextSpawnPos);
        }
    }

    private void LevelCulling(float xPlayerPos)
    {
        GameObject levelPartToRemove = null;

        foreach (var levelPrefab in spawnedParts)
        {
            if (levelPrefab.transform.position.x < xPlayerPos - cullingDistance)
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
        float height = -(prefabHeight / 2);
        var partPos = new Vector3(xPos, height, 0);
        GameObject spawnedPart = Instantiate(newLevelPart, partPos, newLevelPart.transform.rotation);
        spawnedPart.transform.SetParent(transform);
        spawnedParts.Add(spawnedPart);
    }

    private GameObject GetNewLevelPart()
    {
        int levelPartNum = Random.Range(0, levelPrefabs.Count);
        return levelPrefabs[levelPartNum];
    }
}
