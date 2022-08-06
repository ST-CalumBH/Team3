using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenMovement : MonoBehaviour
{
    [SerializeField] private int lives;

    private Animator anim;

    private bool win = false;

    private float x;
    private float y;

    void Start()
    {
        x = 0f;
        y = 0f;
        anim = GetComponent<Animator>();
        InvokeRepeating("DirectionModifier", 0, 1.5f);
    }

    void Update()
    {
        if (win == false)
        {
            transform.Translate(x * Time.deltaTime, y * Time.deltaTime, 0);
        }
    }

    private void DirectionModifier()
    {
        x = Random.Range(-5, 5);
        y = Random.Range(-5, 5);
    }

    public void Hit()
    {
        anim.Play("hit");
        win = true;
    }
}
