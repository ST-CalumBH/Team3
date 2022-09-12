using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DucksOnTheRoad {
    public class RoadScroll : MonoBehaviour
    {
        public float ScrollSpeed = 0.5f;
        
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            Vector2 pos = transform.position;

            var posDelta = -ScrollSpeed * Time.fixedDeltaTime;

            var threshold = -20.19f;

            if (pos.x + posDelta <= threshold)
                pos.x = -1f - posDelta;
            else 
                pos.x += posDelta;

            transform.position = pos;

        }

    }
}