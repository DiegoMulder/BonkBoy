using UnityEngine;

public class PhysicsDrivenAnimation : MonoBehaviour
{
    public Transform animationBone;
    public float forceMultiplier = 10f;  // Controls how much force is applied to correct the position
    public float torqueMultiplier = 10f; // Controls how much torque is applied to correct the rotation
    public float damping = 0.5f;         // Damping factor to smooth the velocity change

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 10f; // Set a reasonable limit for angular velocity to avoid over-rotation
    }

    void FixedUpdate()
    {
        // Calculate position error
        Vector3 positionDifference = animationBone.position - transform.position;
        Vector3 velocityError = -rb.velocity;  // The velocity error is the negative of the current velocity

        // PD controller for position
        Vector3 force = (positionDifference * forceMultiplier) + (velocityError * damping);
        rb.AddForce(force);

        // Calculate rotation difference
        Quaternion rotationDifference = animationBone.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);  // Get angle-axis from quaternion
        Vector3 angularVelocityError = -rb.angularVelocity;  // The angular velocity error is the negative of the current angular velocity

        // PD controller for rotation
        Vector3 torque = (axis * angle * torqueMultiplier) + (angularVelocityError * damping);
        rb.AddTorque(torque);
    }
}
