using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public static class PersistantClass
{ 
    public static GameObject artworkARPrefab { get; set; }
    public static string testString { get; set; }
}

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

    [SerializeField]
    Button m_closeButton;

    private Image m_Image;
    private RawImage m_RawImage;
    private VideoPlayer m_VideoPlayer;
    private ArtworkItem m_artworkItem;
    private RectTransform m_rectTransform;

    private Vector2 resetSizeDelta;
    private Vector3 resetLocalPosition;

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
            m_Image = m_ArtworkView.AddComponent<Image>();
            if (m_Image == null)
            {
                Debug.LogAssertion("Image component not found.", gameObject);
                return;
            }
            else
            {
                m_Image.sprite = artworkItem.artworkPreview;
                m_Image.type = Image.Type.Filled;
                m_Image.preserveAspect = true;
                m_Image.fillMethod = Image.FillMethod.Horizontal;

                float heightToWidthRatio = m_Image.sprite.rect.height / m_Image.sprite.rect.width;
                if ( heightToWidthRatio >= 1.0f )
                {
                    m_rectTransform.sizeDelta = new Vector2(m_rectTransform.sizeDelta.x, m_rectTransform.sizeDelta.y * heightToWidthRatio);
                }
                else
                {
                    float yOffset = m_rectTransform.sizeDelta.y - (m_rectTransform.sizeDelta.y * heightToWidthRatio);
                    m_rectTransform.localPosition = new Vector3(m_rectTransform.localPosition.x, m_rectTransform.localPosition.y - yOffset, m_rectTransform.localPosition.z);
                }
            }
        }

        m_artworkItem = artworkItem;
        m_ArtworkName.text = artworkItem.artworkInfo.ArtworkName;
        m_ArtworkSize.text = artworkItem.artworkInfo.ArtworkSize;
        m_ArtworkDescription.text = artworkItem.artworkInfo.ArtworkDescription;

        m_closeButton.GetComponent<Image>().color = artworkItem.backButtonColor;
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

        m_rectTransform.sizeDelta = resetSizeDelta;
        m_rectTransform.localPosition = resetLocalPosition;

        Destroy(m_RawImage);
        Destroy(m_VideoPlayer);
        Destroy(m_Image);
    }


    public void StartAR()
    {
        PersistantClass.testString = "Loaded Artwork in AR";
        PersistantClass.artworkARPrefab = m_artworkItem.placedARPrefab;
        SceneManager.LoadScene("UXManagerScene");
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

        m_rectTransform = m_ArtworkView.GetComponent<RectTransform>();
        if (m_rectTransform == null)
        {
            Debug.LogAssertion("RectTransform component not found.", gameObject);
        }
        else
        {
            resetSizeDelta = m_rectTransform.sizeDelta;
            resetLocalPosition = m_rectTransform.localPosition;
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
    }
}
