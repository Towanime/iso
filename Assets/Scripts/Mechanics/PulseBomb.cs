using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBomb : MonoBehaviour
{
    [Tooltip("Number of seconds before the explosion.")]
    public float countdown;
    [Tooltip("Damage to apply to the surronding damageable entities.")]
    public float damage;
    [Tooltip("Force to apply to the rigid bodies in the area.")]
    public float power;
    [Tooltip("Radius of the explosion.")]
    public float radius;
    [Tooltip("Extra time for the death entities after the explosion.")]
    public float onDeathDelay;
    private float currentTime;
    
    // Update is called once per frame
    void Update()
    {
        // activate countdown
        currentTime += Time.deltaTime;
        if (currentTime >= countdown)
        {
            Detonate();
        }
    }

    void Detonate()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        // get all the rigid bodies around
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // deteach them if needed so they can fly around
                hit.transform.parent = null;
                // apply explosion force
                rb.AddExplosionForce(power, explosionPosition, radius, 3.0F);
            }
            // also apply delayed damage
            DamageableEntity entity = hit.GetComponent<DamageableEntity>();
            if(entity != null)
            {
                // check if they die instantly
                bool noDelay = hit.CompareTag("Player") || hit.CompareTag("Instant Death");
                entity.OnDamage(gameObject, damage, noDelay ? 0 : onDeathDelay);
            }
        }
        // destroy bomb too
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // stay where they touch
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        // remove the parent of the collision so they can be affected by the explosion in the platforms
        collision.transform.parent = null;
    }
}
