using System.Collections.Generic;
using UnityEngine;

public class StartPageSpawn : MonoBehaviour
{
    // Prefab to spawn - assign this in the Inspector
    public GameObject spawnPrefab;

    // Array of GameObjects
    public GameObject[] bomb;

    // List of GameObjects
    public List<GameObject> bombList;

    GameObject bombHolder;

    void Start()
    {
        bombHolder = new GameObject(name: "bomb Holder");

        bomb = new GameObject[3];
        bombList = new List<GameObject>();

        for (int i = 0; i < bomb.Length; i++)
        {
            GameObject objToSpawn = spawnPrefab != null
                ? Instantiate(spawnPrefab)
                : GameObject.CreatePrimitive(PrimitiveType.Capsule);

            objToSpawn.transform.position = new Vector3(
                Random.Range(-20f, 20f),
                Random.Range(20f, 20f),
                Random.Range(5.7f, 57f));

            objToSpawn.transform.parent = bombHolder.transform;
            bombList.Add(objToSpawn);
            bomb[i] = objToSpawn;
        }

        InvokeRepeating(nameof(SpawnCapsule), time: 0.1f, repeatRate: 0.1f);
    }

    public void SpawnCapsule()
    {
        GameObject newbomb = spawnPrefab != null
            ? Instantiate(spawnPrefab)
            : GameObject.CreatePrimitive(PrimitiveType.Capsule);

        newbomb.transform.position = new Vector3(
            Random.Range(-20f, 20f),
            Random.Range(20f, 20f),
            Random.Range(5.7f, 57f));

        newbomb.transform.parent = bombHolder.transform;

        if (newbomb.GetComponent<Rigidbody>() == null)
        {
            newbomb.AddComponent<Rigidbody>();
        }

        bombList.Add(newbomb);

        // Destroy the object after 10 seconds
        Destroy(newbomb, 10f);
    }
}