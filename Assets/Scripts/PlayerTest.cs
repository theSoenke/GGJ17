using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public LevelGenerator levelGenerator;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            levelGenerator.UpdatePlayerPosition(transform.position.x);
        }
    }
}
