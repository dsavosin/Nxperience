using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    [SerializeField]
    public float m_FrameRootOffset = 1.0f;

    [SerializeField]
    bool m_AllowLookAt;

    public bool allowLookAt
    {
        get => m_AllowLookAt;
        set => m_AllowLookAt = value;
    }

    [SerializeField]
    GameObject ShadowPlane;

    [SerializeField]
    GameObject FrameRoot;

    public void SetShadowPlaneEnabled(bool enabled)
    {
        ShadowPlane.SetActive(enabled);
    }

    public void ApplyFrameRootOffset()
    {
        FrameRoot.transform.localPosition = new Vector3(0.0f, m_FrameRootOffset, 0.0f);
    }

    void Update()
    {
        if(m_AllowLookAt)
        {
            Vector3 targetTransform = Camera.main.transform.position - transform.position;
            Vector3 Euler = Quaternion.LookRotation(targetTransform, Camera.main.transform.up).eulerAngles;
            Euler.x = 0.0f;
            Euler.z = 0.0f;
            transform.rotation = Quaternion.Euler(Euler);
        }
    }
}
