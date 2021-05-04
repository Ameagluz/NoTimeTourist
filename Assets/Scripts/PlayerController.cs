using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 1f;
    [SerializeField] float yRange = 1f;
    [SerializeField] float positionPitchFactor = -2f; // changes the pitch depending on the position of the player
    [SerializeField] float positionYawFactor = -5f; // changes the yaw depending on the position of the player
    [SerializeField] float controlPitchFactor = -10f; //changes the pitch depenting on button press
    [SerializeField] float controlRollFactor = 0f; //changes roll depending on putton press
    
    float xThrow;
    float yThrow;   

   
    void Update()

    {
        ProcessTranslation();
        ProcessRotation();
    }

   void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {   float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yaw = yawDueToPosition;

        float rollDueToControllThrow = xThrow * controlRollFactor;
        float roll = rollDueToControllThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //zorgt ervoor dat de volgorde van roteren niet meer uitmaakt
    }

    void OnEnable() 
    {
        movement.Enable();    
    }

    void OnDisable() 
    {
        movement.Disable();  
    }

}
