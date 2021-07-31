using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ArtworkInfoScriptableObject", order = 1)]
public class ArtworkInfoScriptableObject : ScriptableObject
{
    public string ArtworkName;
    public string ArtworkSize;
    public string ArtworkDescription;
}
