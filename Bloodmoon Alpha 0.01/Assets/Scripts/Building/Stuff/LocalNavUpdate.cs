using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class LocalNavUpdate : MonoBehaviour
{
    public NavMeshData navMeshData;
    public NavMeshBuildSettings buildSettings;
    public List<NavMeshBuildSource> sourse = new List<NavMeshBuildSource>();
    public float worldSize;
    private Bounds bounds;
    public LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(worldSize, 1000, worldSize));
        buildSettings = GetComponent<NavMeshSurface>().GetBuildSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            NavMeshBuilder.CollectSources(bounds, mask, NavMeshCollectGeometry.RenderMeshes, 0, new List<NavMeshBuildMarkup>(), sourse);
            Debug.Log(sourse.Count);
        }
        if (sourse != null)
        {
            NavMeshUpdate();
        }
    }
    private void NavMeshUpdate()
    {
        NavMeshBuilder.UpdateNavMeshData(navMeshData, buildSettings, sourse, bounds);
    }
}
