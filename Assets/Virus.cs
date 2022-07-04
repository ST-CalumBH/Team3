using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float moveSpeed = 4f;
    public GameObject gameManager;

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
    }

    private void OnBecameInvisible()
    {
        // add lives lost to lose condition
        Destroy(gameObject);
    }
}
