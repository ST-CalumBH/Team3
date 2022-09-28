using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unFreezeCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
