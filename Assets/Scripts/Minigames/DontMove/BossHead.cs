using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour
{
    SpriteRenderer rend;
    public Animator anim;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public IEnumerator fadeOut()
    {
        for (float f = 1f; f <= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }

        anim.Play("bossConsume");
    }
}
