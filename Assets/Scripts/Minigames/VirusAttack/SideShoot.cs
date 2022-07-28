using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShoot : MonoBehaviour
{
    public float fireDelay = 0.2f;
    private float fireElaspedTime = 0f;

    public GameObject bullet;

    void Update()
    {
        fireElaspedTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireElaspedTime >= fireDelay)
        {
            fireElaspedTime = 0f;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        temp.transform.SetParent(transform.parent.transform, true);
    }
}
