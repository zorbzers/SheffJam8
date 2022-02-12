using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShipMovement
{
    public bool active;
    public Rigidbody shipRigid;
    public MovementValues shipMovementValues;
    public TurretScript[] turrets;
    public Transform crosshair;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        turrets = gameObject.GetComponentsInChildren<TurretScript>();
    }

    void Update(){
        if(Input.GetButtonUp("Fire1")){
            foreach(TurretScript turret in turrets){
                turret.StopFire();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        turrets = gameObject.GetComponentsInChildren<TurretScript>();
        Vector2 mouseInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
        crosshair.position = aimPoint;
        if(active){
            foreach(TurretScript turret in turrets){
                turret.TurnTurret(aimPoint);
                GunTest(turret, aimPoint);
            }

            Vector3 movementInput = new Vector3(0 ,Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            SetMovementValues(shipMovementValues);
            SetVelocity(shipRigid.velocity);

            Vector3 maxSpeedVector = new Vector3(shipMovementValues.maxSpeedVector.x, shipMovementValues.maxSpeedVector.y, shipMovementValues.maxSpeedVector.z);
            Vector3 finalVector = CalculateFinalInput(movementInput, maxSpeedVector);

            shipRigid.AddForce(finalVector);
        }else{
            foreach(TurretScript turret in turrets){
                turret.TurnTurret(aimPoint);
            }
        }

        float screenCentreX = Screen.width / 2;
            float finalInputX = (Input.mousePosition.x - screenCentreX) / screenCentreX / 2;
            finalInputX = Mathf.Clamp(finalInputX, -1, 1);

            float screenCentreY = Screen.height / 2;
            float finalInputY = (Input.mousePosition.y - screenCentreY) / screenCentreY / 2;
            finalInputY = Mathf.Clamp(finalInputY, -1, 1);

            Vector2 screenCentre = new Vector2(screenCentreX, screenCentreY);
            float distance = (mouseInput - screenCentre).magnitude / 100;

            distance = Mathf.Clamp(5 + distance, 5, 10);

            Camera.main.orthographicSize = distance;
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, Vector3.zero + (Vector3.forward * -10) + (Vector3.up * finalInputY * 10) + (Vector3.right * finalInputX * 10), 6 * Time.deltaTime);
    }

    void GunTest(TurretScript turret, Vector3 aimPoint){
        if(turret.canFire){
            if(Input.GetButton("Fire1")){
                turret.Fire(aimPoint);
            }
        }
    }
}
