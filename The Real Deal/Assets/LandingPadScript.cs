using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPadScript : MonoBehaviour
{
    public PlayerController player;
    public GameObject menu;

    public enum LandingStates{
        CanLand,
        OpenPad,
        Landed
    }

    public LandingStates landingState;
    // Start is called before the first frame update
    void Start()
    {
        landingState = LandingStates.OpenPad;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(landingState == LandingStates.Landed){
            player.transform.position = Vector3.Lerp(player.transform.position, transform.position + Vector3.up, 2 * Time.deltaTime);
            
        }

        if(Input.GetButtonDown("Land Ship")){
            if(landingState == LandingStates.CanLand){
                player.active = false;
                menu.SetActive(true);
                player.shipRigid.velocity = Vector3.zero;
                landingState = LandingStates.Landed;
            }else if(landingState == LandingStates.Landed){
                player.active = true;
                menu.SetActive(false);
                player.shipRigid.velocity = Vector3.up;
                landingState = LandingStates.CanLand;
            }
        }
    }

    void OnTriggerEnter(Collider collider){
        if(collider.transform.parent.tag == "Player"){
            Debug.Log("Want to Land?");
            landingState = LandingStates.CanLand;
        }
    }

        void OnTriggerExit(Collider collider){
        if(collider.transform.parent.tag == "Player"){
            Debug.Log("Goodbye!");
            landingState = LandingStates.OpenPad;
        }
    }
}
