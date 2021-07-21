using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtworkInfoLayout : MonoBehaviour
{

    public void showHidePanelDynamically(GameObject yourObject)
    {
        var getCanvasGroup = yourObject.GetComponent<CanvasGroup>();
        if (getCanvasGroup.alpha == 0)
        {
            getCanvasGroup.alpha = 1;
            getCanvasGroup.interactable = true;
            getCanvasGroup.blocksRaycasts = true;

        }
        else
        {
            getCanvasGroup.alpha = 0;
            getCanvasGroup.interactable = false;
            getCanvasGroup.blocksRaycasts = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
