using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    GameObject player;

    int chunkSize = 500;
    int radius = 1;

    // WORLD SIZE (5 x 4)
    int minX = 1;
    int maxX = 4;
    int minZ = 1;
    int maxZ = 5;

    HashSet<string> loadingScenes = new HashSet<string>();
    HashSet<string> loadedScenes = new HashSet<string>();

    void Start()
    {
        player = GameObject.Find("Character");
    }

    void Update()
    {
        UpdateChunks();
    }

    void UpdateChunks()
    {
        int playerChunkX = Mathf.FloorToInt(player.transform.position.x / chunkSize) + 1;
        int playerChunkZ = Mathf.FloorToInt(player.transform.position.z / chunkSize) + 1;

        Debug.Log(playerChunkX);
        Debug.Log(playerChunkZ);

        HashSet<string> requiredScenes = new HashSet<string>();

        // Determine which chunks SHOULD be loaded
        for (int x = -radius; x <= radius; x++)
        {
            for (int z = -radius; z <= radius; z++)
            {
                int chunkX = playerChunkX + x;
                int chunkZ = playerChunkZ + z;

                if (chunkX < minX || chunkX > maxX || chunkZ < minZ || chunkZ > maxZ)
                    continue;

                string sceneName = $"Scene_Terrain-{chunkX}_{chunkZ}";
                requiredScenes.Add(sceneName);

                Scene scene = SceneManager.GetSceneByName(sceneName);

                if (!scene.isLoaded && !loadingScenes.Contains(sceneName))
                {
                    loadingScenes.Add(sceneName);
                    StartCoroutine(LoadScene(sceneName));
                }
            }
        }

        // Unload scenes outside radius
        List<string> scenesToRemove = new List<string>();

        foreach (string sceneName in loadedScenes)
        {
            if (!requiredScenes.Contains(sceneName))
            {
                SceneManager.UnloadSceneAsync(sceneName);
                scenesToRemove.Add(sceneName);
            }
        }

        foreach (string scene in scenesToRemove)
        {
            loadedScenes.Remove(scene);
        }
    }

    System.Collections.IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return op;

        loadingScenes.Remove(sceneName);
        loadedScenes.Add(sceneName);
    }
}