using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerManager : MonoBehaviour
{
    public GameObject item;
    public bool gameActive = true;

    // might change data type from 'gameObject' to a 'list' of possible items to spawn, meteors with viruses riding on top

    public float spawnDelay = 6f;                                       // initial spawn delay
    private float spawnElaspedTime = 0f;

    [SerializeField] private float btmSpawnDelay;
    [SerializeField] private float topSpawnDelay;

    [Header("Spawn Options")]
    public bool limitedSpawn = false;
    [SerializeField] private int amount2Spawn;
    private int spawnCounter = 0;

    [Header("Spawn Area")]
    public float TopX;
    public float BottomX;
    public float TopY;
    public float BottomY;

    void Update()
    {
        spawnElaspedTime += Time.deltaTime;

        if (spawnElaspedTime >= spawnDelay && gameActive == true)
        {
            if (spawnCounter < amount2Spawn && limitedSpawn == true)
            {
                spawnCounter++;
                spawnElaspedTime = 0f;
                spawnDelay = Random.Range(btmSpawnDelay, topSpawnDelay);
                randomMove();
                spawnItem();
            }
            else if (limitedSpawn == false)
            {
                spawnElaspedTime = 0f;
                spawnDelay = Random.Range(btmSpawnDelay, topSpawnDelay);
                randomMove();
                spawnItem();
            }
        }
    }

    public void spawnItem()
    {
        GameObject temp = Instantiate(item, transform.position, transform.rotation);
        temp.transform.SetParent(transform.parent.transform, true);
    }

    public void randomMove()
    {
        transform.position = new Vector2(Random.Range(TopX, BottomX), Random.Range(TopY, BottomY));
    }

    public void changeState()
    {
        gameActive = !gameActive;
    }

    public bool checkState()
    {
        return gameActive;
    }
}
