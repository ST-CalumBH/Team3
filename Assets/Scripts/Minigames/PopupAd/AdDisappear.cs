using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PopupAd {
    public class AdDisappear : MonoBehaviour
    {
        [SerializeField] private popupadManager manager;

        public Sprite[] advertismentSprites;

        private int randomiser;

        public MinigameSFX mSFX;

        void Awake()
        {
            manager = GameObject.Find("gameManager").GetComponent<popupadManager>();
            mSFX.PlaySound(0, 0.5f);
            SpriteChange();
        }

        public void SpriteChange()
        {
            randomiser = Random.Range(0, advertismentSprites.Length);
            GetComponent<SpriteRenderer>().sprite = advertismentSprites[randomiser];
        }

        public void DestroySelf()
        {
            manager.AdClosed();
            Destroy(gameObject);
        }
    }
}