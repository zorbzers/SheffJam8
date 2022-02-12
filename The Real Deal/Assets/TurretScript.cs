using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public float sensitivity;
    public float maxAngle;
    public float minAngle;
    public float turretAssist;
    public Transform turretBarrel;
    public Transform spawnPoint;
    public bool canFire;
    public Transform player;
    public GunBase turretStats;


    public void Fire(Vector3 aimPoint){
        turretStats.Fire(spawnPoint.position, turretBarrel.rotation, aimPoint, player);
    }

    public void StopFire(){
        turretStats.StopFire();
    }

    private float angle;
    public void TurnTurret(Vector3 aimPoint){
        float offset = Vector2.Dot(turretBarrel.right, (aimPoint - turretBarrel.position).normalized);
        float lookingAhead = Vector2.Dot(turretBarrel.up, (aimPoint - turretBarrel.position).normalized);
        if(Mathf.Abs(offset) < turretAssist && lookingAhead > 0){
            canFire = true;
        }else{
            canFire = false;
            StopFire();
        }
        angle -= offset * sensitivity;
        angle =  Mathf.Clamp(angle, minAngle, maxAngle);
        turretBarrel.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
