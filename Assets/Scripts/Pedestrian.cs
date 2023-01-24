using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour, Checkable, Killable{
    [SerializeField] private float speed;
    [SerializeField] private int serializedID;

    private Transform targetPosition;

    public void AssignPosition(Transform pos){
        targetPosition = pos;
    }

    private void Update(){
        Vector3 dir = targetPosition.position - transform.position;
        dir = dir.normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    public int ReturnID(){
        return serializedID;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
