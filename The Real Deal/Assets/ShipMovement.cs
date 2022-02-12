using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    private Vector3 velocity;
    [System.Serializable]
    public class MovementValues
    {

        public Vector3 maxSpeedVector;
        public Vector3 maxForceVector;
        public float inputSlowdownMultiplier; //Used for then there are no inputs and the ship needs to slow down on itself (used so it doesn't just come to an immediate stop and is a nice smooth slowdown)
        

        public bool thrust;
        public float speedMultiplier;
        public float forceMultiplier;
    }


    private MovementValues movementValues = new MovementValues();
    public ParticleSystem[] thrusterEffects;

    // Start is called before the first frame update
    public void SetVelocity(Vector3 receivedVelocity)
    {
        velocity = receivedVelocity; //sets the velocity of the ship
    }

    public void SetMovementValues(MovementValues receivedMovementValues)
    {
        movementValues = receivedMovementValues; //some jank to get this working
    }

    // Update is called once per frame
    public Vector3 CalculateFinalInput(Vector3 inputVector, Vector3 maxSpeedVector) //receives a vector holding the inputs and a vector of the maximum speeds in each axis
    {
        //calculates in which directions the ship needs to thrust depending on player input.
        Vector3 lateralThrustVector = HandleForce(Vector3.right, inputVector.y, movementValues.maxForceVector.y * 1000, maxSpeedVector.y);
        Vector3 verticalThrustVector = HandleForce(Vector3.up, inputVector.z, movementValues.maxForceVector.z * 1000, maxSpeedVector.z);
        Vector3 finalInput = lateralThrustVector + verticalThrustVector;
        return finalInput; //returns a final physics vector for unity's rigidbody to use.
    }

    void HandleThrusterEffects(Vector3 axisDirection, float inputReceived)
    {
        foreach (ParticleSystem thruster in thrusterEffects)
        {
            float direction = Vector3.Dot(-thruster.transform.forward, axisDirection * inputReceived);

            if (direction > 0.05f)
            {
                thruster.Play();
            }
        }
    }

    Vector3 HandleForce(Vector3 axisDirection, float input, float force, float maxValue){
        float velocityDotProduct = Vector3.Dot(axisDirection, velocity); //get a dot product of the the axis of the ship and the direction it is travelling
        float localPlayerInput = input * 2; //the multiplication is really a buffer, separates the player input in another variable to be used for the actual input (for visual reasons)

        if (Mathf.Abs(velocityDotProduct) > maxValue) //check if the ship has reached cruise speed
        {
            if(localPlayerInput > 0 && velocityDotProduct > 0 || localPlayerInput < 0 && velocityDotProduct < 0)
            {
                localPlayerInput = 0; //set the player input to 0, as it should stop receiving inputs in this direction
            }
            
        }
        float inputDirection = localPlayerInput + Mathf.Clamp(-velocityDotProduct * Mathf.Clamp(movementValues.inputSlowdownMultiplier, 0, 1), -1, 1); // calculate the direction the ship needs to thrust in. as you can see it takes both player input and counter thrust into consideration.

        if (movementValues.thrust) HandleThrusterEffects(axisDirection, input + inputDirection);

        return axisDirection *  Mathf.Clamp(inputDirection, -1, 1) * force; //return a velocity vector in with the right direction and force.
    }
}
