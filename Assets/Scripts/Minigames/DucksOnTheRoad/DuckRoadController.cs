using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace DucksOnTheRoad {
    public class DuckRoadController : Minigame
    {
        private float[] yPositions = {-1.09f, -2.7f, -3.8f};
        public GameObject duckPrefab;
        // Start is called before the first frame update
        void Start()
        {
            spawnDuck();
        }

        // Update is called once per frame
        void Update()
        {
            
            var lane = Random.Range(0, 10000);
            if (lane < 5) spawnDuck();
        }

        void spawnDuck() {
            var lane = Random.Range(0, 3);

            var duck = Instantiate(duckPrefab, new Vector3(11.5f, yPositions[lane], 0), Quaternion.identity);
            var duckRenderer = duck.GetComponent<SpriteRenderer>();
            duckRenderer.sortingLayerID = SortingLayer.NameToID("Player");
            duckRenderer.sortingOrder = lane;
        }
    }
}