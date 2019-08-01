using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Locomotion : MonoBehaviour
    {

        public GameObject rig;
        public float thrust = 10f;
        public int count;
        public float xF = 0;
        public float zF = 0;


        public Camera m_Camera;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            zF = thrust * Mathf.Cos(m_Camera.transform.eulerAngles.y * Mathf.Deg2Rad);
            xF = thrust * Mathf.Sin(m_Camera.transform.eulerAngles.y * Mathf.Deg2Rad);

            rig.gameObject.GetComponent<Rigidbody>().AddForce(xF, 0, zF);

            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadUp))
            {
                Debug.Log("Press DpadUp detected");
                count++;

                rig.gameObject.GetComponent<Rigidbody>().AddForce(xF, 0, zF);

                //Vector2 xInput = ViveInput.GetPadPressAxis(HandRole.RightHand);

            }

            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadDown))
            {
                thrust = -thrust;
                rig.gameObject.GetComponent<Rigidbody>().AddForce(-xF, 0, -zF);
                Debug.Log("Press DpadDown detected");

            }

            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadLeft))
            {

                rig.gameObject.GetComponent<Rigidbody>().AddForce(thrust, 0, 0);
                Debug.Log("Press DpadLeft detected");
            }

            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadRight))
            {

                rig.gameObject.GetComponent<Rigidbody>().AddForce(thrust, 0, 0);
                Debug.Log("Press DpadRight detected");
            }


        }
    }
}