using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerManager : MonoBehaviour
{
    public float amountToSpawn;
    private int spawnCounter = 0;
    public GameObject item;

    // might make into a list of possible items to spawn, meteors with viruses riding on top

    public float spawnDelay = 6f;                   // randomise this
    private float spawnElaspedTime = 0f;

    [Header("Spawn Range")]
    public float TopX;
    public float BottomX;
    public float TopY;
    public float BottomY;

    void Update()
    {
        spawnElaspedTime += Time.deltaTime;

        if (spawnCounter < amountToSpawn && spawnElaspedTime >= spawnDelay)
        {
            spawnCounter++;
            spawnElaspedTime = 0f;
            RandomMove();
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        Instantiate(item, transform.position, transform.rotation);
    }

    void RandomMove()
    {
        transform.position = new Vector2(Random.Range(TopX, BottomX), Random.Range(TopY, BottomY));
    }
}
