using UnityEngine;

public class SaveMeeee : MonoBehaviour
{
    public void Save(ref PlayerData Data)
    {
        Data.Location = transform.position;
        Data.Rotation = transform.rotation;
        Debug.Log("Player saved:" + Data.Location);
    }
    public void Load(PlayerData Data)
    {
        transform.position = Data.Location;
        transform.rotation = Data.Rotation;
    }
}
[System.Serializable]
public struct PlayerData
{
    public Vector3 Location;
    public Quaternion Rotation;
}
