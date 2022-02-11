using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("Velocity Configuration")]
    public float toVel = 2.5f;
    public float maxVel = 15.0f;
    public float maxForce = 40.0f;
    public float gain = 5f;

    [Header("Moving Points")]
    public Transform[] destination;

    private Rigidbody2D _rb2D;

    private Vector2 currentTargetPosition;
    private int currentIndex = 1;

    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        currentTargetPosition = destination[currentIndex].position;
        Debug.Log(destination.Length);
    }


    void FixedUpdate()
    {
        Vector2 dist = currentTargetPosition - (Vector2)transform.position;
        //dist.y = 0; // ignore height differences
        // calc a target vel proportional to distance (clamped to maxVel)
        Vector2 tgtVel = Vector2.ClampMagnitude(toVel * dist, maxVel);
        // calculate the velocity error
        Vector2 error = tgtVel - _rb2D.velocity;
        // calc a force proportional to the error (clamped to maxForce)
        Vector2 force = Vector2.ClampMagnitude(gain * error, maxForce);
        _rb2D.AddForce(force);

        if(Vector2.Distance(transform.position,currentTargetPosition) < 0.1f)
        {
            _rb2D.velocity = Vector2.zero;
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        Debug.Log("ISAMCN");
        if(currentIndex < destination.Length-1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
        currentTargetPosition = destination[currentIndex].position;
    }
}
