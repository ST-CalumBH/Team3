using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirusAttack {
    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed = 15f;

        void Update()
        {
            transform.Translate(bulletSpeed * Time.deltaTime, 0, 0);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}