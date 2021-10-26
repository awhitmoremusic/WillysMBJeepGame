using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class acts as a global stat holder for the game. For the current build there is no need to provide a more complete solution
    Further updates may make elements seralisable and hold methods to create save states
*/
public struct MissionProgress
{   
    //Mission Progression
    //1=Open Glove Box
    //2=Refuel
    //3=Start Up Jeep
    //4=Get Audio On The Radio
    //5=Set correct
    //6=Set correct gears, handbreak
    //7=Go (press go and finish)

    //Completion Checklist
    //0=Lights
    //1=Fuel
    //2=Ignition
    //3=Radio
    //4=MapSelection/Destination
    //5=Gear
    //6=HandBreak
    
    public static ArrayList completionChecklist = new ArrayList {0, 0, 0, 0, 0, 0, 0};

    public static int radioMessage = -1;

    private static int missionProgression = 0;
    public static int MissionProgression
    {
        get
        {
            return missionProgression;
        }
        set
        {
            missionProgression = value;
        }
    }
}
