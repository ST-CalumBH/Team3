using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InkDropMinigame : Minigame
{
    public Transform inkSpawn;
    Transform inkSpawnEdit;

    public GameObject projectile;

    public TextMeshProUGUI inkCounter;

    float variance;
    public int inkCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        variance = Random.Range(-10f, 10f);
        inkCounter.text = "0";
        ShootProjectile();
    }

    //Update is called once per frame
    void Update()
    {
        inkCounter.text = inkCollected.ToString();
        if(inkCollected == 7)
        {
            EndMinigame();
        }
    }

    private void ShootProjectile()
    {
        inkSpawnEdit.localPosition.Set(inkSpawn.localPosition.x, inkSpawn.localPosition.y + variance, inkSpawn.localPosition.z);
        Instantiate(projectile, inkSpawnEdit);
        variance = Random.Range(-10f, 10f);
        //StartCoroutine(ShotCooldown());
    }

    public void CallShotCooldown()
    {
        StartCoroutine(ShotCooldown());
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(2f);
        ShootProjectile();
    }
   
}
