using UnityEngine;

public class PhysicsDrivenAnimation : MonoBehaviour
{
    public Transform animationBone;
    public float forceMultiplier = 10f;
    public float torqueMultiplier = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 positionDifference = animationBone.position - transform.position;
        Vector3 force = positionDifference * forceMultiplier;
        rb.AddForce(force);

        Quaternion rotationDifference = animationBone.rotation * Quaternion.Inverse(transform.rotation);
        Vector3 torque = new Vector3(rotationDifference.x, rotationDifference.y, rotationDifference.z) * torqueMultiplier;
        rb.AddTorque(torque);
    }
}
