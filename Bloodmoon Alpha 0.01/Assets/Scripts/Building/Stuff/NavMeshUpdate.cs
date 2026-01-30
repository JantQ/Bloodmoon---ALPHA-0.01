using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

public class NavMeshUpdate : MonoBehaviour
{
    public NavMeshSurface prefabNavmesh;
    public int mapx;
    public int mapz;
    public int chunksize;
    NavMeshSurface[] surface;
    private void Start()
    {
        for (int x = -mapx; x <= mapx; x += chunksize)
        {
            for (int z = -mapz; z <= mapz; z += chunksize)
            {
                Instantiate(prefabNavmesh, new Vector3(x, 0, z), new Quaternion(), transform);
            }
        }
        surface = GetComponentsInChildren<NavMeshSurface>();
        for (int i = 0; i < surface.Length; i++)
        {
            surface[i].BuildNavMesh();
        }
    }
    public void NavMeshupdate(GameObject build)
    {
        for (int i = 0; i < surface.Length; i++)
        {
            Debug.Log(surface[i].transform.position);
        }
    }
}
