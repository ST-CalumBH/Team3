using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordDodgeMinigame : Minigame
{
    public GameObject animatorGO;
    Animator animator;

    public Warning lWarning;
    public Warning mWarning;
    public Warning rWarning;

    public List<Warning> warnings;

    public Text text;

    SpriteRenderer lWarningGO;
    SpriteRenderer mWarningGO;
    SpriteRenderer rWarningGO;

    int laneNum;
    public int dodgeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = animatorGO.GetComponent<Animator>();
        animator.StopPlayback();
        warnings.Add(lWarning);
        warnings.Add(mWarning);
        warnings.Add(rWarning);
        lWarningGO = lWarning.GetComponent<SpriteRenderer>();
        mWarningGO = mWarning.GetComponent<SpriteRenderer>();
        rWarningGO = rWarning.GetComponent<SpriteRenderer>();
        lWarningGO.enabled = false;
        mWarningGO.enabled = false;
        rWarningGO.enabled = false;
        laneNum = 0;//Random.Range(0, 3);
        StartCoroutine(ShowWarning());
    }

    // Update is called once per frame
    void Update()
    {
        text.text = dodgeCount.ToString();
    }

    IEnumerator ShowWarning()
    {
        yield return new WaitForSeconds(1f);
        warnings[laneNum].StartFlash();
        yield return new WaitForSeconds(warnings[laneNum].GetTotalFlashTime() + 1f);
        Slash();
    }

    IEnumerator LeftAttack()
    {
        Debug.Log("Start Left");
        animator.Play("Base Layer.Left Lane");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0));
        Debug.Log("Finished Left");
    }
    IEnumerator MiddleAttack()
    {
        animator.Play("Base Layer.Mid Lane");
        yield return new WaitForSeconds(2f);
    }
    IEnumerator RightAttack()
    {
        animator.Play("Base Layer.Right Lane");
        yield return new WaitForSeconds(2f);
    }

    private void Slash()
    {
        switch (laneNum)
        {
            case 0:
                StartCoroutine(LeftAttack());
            break;
            case 1:
                StartCoroutine(MiddleAttack());
            break;
            case 2:
                StartCoroutine(RightAttack());
            break;
            default:
                StartCoroutine(MiddleAttack());
            break;
        };
        laneNum = Random.Range(0, 3);
    }

    public void CallShowWarning()
    {
        StartCoroutine(ShowWarning());
    }

    public void CallEndMinigame()
    {
        StartCoroutine(EndMinigame());
    }

}
