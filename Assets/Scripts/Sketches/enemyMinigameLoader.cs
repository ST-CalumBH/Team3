using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sketches {
    public class enemyMinigameLoader : MonoBehaviour
    {
        [SerializeField] private GameObject minigameName;

        Camera cam;

        public void Start()
        {
            cam = Camera.main;
        }

        public void InstantiateMinigame()
        {
            Instantiate(minigameName, new Vector3(cam.transform.position.x, cam.transform.position.y, 0), Quaternion.identity);
        }
    }
}