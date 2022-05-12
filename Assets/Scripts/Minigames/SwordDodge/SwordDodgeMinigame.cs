using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordDodgeMinigame : Minigame
{
    public GameObject animatorGO;
    Animator animator;

    public Warning lWarningGO;
    public Warning mWarningGO;
    public Warning rWarningGO;

    public SwordDodgePlayer Player;

    public List<Warning> warnings;

    public Text text;

    SpriteRenderer lWarning;
    SpriteRenderer mWarning;
    SpriteRenderer rWarning;



    int laneNum;
    public int dodgeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = animatorGO.GetComponent<Animator>();
        animator.StopPlayback();
        warnings.Add(lWarningGO);
        warnings.Add(mWarningGO);
        warnings.Add(rWarningGO);
        lWarning = lWarningGO.GetComponent<SpriteRenderer>();
        mWarning = mWarningGO.GetComponent<SpriteRenderer>();
        rWarning = rWarningGO.GetComponent<SpriteRenderer>();
        lWarning.enabled = false;
        mWarning.enabled = false;
        rWarning.enabled = false;
        laneNum = Random.Range(0, 3);
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
        animator.Play("Base Layer.Left Lane");
        yield return new WaitForSeconds(1.07f); //Currently just manually inputting the animation length, but should make a dynamic system.
        animator.Play("Base Layer.Neutral");
        StartCoroutine(ShowWarning());
    }
    IEnumerator MiddleAttack()
    {
        animator.Play("Base Layer.Mid Lane");
        yield return new WaitForSeconds(1.07f);
        animator.Play("Base Layer.Neutral");
        StartCoroutine(ShowWarning());
    }
    IEnumerator RightAttack()
    {
        animator.Play("Base Layer.Right Lane");
        yield return new WaitForSeconds(1.07f);
        animator.Play("Base Layer.Neutral");
        StartCoroutine(ShowWarning());
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
