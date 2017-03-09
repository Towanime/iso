using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Tooltip("Line renderer for the laser.")]
    public LineRenderer line;
    [Tooltip("Starting point for the laser.")]
    public GameObject emissor;
    [Tooltip("Total distancefor the laser from the start point.")]
    public float distance = 80;
    [Tooltip("Damage to apply on contact.")]
    public float damage;
    [Tooltip("Player avatar that will be rotated.")]
    public float damageRate = 1;
    // used while there is a damageable entity touching the laser
    private DamageableEntity target;
    private bool firstHit;

    // Use this for initialization
    void Start () {
        StartCoroutine("FireLaser");
    }

    private IEnumerator FireLaser()
    {
        while (true)
        {
            Ray ray = new Ray(emissor.transform.position, emissor.transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hit, distance))
            {
                line.SetPosition(1, hit.point);
                target = hit.collider.gameObject.GetComponent<DamageableEntity>();
                //Debug.Log("Hitting: " + hit.collider.name + " with damageable entity? " + (target != null));
                if (hit.collider.CompareTag("Player") && target != null && !firstHit)
                {
                    DoDamage();
                    firstHit = true;
                }else
                {
                    // reset hit if it's not a damageable entity
                    firstHit = false;
                }
            }
            else
            {
                target = null;
                firstHit = false;
                line.SetPosition(1, ray.GetPoint(distance));
            }
            yield return null;
        }
    }

    /// <summary>
    /// Do damage on the hit entity every X seconds (damage rate)
    /// </summary>
    private void DoDamage()
    {
        if (target)
        {
            target.OnDamage(gameObject, damage);
            // call it again
            Invoke("DoDamage", damageRate);
        }
    }
}
