using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    public float warkSpeed = 4f;
    public float maxVelocityChange = 10f;


    private Vector3 _input;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _input.Normalize();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(CalculateMovement(warkSpeed), ForceMode.VelocityChange);
    }

    private Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(_input.x, 0, _input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= speed;

        Vector3 velocity = _rb.velocity;

        if (_input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;

            return (velocityChange);
        }
        else
        {
            return new Vector3();
        }  
    }
}
