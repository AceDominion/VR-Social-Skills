using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class Locomotion : MonoBehaviour
{

    public GameObject rig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadUp))
        {
            Debug.Log("Press DpadUp detected");
            Vector2 xInput = ViveInput.GetPadPressAxis(HandRole.RightHand);

        }

        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadDown))
        {
            Debug.Log("Press DpadDown detected");
        }

        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadLeft))
        {
            Debug.Log("Press DpadLeft detected");
        }

        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadRight))
        {
            Debug.Log("Press DpadRight detected");
        }




    }
}
