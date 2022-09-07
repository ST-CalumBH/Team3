using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DucksOnTheRoad {
    public class DuckRoadDuck : MonoBehaviour
    {
        public Animator anim;

        public float ScrollSpeed = 4f;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            GameObject player = GameObject.Find("Player");
            Bounds playerBounds = player.GetComponent<BoxCollider2D>().bounds;
            Bounds colliderBounds = GetComponent<BoxCollider2D>().bounds;

            string chosenLayer = playerBounds.min.y > colliderBounds.max.y ? "Overlay" : "Default";
            renderer.sortingLayerID = SortingLayer.NameToID(chosenLayer);
            
        }

        private void FixedUpdate() 
        {            
            Vector2 pos = transform.position;
            var posDelta = -ScrollSpeed * Time.fixedDeltaTime;
            pos.x += posDelta;

            if (pos.x <= -12) 
                Destroy(gameObject);
                
            transform.position = pos;
        }
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Player")
            {
                Debug.Log("Duck dies");
                anim.Play("Base Layer.DuckCollision");

            }
        }
    }
}