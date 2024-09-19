using UnityEngine;

public class RagdollCollisionResponse : MonoBehaviour
{
    // Kracht die we toepassen wanneer een lichaamsdeel geraakt wordt
    public float forceMultiplier = 5.0f;

    // De verschillende lichaamsdelen met rigidbodies
    [SerializeField] private Rigidbody[] rigidbodies;

    // Activeer de ragdoll als een botsing optreedt
    private bool isRagdollActive = false;

    // Start wordt aangeroepen bij het begin van de game
    void Start()
    {
        // Haal alle Rigidbodies van de character binnen
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        // Zorg dat alle ledematen physics hebben, maar niet actief zijn (de animator stuurt de bewegingen)
        SetRagdollActive(false);
    }

    // Activeer of deactiveer de ragdoll
    public void SetRagdollActive(bool active)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !active; // Als niet actief, laat de animator sturen; anders physics
        }
        isRagdollActive = active;
    }

    // Als er een botsing plaatsvindt
    private void OnCollisionEnter(Collision collision)
    {
        // Check of de botsing significant is (je kan bijvoorbeeld een minimum kracht vereisen)
        if (collision.relativeVelocity.magnitude > 2f)
        {
            // Activeer de ragdoll bij een harde botsing
            SetRagdollActive(true);

            // Toepassen van kracht op het deel van de ragdoll dat wordt geraakt
            Rigidbody hitRigidbody = collision.rigidbody;
            if (hitRigidbody != null)
            {
                Vector3 forceDirection = collision.contacts[0].normal * -1; // Kracht in de richting weg van de botsing
                hitRigidbody.AddForce(forceDirection * collision.relativeVelocity.magnitude * forceMultiplier, ForceMode.Impulse);
            }

            // Eventueel kun je na een tijdje de ragdoll weer terugzetten naar animatie (recovery)
            Invoke("ResetToAnimation", 3f); // 3 seconden na de botsing wordt de ragdoll weer naar de animatie teruggezet
        }
    }

    // Herstel naar animatie
    private void ResetToAnimation()
    {
        // Zet de ragdoll physics uit en laat de animatie weer sturen
        SetRagdollActive(false);
    }
}
