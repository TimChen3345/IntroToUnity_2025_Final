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
                Random.Range(-13f, 13f),
                Random.Range(0f, 0f),
                Random.Range(5f, 10f));

            objToSpawn.transform.parent = bombHolder.transform;
            bombList.Add(objToSpawn);
            bomb[i] = objToSpawn;
        }

        InvokeRepeating(nameof(SpawnCapsule), time: 0.5f, repeatRate: 0.4f);
    }

    public void SpawnCapsule()
    {
        GameObject newbomb = spawnPrefab != null
            ? Instantiate(spawnPrefab)
            : GameObject.CreatePrimitive(PrimitiveType.Capsule);

        newbomb.transform.position = new Vector3(
            Random.Range(-13f, 13f),
            Random.Range(0f, 0f),
            Random.Range(5f, 10f));

        newbomb.transform.parent = bombHolder.transform;

        Rigidbody rb = newbomb.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = newbomb.AddComponent<Rigidbody>();
        }

        // Make it fall slowly
        rb.mass = 0.8f;          // Lower mass
        rb.linearDamping = 1.2f;            // Increase air resistance
        rb.useGravity = true;    // Ensure gravity is still on

        bombList.Add(newbomb);

        Destroy(newbomb, 20f);
    }

}