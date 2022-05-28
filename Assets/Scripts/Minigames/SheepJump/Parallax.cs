using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1;
    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realVelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= -8)
            pos.x = 20;

            transform.position = pos;

    }
}
