using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour
{
    [SerializeField]
    private float degreesPerSecond = 15.0f;
    [SerializeField]
    private float amplitude = 0.5f;
    [SerializeField]
    private float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}