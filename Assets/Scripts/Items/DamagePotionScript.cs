using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DamagePotionScript : MonoBehaviour
{
    PlayerSword playerSword;
    public bool isSelected = false;
    public bool testDamage = false;
    private InputDevice rightController;

    public float potionDuration = 10.0f;
    private bool potionActive = false;
    private float potionTimer = 0f;

    private void Start()
    {
        playerSword = GameObject.FindGameObjectWithTag("Sword").GetComponent<PlayerSword>();
        var rightHandedControllers = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandedControllers);
        if (rightHandedControllers.Count > 0)
        {
            rightController = rightHandedControllers[0];
        }
    }

    public void IsSelected()
    {
        isSelected = true;
    }

    public void IsNotSelected()
    {
        isSelected = false;
    }

    public void UseDamagePotion()
    {
        playerSword.DamagePotionGrab();
        potionActive = true;
        potionTimer = potionDuration;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (!rightController.isValid)
        {
            var rightHandedControllers = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandedControllers);
            if (rightHandedControllers.Count > 0)
            {
                rightController = rightHandedControllers[0];
            }
        }

        if (rightController.isValid && isSelected)
        {
            bool triggerPressed;
            if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
            {
                testDamage = true;
            }
        }

        if (testDamage && !potionActive)
        {
            UseDamagePotion();
            testDamage = false;
        }

        if (potionActive)
        {
            potionTimer -= Time.deltaTime;
            if (potionTimer <= 0)
            {
                EndPotionEffect();
            }
        }
    }

    private void EndPotionEffect()
    {
        playerSword.RemovePotionEffect();
        potionActive = false;
        Destroy(gameObject); 
    }
}
