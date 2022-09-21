using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopupAd {
    public class DelaySpawn : MonoBehaviour
    {
        public float delaySeconds = 5f;
        public GameObject spawnObject;
        public bool parented = true;

        void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(delaySeconds);

            

            if (parented == true) { GameObject temp = Instantiate(spawnObject); temp.transform.SetParent(transform, true); }
            else { GameObject temp = Instantiate(spawnObject, new Vector3(0.1f,-17.1f,0), Quaternion.identity); }
            
        }

        IEnumerator SpawnOutside()
        {
            yield return new WaitForSeconds(delaySeconds);

            GameObject temp = Instantiate(spawnObject);
        }
    }
}
