using System.Collections;
using UnityEngine;

public class PhysicsDrivenAnimation : MonoBehaviour
{
    public GameObject ragdollSkeleton;   // Het ragdoll skelet met renderers en rigidbodies
    public GameObject animationSkeleton; // Het animatie skelet dat de animatie bepaalt

    public float lerpSpeed = 5f;         // Hoe snel lichaamsdelen teruggaan naar de animatiepositie
    public float forceThreshold = 0.1f;  // Threshold voor hoeveel kracht een lichaamsdeel nodig heeft om af te wijken van de animatie
    public float snapThreshold = 0.1f;   // Drempelwaarde voor wanneer het lichaamsdeel dicht genoeg is om te "snappen" naar de animatiepositie

    private Rigidbody[] ragdollRigidbodies;  // Rigidbodies van het ragdoll-skelet
    private Transform[] ragdollTransforms;   // Transforms van het ragdoll-skelet
    private Transform[] animationTransforms; // Transforms van het animatie-skelet

    // Start wordt één keer aangeroepen bij het begin
    void Start()
    {
        // Haal de rigidbodies van het ragdoll skelet op
        ragdollRigidbodies = ragdollSkeleton.GetComponentsInChildren<Rigidbody>();

        // Haal de transforms van beide skeletten op
        ragdollTransforms = ragdollSkeleton.GetComponentsInChildren<Transform>();
        animationTransforms = animationSkeleton.GetComponentsInChildren<Transform>();

        // Zorg ervoor dat physics altijd actief is
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false; // Physics altijd actief
        }
    }

    // Update wordt elke frame aangeroepen
    void Update()
    {
        // Controleer de minimale lengte van de arrays om out-of-bounds fouten te voorkomen
        int count = Mathf.Min(ragdollTransforms.Length, animationTransforms.Length, ragdollRigidbodies.Length);

        // Loop door elk lichaamsdeel van de ragdoll
        for (int i = 0; i < count; i++)
        {
            Rigidbody rb = ragdollRigidbodies[i];
            Transform ragdollTransform = ragdollTransforms[i];
            Transform animationTransform = animationTransforms[i];

            // Als er geen significante kracht op het lichaamsdeel staat, handmatig terug-lerpen naar de animatie
            if (rb.velocity.magnitude < forceThreshold)
            {
                // Handmatige lineaire interpolatie voor positie
                ragdollTransform.position = CustomLerp(
                    ragdollTransform.position,
                    animationTransform.position,
                    Time.deltaTime * lerpSpeed
                );

                // Handmatige interpolatie voor rotatie
                ragdollTransform.rotation = CustomLerpRotation(
                    ragdollTransform.rotation,
                    animationTransform.rotation,
                    Time.deltaTime * lerpSpeed
                );

                // Controleer of het lichaamsdeel dicht genoeg bij de animatiepositie is om te snappen
                if (Vector3.Distance(ragdollTransform.position, animationTransform.position) < snapThreshold)
                {
                    ragdollTransform.position = animationTransform.position;
                    ragdollTransform.rotation = animationTransform.rotation;
                    rb.velocity = Vector3.zero; // Stop de beweging van het lichaamsdeel
                    rb.angularVelocity = Vector3.zero; // Stop de draaiing van het lichaamsdeel
                }
            }
        }
    }

    // Handmatige lineaire interpolatie voor Vector3
    private Vector3 CustomLerp(Vector3 current, Vector3 target, float lerpFactor)
    {
        return new Vector3(
            current.x + (target.x - current.x) * lerpFactor,
            current.y + (target.y - current.y) * lerpFactor,
            current.z + (target.z - current.z) * lerpFactor
        );
    }

    // Handmatige lerp voor rotatie (Quaternion slerp-achtig gedrag)
    private Quaternion CustomLerpRotation(Quaternion current, Quaternion target, float lerpFactor)
    {
        return new Quaternion(
            current.x + (target.x - current.x) * lerpFactor,
            current.y + (target.y - current.y) * lerpFactor,
            current.z + (target.z - current.z) * lerpFactor,
            current.w + (target.w - current.w) * lerpFactor
        ).normalized; // Normaliseer om te zorgen dat het een geldige rotatie blijft
    }
}
