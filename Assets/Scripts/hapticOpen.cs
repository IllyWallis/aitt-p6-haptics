using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class hapticOpen : MonoBehaviour
{
    protected SteamVR_Behaviour_Pose controller;

    public float newAngle;
    public float angle;

    public float angularDifference;

    

    // Start is called before the first frame update
    void Start()
    {
        angle = gameObject.GetComponent<HingeJoint>().angle;
    }

    // Update is called once per frame
    void Update()
    {
        newAngle = gameObject.GetComponent<HingeJoint>().angle;
        angularDifference = newAngle - angle;
        //print(controller);

        if (controller != null && SteamVR_Input.GetState("GrabPinch", controller.inputSource))
        {
            //print("true");
            float duration = 0.01f;
            float frequency = 0;
            float strength = 0;

            if (angularDifference > 0)
            {
                frequency = angularDifference * 2.0f;
                strength = frequency;
            }
            else if (angularDifference < 0)
            {
                frequency = -angularDifference * 2.0f;
                strength = frequency;
            }
            
            SteamVR_Actions.default_Haptic[controller.inputSource].Execute(0, duration, frequency, strength);
        }
        angle = newAngle;

        if (controller != null && SteamVR_Input.GetStateUp("GrabPinch", controller.inputSource))
        {
            print("RESET");
            controller = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (controller == null)
        {
            controller = other.gameObject.GetComponent<SteamVR_Behaviour_Pose>();
        }
    }
}
