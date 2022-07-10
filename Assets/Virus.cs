using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float moveSpeed = 4f;

    [SerializeField] private minigameManager manager;

    void Start()
    {
        manager = GameObject.Find("spawnerManager").GetComponent<minigameManager>();
    }

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            manager.loseLife();
            Destroy(gameObject);
        }

        if (collision.tag == "Bullet")
        {
            manager.gainPoint();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        manager.loseLife();
        Destroy(gameObject);
    }
}
