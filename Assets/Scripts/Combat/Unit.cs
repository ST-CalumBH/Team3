using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;

namespace Combat {
	public class Unit : MonoBehaviour
	{

		public string unitName;
		public int unitLevel;

		public int damage;

		public int maxHP;
		public int currentHP;

		public GameObject[] minigames;
		public Minigame playedMinigame;
		public int minigameCount = 0;

		Camera cam;

		[Space(20)]
		public DialogueObject[] dialogueList = new DialogueObject[5];				// 1 = encounter, 2 = attack, 3 = successful attack, 4 = defend, 5 = win quote

		// Start is called before the first frame update
		void Start()
		{
			cam = Camera.main;
		}

		public bool TakeDamage(int dmg)
		{
			currentHP -= dmg;

			if (currentHP <= 0)
				return true;
			else
				return false;
		}

		public void Heal(int amount)
		{
			currentHP += amount;
			if (currentHP > maxHP)
				currentHP = maxHP;
		}

		public void PlayMinigame(int index)
		{
			GameObject playedMinigameGO = Instantiate(minigames[index], new Vector3(cam.transform.position.x, cam.transform.position.y, 0), Quaternion.identity);
			playedMinigame = playedMinigameGO.GetComponentInChildren<Minigame>();
		}
	}
}
