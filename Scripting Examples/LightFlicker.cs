using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * A simple script to add light variation to a game object in a scene
 */
public class LightFlicker : MonoBehaviour
{
    Light l1;
    void Start()
    {
        l1 = this.GetComponent<Light>();
    }

    void Update()
    {
        //Single line of code causing a flickering
        l1.intensity = Mathf.Lerp(1f,(Random.Range(150f, 160f)/100f),Time.deltaTime*50);
    }
}
