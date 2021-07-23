using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum PreviewType
{
    VideoPreview = 0,
    ImagePreview = 1
}

public class ArtworkItem : MonoBehaviour
{
    public ArtworkInfoScriptableObject artworkInfo;

    [SerializeField]
    public PreviewType previewType;

    [SerializeField]
    private ArtworkInfoLayout m_InfoLayout;

    [SerializeField]
    private VideoClip m_videoClip;

    public VideoClip videoClip
    {
        get => m_videoClip;
        set => m_videoClip = value;
    }

    [SerializeField]
    private RenderTexture m_renderTexture;

    public RenderTexture renderTexture
    {
        get => m_renderTexture;
        set => m_renderTexture = value;
    }
}
