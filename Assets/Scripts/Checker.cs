using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Checker : MonoBehaviour{
    [SerializeField] private Filter[] filter;
    [SerializeField] private float waitTime = 1;
    [SerializeField] private float lookRange, speed;
    [SerializeField] private CheckerPriority priority;

    [SerializeField] private Animator animator;

    private PedestrianStates state = PedestrianStates.looking;
    private Transform targetTransform;

    private void Update(){
        if (state == PedestrianStates.transform) {
            WalkToTransform();
        }
        else if(state == PedestrianStates.looking){
            LookingForPedestrian();
        }
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Beep, Somthing has passed me");
        if (state == PedestrianStates.waiting){
            return;
        }

        if (other.TryGetComponent(out Checkable check)) {
            state = PedestrianStates.waiting;
            StartCoroutine(CheckCheckable(check));
        }
    }

    private void WalkToTransform(){
        if (targetTransform == null){
            state = PedestrianStates.looking;
            return;
        }
        Vector3 dir = targetTransform.position - transform.position;
        dir = dir.normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir),5f * Time.deltaTime);
    }

    private void LookingForPedestrian(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, lookRange);

        Collider nearestCollider = null;
        Checkable checker = null;
        float minSqrDistance = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++){
            float sqrDistanceToCenter = Mathf.Infinity;
            if (priority == CheckerPriority.local){
                sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
            }
            else if(priority == CheckerPriority.global){
                sqrDistanceToCenter = (transform.position + new Vector3(0,0,lookRange) - colliders[i].transform.position).sqrMagnitude;
            }
            

            if (colliders[i].TryGetComponent(out Checkable check)){
                if (sqrDistanceToCenter < minSqrDistance && !check.IsChecked()){
                    minSqrDistance = sqrDistanceToCenter;
                    nearestCollider = colliders[i];
                    checker = check;
                }
            }
        }

        if (nearestCollider != null){
            Debug.Log("Found someone!");
            checker.SetChecked(true);
            state = PedestrianStates.transform;
            targetTransform = nearestCollider.transform;
        }
    }

    IEnumerator CheckCheckable(Checkable c){
        Debug.Log("Check!");
        c.ChangeState(PedestrianStates.waiting);
        animator.SetBool("IsChecking", true);

        yield return new WaitForSeconds(waitTime);

        
        bool toggle = true;
        foreach (Filter item in filter){
            if (item.ID == c.ReturnID()){
                c.AssignPosition(item.position);
                toggle = false;
                c.ChangeState(PedestrianStates.transform);
            }
        }
        if (toggle){
            c.ChangeState(PedestrianStates.direction);
        }

        animator.SetBool("IsChecking", false);
        state = PedestrianStates.looking;
    }
}

[Serializable]
public class Filter{
    public int ID;
    public Transform position;
}

public enum CheckerPriority{
    local,
    global
}