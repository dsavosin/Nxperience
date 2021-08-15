using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationSettings : MonoBehaviour
{
    [SerializeField]
    int TargetFPS = 30;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Application.targetFrameRate = TargetFPS;
    }
}
