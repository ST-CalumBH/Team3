using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirusAttack {
    public class BasicMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public Rigidbody2D rb;
        private Vector2 movement;



        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
