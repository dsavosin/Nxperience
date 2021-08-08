using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class VideoOverlayManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Video Surface Mesh")]
    MeshRenderer m_VideoMesh;

    public MeshRenderer videoMesh
    {
        get => m_VideoMesh;
        set => m_VideoMesh = value;
    }

    public void EnableVideoSurface(bool enable)
    {
        m_VideoMesh.enabled = enable;
    }
}
