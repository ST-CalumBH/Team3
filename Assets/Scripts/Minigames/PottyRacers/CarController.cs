using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //car basic stuff
    [Header("Car Settings")]
    public float accelFactor = 30.0f;
    public float turnFacotor = 3.5f;

    //local variables
    float accelInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    //components
    Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    void FixedUpdate()
    {

        ApplyEngineForce();

        ApplySteering();

    }

    void ApplyEngineForce()

    {
        Vector2 engineForceVector = transform.up * accelInput * accelFactor;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        rotationAngle -= steeringInput * turnFacotor;

        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelInput = inputVector.y;
    
    }


}
