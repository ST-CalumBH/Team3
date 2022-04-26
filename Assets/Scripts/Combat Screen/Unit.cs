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

	public bool PlayMinigame(int index)
    {
		GameObject minigame = Instantiate(minigames[index], new Vector3(cam.transform.position.x, cam.transform.position.y, 0), Quaternion.identity);
		//minigame.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		return true;
	}

}
