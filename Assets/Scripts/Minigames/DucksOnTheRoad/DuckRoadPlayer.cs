using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DucksOnTheRoad {
    public class DuckRoadPlayer : MonoBehaviour
    {
        public float moveSpeed = 15f;
        public Rigidbody2D rb;
        [SerializeField] private Vector2 movement;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            movement.y = Input.GetAxisRaw("Vertical");            
        }

        void FixedUpdate() //movement, physics
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("DUCK DOWN");
        }
    }
}