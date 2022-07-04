using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float moveSpeed = 4f;
    public minigameManager manager;

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "player")
        {
            manager.LoseLife();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        manager.LoseLife();
        Destroy(gameObject);
    }
}
