using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class controls the map behavior in the map within the game. 
 *It shows an example of updating 3d elements from a raycast input and then checking mission progression
 */
public class Westville
{

    LineRenderer lineRenderer;

    Vector3 point1;
    Vector3 point2;

    //The constructor gets the lineRender component from the mapline object 
    public Westville()
    {
        lineRenderer = GameObject.Find("MapLine").GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
       
        Debug.Log("Westville Constructor DBL");
    }


    //This method draws the line between two points so that in game we can see a line path between two game objects
    public void SetUpLine(Vector3 X, string hitcolliderTag)
    {
        // Debug.Log("Welcome to Westville DBL");

        //create an Array of two Vector 3s: The starting X position and then where the mouse Raycast has clicked
        var Positions = new Vector3[]{ X, GameObject.FindGameObjectWithTag(hitcolliderTag).transform.position};
        
        //setup lineRenderer with two points
        lineRenderer.positionCount = Positions.Length;
        lineRenderer.SetPositions(Positions);

        //Correct Item is evaluated from the Raycast Hit Tag and comparing it to the static value stored in the Mission Progress script
        if(int.Parse(hitcolliderTag.Split('_')[1]) == MissionProgress.radioMessage)
        {
            //game progress meters are updated and objects are shown on screen
            MissionProgress.completionChecklist[4] = 1;
            GameObject.Find("TickE").transform.localScale = Vector3.one;
            Debug.Log("Correct option selected DBL");
        }
        else 
        { 
            //game progress reset when clicking on wrong value
            MissionProgress.completionChecklist[4] = 0; 
         }

                
    }
}
