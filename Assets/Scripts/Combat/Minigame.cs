using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Minigame : MonoBehaviour
{

    public bool result = false;
    public bool isInProgress = true;

    public IEnumerator EndMinigame(bool res)
    {
        if (res)
        {
            Analytics.CustomEvent("MinigameWin");
        }
        else
        {
            Analytics.CustomEvent("MinigameLoss");
        }
        yield return new WaitForSeconds(1f);
        isInProgress = false;
        result = res;
        Debug.Log(res.ToString());
        CheckResult(res);
        Destroy(transform.parent.gameObject);
    }

    public IEnumerator EndMinigame(float time, bool res)
    {
        if (res)
        {
            Analytics.CustomEvent("MinigameWin");
        }
        else
        {
            Analytics.CustomEvent("MinigameLoss");
        }
        yield return new WaitForSeconds(time);
        isInProgress = false;
        result = res;
        Debug.Log(res.ToString());
        CheckResult(res);
        Destroy(transform.parent.gameObject);
    }

    private void CheckResult(bool result)
    {
        if (result == false)
        {
            LifeSystem.Instance.playerLives--;
        }
    }
}