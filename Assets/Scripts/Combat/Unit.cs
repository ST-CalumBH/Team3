using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


