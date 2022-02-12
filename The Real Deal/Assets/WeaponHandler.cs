using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public TurretHardpoint[] turretPoints;
    public GameObject[] turretTypes;
    public int selectedWeapon;
    private int currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = selectedWeapon;
        foreach (Transform turretPoint in transform){
            if(turretPoint.tag == "TurretPoint"){
                GameObject t = Instantiate(turretTypes[selectedWeapon], turretPoint.position, turretPoint.rotation) as GameObject;
                t.transform.parent = turretPoint;
                t.GetComponent<TurretScript>().player = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWeapon != selectedWeapon){
            currentWeapon = selectedWeapon;
            foreach (Transform turretPoint in transform){
                if(turretPoint.tag == "TurretPoint"){

                    Destroy(turretPoint.GetChild(0).gameObject);

                    GameObject t = Instantiate(turretTypes[currentWeapon], turretPoint.position, turretPoint.rotation) as GameObject;
                    t.transform.parent = turretPoint;
                    t.GetComponent<TurretScript>().player = transform;
                }
            }
        }
    }
}
