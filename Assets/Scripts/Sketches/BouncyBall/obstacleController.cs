using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sketches {
    public class obstacleController : MonoBehaviour
    {

        public Rigidbody2D rb;
        public float xforce;
        public float yforce;
        // Start is called before the first frame update
        void Start()
        {
            rb.AddForce(new Vector2(xforce, yforce));
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}