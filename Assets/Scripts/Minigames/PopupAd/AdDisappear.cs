using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdDisappear : MonoBehaviour
{
    [SerializeField] private popUpAdManager manager;

    void Awake()
    {
        manager = GameObject.Find("gameManager").GetComponent<popUpAdManager>();
        manager.AdAdded();
    }

    public void DestroySelf()
    {
        manager.AdDeleted();
        Destroy(gameObject);
    }
}
