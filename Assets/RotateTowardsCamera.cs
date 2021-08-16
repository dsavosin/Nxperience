using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    void Update()
    {
        Vector3 targetTransform = Camera.main.transform.position - transform.position;
        Vector3 Euler = Quaternion.LookRotation(targetTransform,  Camera.main.transform.up).eulerAngles;
        Euler.x = 0.0f;
        Euler.z = 0.0f;
        transform.rotation = Quaternion.Euler(Euler);
    }
}
