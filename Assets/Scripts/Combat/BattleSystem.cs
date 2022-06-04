using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	[SerializeField] private string nextScene;

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

	// Start is called before the first frame update
	void Start()
    {
		cam = Camera.main;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene("homeBedroomScene");
		}
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
		Debug.Log("Player Turn");
		StartCoroutine(PlayerAttack());
	}

	IEnumerator PlayerAttack()
	{
		dialogueText.text = "Keith attacks!";

		yield return new WaitForSeconds(3f);

		enemyUnit.PlayMinigame(enemyUnit.minigameCount);
		yield return new WaitUntil(() => !(enemyUnit.playedMinigame.isInProgress));
		if (enemyUnit.minigameCount < enemyUnit.minigames.Length - 1)
		{ enemyUnit.minigameCount++; }

		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "Take that good sir";

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
		dialogueText.text = enemyUnit.unitName + " attacks! Uh Oh";

		yield return new WaitForSeconds(1f);

		enemyUnit.PlayMinigame(enemyUnit.minigameCount);
		yield return new WaitUntil(() => !(enemyUnit.playedMinigame.isInProgress));
		if (enemyUnit.minigameCount < enemyUnit.minigames.Length - 1)
		{ enemyUnit.minigameCount++; }

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(0f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			StartCoroutine(PlayerAttack());
		}

	}

	IEnumerator EndCombat()
    {
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(nextScene);
	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "AHA, breakfast served sunny side up ;)";
			StartCoroutine(EndCombat());
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
			StartCoroutine(EndCombat());
		}
	}

}
