using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fences : MonoBehaviour
{

    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

    }
 
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        if (pos.x <= -18)
            pos.x = 49;

        transform.position = pos;

    }
}
