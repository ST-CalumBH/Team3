using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetect : MonoBehaviour
{
    private bool isDistracted = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDistracted)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.SendMessage("DestroySelf");
            }
        }
    }

    public void distractionChange()
    {
        isDistracted = !isDistracted;
    }
}
