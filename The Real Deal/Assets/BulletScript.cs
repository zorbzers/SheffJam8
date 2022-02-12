using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody bulletRigid;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigid.velocity = (transform.up * bulletSpeed);
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter(Collision collision){
        Destroy(gameObject);
    }
}
