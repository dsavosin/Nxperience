using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ArtworkInfoLayout : MonoBehaviour
{
    [SerializeField]
    Text m_ArtworkName;

    [SerializeField]
    Text m_ArtworkSize;

    [SerializeField]
    Text m_ArtworkDescription;

    [SerializeField]
    GameObject m_ArtworkView;

    private RawImage m_RawImage;
    private VideoPlayer m_VideoPlayer;

    public void showHidePanelDynamically(ArtworkItem artworkItem)
    {
        CanvasGroup getCanvasGroup = GetComponent<CanvasGroup>();
        if ( getCanvasGroup != null && getCanvasGroup.alpha == 0 )
        {
            getCanvasGroup.alpha = 1;
            getCanvasGroup.interactable = true;
            getCanvasGroup.blocksRaycasts = true;
        }

        if( artworkItem.previewType == PreviewType.VideoPreview )
        {
            m_RawImage = m_ArtworkView.AddComponent<RawImage>();
            if ( m_RawImage == null )
            {
                Debug.LogAssertion("RawImage component not found.", gameObject);
                return;
            }
            else
            {
                m_RawImage.texture = artworkItem.renderTexture;
            }
        
            m_VideoPlayer = m_ArtworkView.AddComponent<VideoPlayer>();
            if( m_VideoPlayer == null )
            {
                Debug.LogAssertion("VideoPlayer component not created.", gameObject);
                return;
            }
            else
            {
                m_VideoPlayer.clip = artworkItem.videoClip;
                m_VideoPlayer.playOnAwake = true;
                m_VideoPlayer.isLooping = true;
                m_VideoPlayer.renderMode = VideoRenderMode.RenderTexture;
                m_VideoPlayer.targetTexture = artworkItem.renderTexture;
                m_VideoPlayer.aspectRatio = VideoAspectRatio.FitHorizontally;
            }
        }

        if (artworkItem.previewType == PreviewType.ImagePreview)
        {

        }

        m_ArtworkName.text = artworkItem.artworkInfo.ArtworkName;
        m_ArtworkSize.text = artworkItem.artworkInfo.ArtworkSize;
        m_ArtworkDescription.text = artworkItem.artworkInfo.ArtworkDescription;
    }

    public void HidePanel()
    {
        CanvasGroup getCanvasGroup = GetComponent<CanvasGroup>();
        if ( getCanvasGroup && getCanvasGroup.alpha == 1)
        {
            getCanvasGroup.alpha = 0;
            getCanvasGroup.interactable = false;
            getCanvasGroup.blocksRaycasts = false;

            m_ArtworkName.text = "Name: ";
            m_ArtworkSize.text = "Size: ";
            m_ArtworkDescription.text = "Description: ";
        }

        Destroy(m_RawImage);
        Destroy(m_VideoPlayer);
    }

    private void OnValidate()
    {
        Debug.Assert(m_ArtworkView != null, "ArtworkView is not valid gameObject");
        Debug.Assert(m_ArtworkName != null, "ArtworkName is not valid gameObject");
        Debug.Assert(m_ArtworkSize != null, "ArtworkName is not valid gameObject");
        Debug.Assert(m_ArtworkDescription != null, "ArtworkName is not valid gameObject");
    }

    private void Awake()
    {
        CanvasGroup getCanvasGroup = GetComponent<CanvasGroup>();
        if( getCanvasGroup == null)
        {
            Debug.LogAssertion("CanvasGroup component not found.", gameObject);
        }
        else
        {
            getCanvasGroup.alpha = 0;
        }
    }
}
