using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float m_Gravity = 30.0f;
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;
    public float m_RotateIncrement = 90; // Degrees to snap rotate by.

    public SteamVR_Action_Boolean m_RotatePress = null; // Action for snap rotating.
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;
    
    private float m_Speed = 0.0f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;
    private GameObject player = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // On Start, we will set the player's position
        player = GameObject.Find("VRController");

        if (MenuButtonScripts.Scenario == 1) // If the user hit load scene one (Scenario variable is modified in MenuButtonScripts.cs, it's a static variable).
        {
            //set player transform to outside.
            player.transform.SetPositionAndRotation(new Vector3(1.2f, 6.3f, -45f), new Quaternion(0, 0, 0, 0));
            //possibly add more logic to restrict scenario steps
            
        }
        else if (MenuButtonScripts.Scenario == 2) // If the user hit load scene two (Scenario variable is modified in MenuButtonScripts.cs, it's a static variable).
        {
            player.transform.SetPositionAndRotation(new Vector3(-9.2f, 6.3f, -8.59f), new Quaternion(0, 0, 0, 0));
        }
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    private void Update()
    {
        HandleHeight();
        CalculateMovement();
        SnapRotation();
    }

    private void HandleHeight()
    {
        // Get the head in local space
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        // Cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        // Move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        // Apply
        m_CharacterController.center = newCenter;
    }

    private void CalculateMovement()
    {
        // Figure out movement orientation
        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y, 0); // Keep player in same place in the world
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        // If not moving
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
            m_Speed = 0;

        // If button pressed
        if(m_MovePress.state)
        {
            // Add, clamp
            m_Speed += m_MoveValue.axis.y * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

            // Orientation
            movement += orientation * (m_Speed * Vector3.forward);
        }

        // Gravity
        movement.y -= m_Gravity * Time.deltaTime; // Applies gravity as an acceleration

        // Apply
        m_CharacterController.Move(movement * Time.deltaTime);
    }

    private void SnapRotation()
    {
        float snapValue = 0.0f;

        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
            snapValue = -Mathf.Abs(m_RotateIncrement);

        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
            snapValue = Mathf.Abs(m_RotateIncrement);

        transform.RotateAround(m_Head.position, Vector3.up, snapValue);
    }
}
