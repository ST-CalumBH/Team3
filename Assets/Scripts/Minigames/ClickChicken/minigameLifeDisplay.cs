using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClickChicken
{
    public class minigameLifeDisplay : MonoBehaviour
    {
        public int lives;
        [SerializeField] private GameObject heart;
        [SerializeField] private float rowSpacing = 1f;

        private GameObject[] lifeList;

        private void Start()
        {
            int lifeCount = lives;
            lifeList = new GameObject[lives];
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
            lives--;
            lifeList[lives].GetComponent<lifeHeart>().Disappear();
        }

        public int GetLives()
        {
            return lives;
        }
    }
}
