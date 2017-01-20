using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levelParts;
    public float partWidth = 1;

    private float lastSpawnedPos;
    private int partsSpawned;


    public void SetPlayerPosition(float xPos)
    {
        if (lastSpawnedPos < xPos / partWidth)
        {
            float nextSpawnPos = lastSpawnedPos + (partsSpawned + partWidth);
            SpawnNewLevelPart(nextSpawnPos);
        }
    }

    private void SpawnNewLevelPart(float xPos)
    {
        lastSpawnedPos = xPos;
        GameObject newLevelPart = GetNewLevelPart();
        var partPos = new Vector3(lastSpawnedPos, 0, 0);
        var spawnedPart = Instantiate(newLevelPart, partPos, newLevelPart.transform.rotation);
        spawnedPart.transform.SetParent(transform);
        partsSpawned++;
    }

    private GameObject GetNewLevelPart()
    {
        int levelPartNum = Random.Range(0, levelParts.Length);
        return levelParts[levelPartNum];
    }
}
