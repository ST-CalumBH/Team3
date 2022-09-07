using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeHeart : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void Disappear()
    {
        anim.Play("lose_heart");
    }
}
