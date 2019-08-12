using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Locomotion : MonoBehaviour
    {

        public GameObject rig;
        public float thrust = 5f;
        public int count;
        public float xF = 0;
        public float zF = 0;
        public float zRo;
        public float xRo;
        public float Track;
        public float TrackTwo;

        public Camera m_Camera;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            zF = Mathf.Abs(thrust) * Mathf.Cos(Mathf.Abs(m_Camera.transform.eulerAngles.y));
            xF = Mathf.Abs(thrust) * Mathf.Sin(Mathf.Abs(m_Camera.transform.eulerAngles.y));
            rig.gameObject.GetComponent<Rigidbody>().AddForce(xF, 0, zF);


            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadUp))
            {
                Debug.Log("Press DpadUp detected");
                rig.gameObject.GetComponent<Rigidbody>().AddForce(xF, 0, zF);
                count++;

                //Vector2 xInput = ViveInput.GetPadPressAxis(HandRole.RightHand);

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
}