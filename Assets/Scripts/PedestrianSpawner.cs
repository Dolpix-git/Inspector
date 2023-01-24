using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour{
    [SerializeField] Transform targetPos;
    [SerializeField] GameObject[] pedestrianPrefab;
    [SerializeField] float spawnSpeed;
    float spawnTime;

    private void Start(){
        spawnTime = Time.realtimeSinceStartup;
    }

    private void Update(){
        if (Time.realtimeSinceStartup - spawnTime >= spawnSpeed){
            spawnTime = Time.realtimeSinceStartup;

            GameObject g = Instantiate(pedestrianPrefab[Random.Range(0,pedestrianPrefab.Length)], transform.position, Quaternion.identity);
            g.GetComponent<Checkable>().AssignPosition(targetPos);
        }
    }
}
