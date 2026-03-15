using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{
    [SerializeField] public GameObject parent;
    [SerializeField] public GameObject ennemy;

    public GameObject[] spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = GameObject.FindGameObjectsWithTag("Spawn");
        InvokeRepeating("SpawEnnemy", 0.5f, 1f);
        Invoke("StopSpawn", 60.0f);
    }
    public void SpawEnnemy()
    {
        int random = Random.Range(1,8);
        Vector3 randomizePosition = spawnPosition[random].transform.position;
        GameObject newCube = Instantiate(ennemy, randomizePosition, Quaternion.Euler(randomizePosition));
    }

    public void StopSpawn()
    {
        CancelInvoke(nameof(SpawEnnemy));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
