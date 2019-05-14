using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] private MouseLook m_MouseLook;

        public GameObject rig;
        public Rigidbody player;
        public float thrust = 0.5f;
        public int count;

        public Camera m_Camera;

        // Start is called before the first frame update
        void Start()
        {
            player = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

            //m_MouseLook.LookRotation(transform, m_Camera.transform);

            rig.transform.rotation = new Quaternion(rig.transform.rotation.x, m_Camera.transform.rotation.y, rig.transform.rotation.z, rig.transform.rotation.w);
            m_Camera.transform.rotation = new Quaternion(m_Camera.transform.rotation.x, 0, m_Camera.transform.rotation.z, m_Camera.transform.rotation.w);

            if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.DPadUp))
            {
                Debug.Log("Press DpadUp detected");
                count++;
                rig.transform.position = new Vector3(rig.transform.position.x, rig.transform.position.y, rig.transform.position.z + thrust);
                //player.AddForce(transform.forward * thrust);
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
}