using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClickChicken
{
    public class chickenLifeDisplay : MonoBehaviour
    {
        [SerializeField] private chickenMovement controller;
        [SerializeField] private GameObject heart;
        [SerializeField] private float rowSpacing = 1f;

        private GameObject[] lifeList;

        private void Start()
        {
            int lifeCount = controller.GetLives();
            lifeList = new GameObject[controller.GetLives()];
            float x = transform.position.x;

            for (int i = 0; i < lifeCount; i++)
            {
                lifeList[i] = Instantiate(heart, new Vector3(x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                lifeList[i].transform.SetParent(transform.parent.transform, true);
                x += rowSpacing;
            }
        }

        public void LoseLife()
        {
            lifeList[controller.GetLives()].GetComponent<lifeHeart>().Disappear();
        }
    }
}
