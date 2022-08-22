using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PopupAd {
    public class DelaySpawn : MonoBehaviour
    {
        public float delaySeconds;
        public GameObject spawnObject;

        void Start()
        {
            StartCoroutine(Spawn());
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(delaySeconds);

            GameObject temp = Instantiate(spawnObject, transform.position, transform.rotation);
            temp.transform.SetParent(transform.parent.transform, true);
        }
    }
}
