using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld {
    public class CameraFollowYAxis : MonoBehaviour
    {
        public Transform target;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 position = transform.position;
            position.y = target.position.y;
            transform.position = position;
        }
    }
}