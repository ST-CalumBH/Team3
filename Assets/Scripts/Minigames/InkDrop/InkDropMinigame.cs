using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InkDropMinigame : Minigame
{
    
    Transform inkSpawnRef;
    
    public Transform inkSpawn;
    public GameObject projectile;
    public Slider timebar;

    float variance;
    float time = 0;
    public float maxTime = 15f;
    public int projectileCount = 3;
    public float startGrace = 3f;
    public float shotDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        variance = Random.Range(-10f, 10f);
        inkSpawnRef = inkSpawn;
        timebar.maxValue = maxTime;
        StartCoroutine(WarmUp());
        StartCoroutine(Timer());
    }

    //Update is called once per frame
    void Update()
    {
        if(time >= maxTime)
        {
            StartCoroutine(EndMinigame(true));
        }
    }

    IEnumerator Timer()
    {
        time += 0.01f;
        timebar.value = time;
        yield return new WaitForSeconds(0.01f);
        if (time <= maxTime)
        {
            StartCoroutine(Timer());
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
        while(i < projectileCount)
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
            Debug.Log(i.ToString());
        }
        StartCoroutine(ShotCooldown());
    }

    public void CallEndMinigame(bool res)
    {
        StartCoroutine(EndMinigame(res));
    }

    IEnumerator ShotCooldown()
    {
        float cooldown = shotDelay * projectileCount;
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(ShootProjectiles());
    }
   
}
