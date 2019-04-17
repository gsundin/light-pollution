using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered: " + other.name);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Staying: " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exiting: " + other.name);
    }
}
