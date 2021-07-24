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

    [SerializeField]
    public Color backButtonColor;

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

    [SerializeField]
    private Sprite m_sprite;

    public Sprite artworkPreview
    {
        get => m_sprite;
        set => m_sprite = value;
    }
}
