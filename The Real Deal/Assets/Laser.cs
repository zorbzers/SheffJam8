using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : GunBase
{
    public LineRenderer laserLineThing;
    public Color laserColour;
    public Color laserHideColour;
    public LayerMask laserMask;

    public override void HoldFire(Vector3 position, Vector3 aimPoint, Transform player)
    {
        laserLineThing.startColor = laserColour;
        laserLineThing.endColor = laserColour;
        laserLineThing.SetPosition(0, position);
        laserLineThing.SetPosition(1, player.position + (aimPoint - player.position).normalized);

        RaycastHit hitDetect;
        Vector3 hitPos = Vector3.zero;
        if(Physics.Raycast(player.position + (aimPoint - player.position).normalized, aimPoint - player.position, out hitDetect, ((player.position + (aimPoint - player.position).normalized) - aimPoint).magnitude, laserMask)){
            hitPos = hitDetect.point;
        }
        if(hitPos != Vector3.zero){
            laserLineThing.SetPosition(2, hitPos);
        }else{
            laserLineThing.SetPosition(2, aimPoint);
        }

    }

    public override void OnFire(Vector3 position, Quaternion rotation, Vector3 aimPoint, Transform player)
    {
        RaycastHit hitDetect;
        Vector3 hitPos = Vector3.zero;
        if(Physics.SphereCast(player.position + (aimPoint - player.position).normalized, 0.2f, aimPoint - player.position, out hitDetect, (player.position - aimPoint).magnitude, laserMask)){
            hitPos = hitDetect.point;
        }
        if(hitPos != Vector3.zero){
            //deal Damage
            Debug.Log("Hit!");
        }
    }

    public override void StopFire()
    {
        laserLineThing.startColor = laserHideColour;
        laserLineThing.endColor = laserHideColour;
    }
}
