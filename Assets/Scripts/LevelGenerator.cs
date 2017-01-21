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
        LevelPart newLevelPart = GetNewLevelPart();
        lastSpawnPos = xPos + newLevelPart.width;
        var partPos = new Vector3(xPos, 0, 0);
        GameObject spawnedPart = Instantiate(newLevelPart.gameObject, partPos, newLevelPart.transform.rotation);
        spawnedPart.transform.SetParent(transform);
        spawnedParts.Add(spawnedPart);
    }

    private LevelPart GetNewLevelPart()
    {
        int levelPartNum = Random.Range(0, levelPrefabs.Count);
        return levelPrefabs[levelPartNum];
    }
}
