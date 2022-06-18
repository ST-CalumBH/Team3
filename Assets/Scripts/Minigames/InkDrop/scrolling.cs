using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class scrolling : MonoBehaviour
{
    public float speed = .3f;
    private Renderer r;
    // Use this for initialization
    void Start()
    {
        r = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * speed);
        r.material.mainTextureOffset = offset;
    }
}