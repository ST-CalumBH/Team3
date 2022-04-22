using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargedSpatula : MonoBehaviour
{
    public float speedSpatula = 10f;
    public float speedPlayer = 600f;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * speedSpatula * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(Vector3.forward * speedPlayer * Time.deltaTime);
        }

    }

}
