using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PopupAd {
    public class ClickDetect : MonoBehaviour
    {
        [SerializeField] private bool isDistracted;

        private void Start()
        {
            isDistracted = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && (isDistracted == false))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.SendMessage("DestroySelf");
                }
            }
        }

        public void distractionChange(bool state)
        {
            isDistracted = state;
        }
    }
}