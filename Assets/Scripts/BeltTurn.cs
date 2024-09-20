using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltTurn : MonoBehaviour
{
    public Transform playerHead;      // Reference to the VR headset or camera (player's head)
    public Vector3 waistOffset;       // Offset to position the belt at the player's waist (relative to the playerHead)

    void Update()
    {
        // Get the player's head position (camera position)
        Vector3 headPosition = playerHead.position;

        // Apply the waist offset to the position (keeping the X and Z from head position, adjusting Y with the offset)
        Vector3 waistPosition = new Vector3(headPosition.x, headPosition.y + waistOffset.y, headPosition.z);

        // Set the belt's position to match the waist's position
        transform.position = waistPosition;

        // Get the player's Y-axis rotation (yaw), ignoring pitch (X-axis) and roll (Z-axis)
        float headYRotation = playerHead.eulerAngles.y;

        // Apply only the Y-axis rotation to the belt, keeping its own X and Z rotations as they are
        transform.rotation = Quaternion.Euler(0, headYRotation, 0);
    }
}
