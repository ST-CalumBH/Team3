using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMinigameLoader : MonoBehaviour
{
    [SerializeField] private GameObject minigameName;

    public void InstantiateMinigame()
    {
        Camera cam = Camera.main;
        Instantiate(minigameName, new Vector3(cam.transform.position.x, cam.transform.position.y, 0), Quaternion.identity);
    }
}
