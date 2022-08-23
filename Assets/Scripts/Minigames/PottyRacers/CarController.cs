using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //car basic stuff
    [Header("Car Settings")]

    public float driftFactor = 0.95f;
    public float maxSpeed = 20;

    public float accelFactor = 30.0f;
    public float turnFacotor = 3.5f;

    //local variables
    float accelInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;
    float velocityVsUp = 0;

    //components
    Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {

        ApplyEngineForce();

        killSideVelocity();

        ApplySteering();

    }

    void ApplyEngineForce()

    {

        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //limit the forward speed
        if (velocityVsUp > maxSpeed && accelInput > 0)
            return;

        //limit reverse speed
        if (velocityVsUp < -maxSpeed * 0.5f && accelInput < 0)
            return;

        //limit side speed
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed  * maxSpeed && accelInput > 0)
            return;


        //applying drag or friction when zero accel

        if (accelInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelInput * accelFactor;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);


    }

    void ApplySteering()
    {

        //limit car turning when no forward momentum is being applied
        // value of 8 can be changed
        float minSpeedBeforeTurn = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeTurn = Mathf.Clamp01(minSpeedBeforeTurn);

        rotationAngle -= steeringInput * turnFacotor * minSpeedBeforeTurn;

        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelInput = inputVector.y;
    
    }

    void killSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

}
