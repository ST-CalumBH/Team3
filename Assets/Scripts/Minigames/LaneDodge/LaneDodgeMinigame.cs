using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneDodgeMinigame : Minigame
{
    public Warning lWarning;
    public Warning mWarning;
    public Warning rWarning;

    public Transform lEggSpawn;
    public Transform mEggSpawn;
    public Transform rEggSpawn;

    public List<Warning> warnings;

    public GameObject projectile;

    public Text text;

    SpriteRenderer lWarningGO;
    SpriteRenderer mWarningGO;
    SpriteRenderer rWarningGO;

    int laneNum;
    public int dodgeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        warnings.Add(lWarning);
        warnings.Add(mWarning);
        warnings.Add(rWarning);
        lWarningGO = lWarning.GetComponent<SpriteRenderer>();
        mWarningGO = mWarning.GetComponent<SpriteRenderer>();
        rWarningGO = rWarning.GetComponent<SpriteRenderer>();
        lWarningGO.enabled = false;
        mWarningGO.enabled = false;
        rWarningGO.enabled = false;
        laneNum = Random.Range(0,3);
        StartCoroutine(ShowWarning());
    }

    //Update is called once per frame
    void Update()
    {
        text.text = dodgeCount.ToString();
    }

    IEnumerator ShowWarning()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Flash Start");
        warnings[laneNum].StartFlash();
        yield return new WaitForSeconds(warnings[laneNum].GetTotalFlashTime()+1f);
        //Debug.Log("Shoot CoRoutine");
        ShootProjectile();
    }

    private void ShootProjectile()
    {
        if (dodgeCount < 3)
        {
            Transform eggSpawn = laneNum switch
            {
                0 => lEggSpawn,
                1 => mEggSpawn,
                2 => rEggSpawn,
                _ => mEggSpawn,
            };
            Instantiate(projectile, eggSpawn);
            laneNum = Random.Range(0, 3);
            StartCoroutine(ShowWarning());
        }
        else
        {
            StartCoroutine(EndMinigame());
        }
    }
}
