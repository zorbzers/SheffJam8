using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float spread;
    public float bulletCount;
    private bool ready;
    private float Timer;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        ready = true;
        if(bulletCount == 0){
            bulletCount = 1;
        }
    }

    public void Fire(Vector3 position, Quaternion rotation, Vector3 aimPoint, Transform player){
        HoldFire(position, aimPoint, player);
        if(ready){
            OnFire(position, rotation, aimPoint, player);
            ready = false;
        }
    }

    public virtual void HoldFire(Vector3 position, Vector3 aimPoint, Transform player){

    }

    public virtual void StopFire(){

    }

    public virtual void OnFire(Vector3 position, Quaternion rotation, Vector3 aimPoint, Transform player){

            for(int i=0;i<bulletCount;i++){
                GameObject bullet = Instantiate(bulletPrefab, position, rotation) as GameObject;
                bullet.transform.Rotate(0, 0, Random.Range(-spread, spread));
            }
    }

    // Update is called once per frame
    void Update()
    {
        float delay = 60 / fireRate;
        if (!ready)
        {
            if (Timer >= delay)
            {
                Timer = 0;
                ready = true;
            }
            else
            {
                Timer += Time.deltaTime;
            }
        }
    }
}
