using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhiteCubeSpawn: MonoBehaviour
{
    public string prefabResourceName = "WhiteCube";  // Name of the prefab in Resources
    public GameObject spawnPrefab;

    public GameObject[] bomb;
    public List<GameObject> bombList;

    GameObject bombHolder;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Load prefab from Resources if needed
        if (spawnPrefab == null)
        {
            spawnPrefab = Resources.Load<GameObject>(prefabResourceName);
            if (spawnPrefab == null)
            {
                Debug.LogError($"Prefab '{prefabResourceName}' not found in Resources!");
                return;
            }
        }

        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            Destroy(gameObject);
            return;
        }

        bombHolder = new GameObject("bomb Holder");
        DontDestroyOnLoad(bombHolder);

        bomb = new GameObject[3];
        bombList = new List<GameObject>();

        for (int i = 0; i < bomb.Length; i++)
        {
            GameObject objToSpawn = Instantiate(spawnPrefab);

            objToSpawn.transform.position = new Vector3(
                Random.Range(-13f, 13f),
                0f,
                Random.Range(5f, 10f));

            objToSpawn.transform.parent = bombHolder.transform;
            bombList.Add(objToSpawn);
            bomb[i] = objToSpawn;
        }

        InvokeRepeating(nameof(SpawnCube), 0.5f, 0.4f);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayScene")
        {
            CancelInvoke(nameof(SpawnCube));
            if (bombHolder != null) Destroy(bombHolder);
            Destroy(gameObject);
        }
        else
        {
            // Reload the prefab if it was cleared (e.g., after returning to a scene)
            if (spawnPrefab == null)
            {
                spawnPrefab = Resources.Load<GameObject>(prefabResourceName);
            }
        }
    }

    public void SpawnCube()
    {
        GameObject newbomb = Instantiate(spawnPrefab);

        newbomb.transform.position = new Vector3(
            Random.Range(-13f, 13f),
            0f,
            Random.Range(5f, 10f));

        newbomb.transform.parent = bombHolder.transform;

        Rigidbody rb = newbomb.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = newbomb.AddComponent<Rigidbody>();
        }

        rb.mass = 0.8f;
        rb.linearDamping = 1.2f;
        rb.useGravity = true;

        bombList.Add(newbomb);
        Destroy(newbomb, 20f);
    }
}
