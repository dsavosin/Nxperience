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
    [Tooltip("Instantiates this prefab on a plane at the raycast location.")]
    GameObject m_PreviewPrefab;

    /// <summary>
    /// The prefab to instantiate when successful raycast agains plane.
    /// </summary>
    public GameObject previewPrefab
    {
        get { return m_PreviewPrefab; }
        set { m_PreviewPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /// <summary>
    /// The preview-object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedPreview { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;
    ARPlaneManager m_PlaneManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    [SerializeField]
    bool m_CanReposition = true;

    private Camera m_ARCamera;
    
    public bool canReposition
    {
        get => m_CanReposition;
        set => m_CanReposition = value;
    }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        placedPrefab = PersistantClass.artworkARPrefab;
        m_ARCamera = Camera.main;
        Debug.Log(placedPrefab.name);
    }

    private void Start()
    {
        Debug.Log(PersistantClass.testString);
    }

    void Update()
    {
        if (spawnedObject == null)
        {
            m_RaycastManager.Raycast(m_ARCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), s_Hits, TrackableType.PlaneWithinPolygon);
            if (s_Hits.Count == 0)
            {
                return;
            }

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
        
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began && spawnedPreview != null)
        {
            if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
            {
                m_RaycastManager.Raycast(
                    m_ARCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), s_Hits,
                    TrackableType.PlaneWithinPolygon);
                Pose hitPose = s_Hits[0].pose;
                ARPlane plane = m_PlaneManager.GetPlane(s_Hits[0].trackableId);

                if (plane.alignment.IsHorizontal())
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    spawnedObject.GetComponent<RotateTowardsCamera>().ApplyFrameRootOffset();
                    spawnedObject.GetComponent<RotateTowardsCamera>().SetShadowPlaneEnabled(true);
                    spawnedObject.GetComponent<RotateTowardsCamera>().allowLookAt = true;
                }

                if (plane.alignment.IsVertical())
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);

                    RotateTowardsCamera spawnedArtworkComp = spawnedObject.GetComponent<RotateTowardsCamera>();
                    if( spawnedArtworkComp )
                    {
                        spawnedArtworkComp.SetShadowPlaneEnabled(false);
                        spawnedArtworkComp.allowLookAt = false;
                    }

                    Vector3 targetTransform = Camera.main.transform.position - spawnedObject.transform.position;
                    Vector3 Euler = Quaternion.LookRotation(targetTransform, Camera.main.transform.up).eulerAngles;
                    Euler.x = 0.0f;
                    Euler.z = 0.0f;
                    spawnedObject.transform.rotation = Quaternion.Euler(Euler);
                }

                m_NumberOfPlacedObjects++;
                spawnedPreview.SetActive(false);

                if (onPlacedObject != null)
                {
                    onPlacedObject();
                }
            }
        }
    }
}
