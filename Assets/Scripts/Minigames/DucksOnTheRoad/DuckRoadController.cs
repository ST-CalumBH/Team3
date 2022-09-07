using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Combat;
using Overworld;

namespace DucksOnTheRoad {
    public class DuckRoadController : Minigame
    {
        private float[] yPositions = {-1f, -2.5f, -3.4f};
        private SceneTransitionScript SceneTransition; 
        public GameObject duckPrefab;
        public float minigameDuration;
        
        // Start is called before the first frame update
        void Start()
        {   
            SceneTransition = gameObject.GetComponent<SceneTransitionScript>();
            spawnDuck();
            StartCoroutine(ExecuteAfterTime(minigameDuration));
        }

        // Update is called once per frame
        void Update()
        {
            
            var lane = Random.Range(0, 10000);
            if (lane < 10) spawnDuck();
        }

        void spawnDuck() {
            var lane = Random.Range(0, 3);

            var duck = Instantiate(duckPrefab, new Vector3(11.5f, yPositions[lane], 0), Quaternion.identity);
            var duckRenderer = duck.GetComponent<SpriteRenderer>();
            duckRenderer.sortingLayerID = SortingLayer.NameToID("Player");
            duckRenderer.sortingOrder = lane;
        }

         IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            SceneTransition.changeScene();
        }
    }
}