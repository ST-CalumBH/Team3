using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopupAd {
    public class AdDisappear : MonoBehaviour
    {
        [SerializeField] private popupadManager manager;

        public Sprite[] advertismentSprites;

        private int randomiser;

        void Awake()
        {
            manager = GameObject.Find("gameManager").GetComponent<popupadManager>();
            manager.AdAdded();

            SpriteChange();
        }

        public void SpriteChange()
        {
            randomiser = Random.Range(0, advertismentSprites.Length);
            Debug.Log("Sprite: " + randomiser + "/" + advertismentSprites.Length);
            GetComponent<SpriteRenderer>().sprite = advertismentSprites[randomiser];
        }

        public void DestroySelf()
        {
            manager.AdDeleted();
            Destroy(gameObject);
        }
    }
}