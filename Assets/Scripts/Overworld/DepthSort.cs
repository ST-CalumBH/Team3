using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld {
    public class DepthSort : MonoBehaviour
    {
        public GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            Bounds playerBounds = player.GetComponent<BoxCollider2D>().bounds;
            Bounds colliderBounds = GetComponent<BoxCollider2D>().bounds;

            string chosenLayer = playerBounds.min.y > colliderBounds.max.y ? "Overlay" : "Default";
            renderer.sortingLayerID = SortingLayer.NameToID(chosenLayer);
            
            // Draw debug line between the two edges we're comparing
            // Debug.DrawLine (new Vector3(colliderBounds.center.x, colliderBounds.max.y, 0), new Vector3 (playerBounds.center.x, playerBounds.min.y, 0), Color.red);
        }
    }
}