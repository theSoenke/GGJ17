using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<LevelPart> levelPrefabs;
    public float minPlayerSpawnDistance = 1;
    public float cullingDistance = 1;

    private float lastSpawnPos;
    private readonly List<GameObject> spawnedParts = new List<GameObject>();
    private Transform player;
    private LevelPart currentLevelPart;


    private void Start()
    {
        player = GameController.Instance.Player.transform;
    }

    private void Update()
    {
        float playerPos = player.position.x;
        LoadNewLevelParts(playerPos);
        LevelCulling(playerPos);
    }
    private void LoadNewLevelParts(float xPlayerPos)
    {
        if (lastSpawnPos - xPlayerPos - minPlayerSpawnDistance < 0)
        {
            SpawnNewLevelPart(lastSpawnPos);
        }
    }

    private void LevelCulling(float xPlayerPos)
    {
        GameObject levelPartToRemove = null;

        foreach (GameObject levelPrefab in spawnedParts)
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
        float previousWidthHalf = currentLevelPart != null ? currentLevelPart.width / 2 : 0;
        currentLevelPart = GetNewLevelPart();
        float currentWidthHalf = currentLevelPart.width / 2;
        lastSpawnPos = xPos + previousWidthHalf + currentWidthHalf;
        var partPos = new Vector3(lastSpawnPos, 0, 0);
        GameObject spawnedPart = Instantiate(currentLevelPart.gameObject, partPos, currentLevelPart.transform.rotation);
        spawnedPart.transform.SetParent(transform);
        spawnedParts.Add(spawnedPart);
    }

    private LevelPart GetNewLevelPart()
    {
        int levelPartNum = Random.Range(0, levelPrefabs.Count);
        return levelPrefabs[levelPartNum];
    }
}
