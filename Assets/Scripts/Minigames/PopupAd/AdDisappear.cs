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
            AudioSource.PlayClipAtPoint(mSFX.audioClips[0], Vector3.zero, 1f);
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