using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirusAttack {
    public class BasicMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public Rigidbody2D rb;
        private Vector2 movement;
        public bool paused;


        void Update()
        {
            if (!paused)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }
        }

        private void FixedUpdate()
        {
            if (!paused)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
