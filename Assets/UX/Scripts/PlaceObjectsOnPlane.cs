using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    [SerializeField]
    GameObject m_PreviewPrefab;

    public GameObject previewPrefab
    {
        get { return m_PreviewPrefab; }
        set { m_PreviewPrefab = value; }
    }


    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }


    public GameObject spawnedPreview { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    [SerializeField]
    bool m_CanReposition = true;

    public bool canReposition
    {
        get => m_CanReposition;
        set => m_CanReposition = value;
    }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        placedPrefab = PersistantClass.artworkARPrefab;
        Debug.Log(placedPrefab.name);
    }

    private void Start()
    {
        Debug.Log(PersistantClass.testString);
    }

    void Update()
    {
        if (spawnedObject == null && m_RaycastManager.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = s_Hits[0].pose;

            if ( spawnedPreview == null )
            {
                spawnedPreview = Instantiate(m_PreviewPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedPreview.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && spawnedPreview != null)
            {
                if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, spawnedPreview.transform.position, spawnedPreview.transform.rotation);
                    m_NumberOfPlacedObjects++;
                    spawnedPreview.SetActive(false);

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }

                if (m_CanReposition && m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;
                    spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
