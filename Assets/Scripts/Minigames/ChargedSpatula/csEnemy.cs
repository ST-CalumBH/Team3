using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargedSpatula {
    public class csEnemy : MonoBehaviour
    {
        private bool defeated = false;
        private Vector3 direction;

        void Start()
        {
            direction = new Vector3(Random.Range(0.06f, 0.1f), Random.Range(0.06f, 0.1f), 0);
        }

        void FixedUpdate()
        {
            if (defeated == true)
            {
                Spin();
            }
        }

        public void Spin()
        {
            transform.position += direction;
            transform.Rotate(Vector3.back * 1000f * Time.deltaTime);
        }

        public void defeatedStateChange()
        {
            defeated = true;
        }
    }
}
