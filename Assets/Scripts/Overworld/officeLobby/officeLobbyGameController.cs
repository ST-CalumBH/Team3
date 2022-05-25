using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class officeLobbyGameController : MonoBehaviour
{
    public playerController Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.moveSpeed = 0f;
            Player.animator.SetFloat("Horizontal", 0);
            Player.animator.SetFloat("Vertical", 0);
            Player.animator.SetFloat("Speed", 0);
            Player.freezePlayer();

        }
    }

}
