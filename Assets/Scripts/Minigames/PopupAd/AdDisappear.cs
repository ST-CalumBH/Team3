using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdDisappear : MonoBehaviour
{
    [SerializeField] private popupadManager manager;

    void Awake()
    {
        manager = GameObject.Find("gameManager").GetComponent<popupadManager>();
        manager.AdAdded();
    }

    public void DestroySelf()
    {
        manager.AdDeleted();
        Destroy(gameObject);
    }
}
