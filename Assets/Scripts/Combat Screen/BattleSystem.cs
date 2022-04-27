﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	Camera cam;
    private bool gameEnd = false;

	// Start is called before the first frame update
	void Start()
    {
		cam = Camera.main;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		playerUnit.PlayMinigame(playerUnit.minigameCount);
		yield return new WaitUntil(() => !(playerUnit.playedMinigame.isInProgress));
		if (playerUnit.minigameCount < playerUnit.minigames.Length - 1)
		{ playerUnit.minigameCount++; }

		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		Debug.Log("Enemy Turn");
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		enemyUnit.PlayMinigame(enemyUnit.minigameCount);
		yield return new WaitUntil(() => !(enemyUnit.playedMinigame.isInProgress));
		if (enemyUnit.minigameCount < enemyUnit.minigames.Length - 1)
		{ enemyUnit.minigameCount++; }

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

	//IEnumerator PlayerHeal()
	//{
	//	playerUnit.Heal(5);

	//	playerHUD.SetHP(playerUnit.currentHP);
	//	dialogueText.text = "You feel renewed strength!";

	//	yield return new WaitForSeconds(2f);

	//	state = BattleState.ENEMYTURN;
	//	StartCoroutine(EnemyTurn());
	//}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	//public void OnHealButton()
	//{
	//	if (state != BattleState.PLAYERTURN)
	//		return;

	//	StartCoroutine(PlayerHeal());
	//}

}