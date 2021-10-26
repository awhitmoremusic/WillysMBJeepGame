using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Creating a generic rotation script using Euler Angles. Script can be called from anywhere allowing objects to be rotated over a given time.
 * 
 * 
 */
public class RotateObject : MonoBehaviour
{

    private static bool ableToRotate = false;
    private static int lerpDuration = 0;

    private static Vector3 currentAngle;
    private static Vector3 targetAngle;
   
    private static Transform objectToRotate;

    static float timeElapsed;
    

    // Update is used to make the rotation and is switched on by bool ableToRotate
    void Update()
    {
        //the global rotation of objects in the game. Once the parameters have been passed in the bool controls whether the supplied object will rotate
        if (ableToRotate)
        {
            currentAngle = new Vector3(
              Mathf.LerpAngle(currentAngle.x, targetAngle.x, timeElapsed/lerpDuration),
              Mathf.LerpAngle(currentAngle.y, targetAngle.y, timeElapsed/lerpDuration),
              Mathf.LerpAngle(currentAngle.z, targetAngle.z, timeElapsed/lerpDuration));

            objectToRotate.eulerAngles = currentAngle;
            timeElapsed += Time.deltaTime;

        }
        //this statement switches off the rotations and resets the rotation values
        if (timeElapsed >= lerpDuration && ableToRotate==true)
        {
            print("resetValues");
            ableToRotate = false;
            timeElapsed = 0;
        }

       
        
    }


    //Given a gameobject, an initial and final rotation can be lerped using a time frame
    public static void prepare1ObjectToRotate(GameObject obj, Vector3 initialRotation, Vector3 finalRotation, int timeInSeconds)
    {
        currentAngle = initialRotation;
        targetAngle = finalRotation;
        lerpDuration = timeInSeconds;
        objectToRotate = obj.transform;
        timeElapsed = 0;
        ableToRotate = true;
    }

    //When only one XYZ component is required we can rotate just a single euler angle
    public static void prepare1ObjectToRotateSingleAngle(GameObject obj, Vector3 initialRotation, int additionalAngle, int XYZangle, int timeInSeconds)
    {
        // print("intial rotation " + initialRotation);
        currentAngle = initialRotation;

        //target angle is compiled from the int component passed in
        if (XYZangle == 0)
        {
            targetAngle = (initialRotation += new Vector3(additionalAngle, 0, 0));
        }
        else if (XYZangle == 1)
        {
            targetAngle = (initialRotation += new Vector3(0, additionalAngle, 0));
        }
        else if (XYZangle == 2)
        {
            targetAngle = (initialRotation += new Vector3(0, 0, additionalAngle));
        }

        
        timeElapsed = 0;
        lerpDuration = timeInSeconds;
        objectToRotate = obj.transform;
        ableToRotate = true;
    }



}
