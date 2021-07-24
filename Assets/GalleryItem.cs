using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryItem : MonoBehaviour
{
    [SerializeField]
    private string m_WebsiteURL;

    public void OpenGalleryWebsite()
    {
        Application.OpenURL(m_WebsiteURL);
    }
}
