using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{

    public bool failed = false;
    public bool isInProgress = true;

    public IEnumerator EndMinigame()
    {
        yield return new WaitForSeconds(1f);
        isInProgress = false;
        Destroy(transform.parent.gameObject);
    }
}