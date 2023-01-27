using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour, Checkable, Killable{
    [SerializeField] private float speed;
    [SerializeField] private int serializedID;

    private Transform targetPosition;
    private Vector3 targetDir;
    private bool isChecked = false;

    private PedestrianStates state = PedestrianStates.direction;

    private void Start(){
        targetDir = transform.forward;
    }
    private void Update(){
        if (state == PedestrianStates.direction){
            WalkDirection();
        }else if (state == PedestrianStates.transform){
            WalkToTransform();
        }
    }

    private void WalkToTransform(){
        if (targetPosition == null){
            Debug.Log("Something might have gone wrong!");
            state = PedestrianStates.direction;
            return;
        }
        Vector3 dir = targetPosition.position - transform.position;
        dir = dir.normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
    private void WalkDirection(){
        transform.position += targetDir.normalized * speed * Time.deltaTime;
    }


    public void ChangeDir(Vector3 dir){
        targetDir= dir;
    }

    public void AssignPosition(Transform pos){
        Debug.Log(pos.name);
        targetPosition = pos;
    }

    public int ReturnID(){
        return serializedID;
    }

    public void Kill(){
        Destroy(gameObject);
    }

    public void ChangeState(PedestrianStates s){
        state = s;
    }

    public bool IsChecked(){
        return isChecked;
    }

    public void SetChecked(bool check){
        isChecked = check;
    }
}

public enum PedestrianStates{
    waiting,
    transform,
    direction,
    looking
}
