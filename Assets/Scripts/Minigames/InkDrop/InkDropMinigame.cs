using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InkDropMinigame : Minigame
{
    public Transform inkSpawn;
    Transform inkSpawnRef;

    public GameObject projectile;

    public TextMeshProUGUI inkCounter;

    float variance;
    public int inkCollected = 0;
    public int scoreToReach = 7;
    public float startGrace = 3f;
    public int projectileCount = 2;
    public float shotDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        variance = Random.Range(-10f, 10f);
        inkCounter.text = "0";
        inkSpawnRef = inkSpawn;
        StartCoroutine(WarmUp());
    }

    //Update is called once per frame
    void Update()
    {
        inkCounter.text = inkCollected.ToString();
        if(inkCollected >= scoreToReach)
        {
            StartCoroutine(EndMinigame(true));
        }
    }

    public IEnumerator WarmUp()
    {
        yield return new WaitForSeconds(startGrace);
        StartCoroutine(ShootProjectiles());
    }

    private IEnumerator ShootProjectiles()
    {
        int i = 0;
        while(i<projectileCount)
        {
            inkSpawn.position = inkSpawnRef.position;
            Vector3 temp = inkSpawn.position;
            temp.x = variance;
            inkSpawn.position = temp;
            Instantiate(projectile, inkSpawn);
            inkSpawn.DetachChildren();
            variance = Random.Range(-10f, 10f);
            yield return new WaitForSeconds(shotDelay);
            i++;
        }
        //StartCoroutine(ShotCooldown());
    }

    public void CallShotCooldown()
    {
        StartCoroutine(ShotCooldown());
    }

    public void CallEndMinigame(bool res)
    {
        StartCoroutine(EndMinigame(res));
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(ShootProjectiles());
    }
   
}
