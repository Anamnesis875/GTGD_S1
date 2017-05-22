using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the Blaster projectile and it
/// governs the behaviour of the projectile.
/// </summary>

public class BlasterScript : MonoBehaviour
{
    // Variables Start_____________________________________________________________________________
    // The explosion effect is attached to this
    // in the inspector.
    public GameObject blasterExplosion;

    // A quick reference.
    private Transform myTransform;

    // The projectiles flight speed.
    private float projectileSpeed = 10;

    // Prevent the projectile from causing
    // further harm once it has hit something.
    private bool expended = false;

    // A ray projected in front of the projectile
    // to see if it will hit recognisable collider.
    private RaycastHit hit;

    // The range of the ray.
    private float range = 1.5f;

    // The life span of projectile.
    private float expireTime = 5;
    // Variables End_______________________________________________________________________________

    // Use this for initialization
    void Start ()
    {
        myTransform = transform;

        // As soon as the projectile is created start a countdown
        // to destroy.
        StartCoroutine(DestroyMyselfAfterSomeTime());
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Translate the projectile in the up direction (the pointed
        // end of the projectile).
        myTransform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

        // If the ray hits something then execute this code.
        if(Physics.Raycast(myTransform.position, myTransform.up, out hit, range) && 
            expended == false)
        {
            // If the collider has the tag of Floor then..
            if(hit.transform.tag == "Floor")
            {
                expended = true;

                // Instantiate an explosion effect.
                Instantiate(blasterExplosion, hit.point, Quaternion.identity);

                // Make the projectile become invisible.
                myTransform.renderer.enabled = false;

                // Turn off the light. The halo will also disappear.
                myTransform.light.enabled = false;
            }
        }
	}

    IEnumerator DestroyMyselfAfterSomeTime()
    {
        // Wait for the timer to count up to the expireTime
        // and then destroy the projectile.
        yield return new WaitForSeconds(expireTime);
        Destroy(myTransform.gameObject);
    }
}
