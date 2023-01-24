using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Checker : MonoBehaviour{
    [SerializeField] Filter defaultFilter;
    [SerializeField] Filter[] filter;
    private void OnTriggerEnter(Collider other){
        Debug.Log("Beep, Somthing has passed me");
        if (other.transform.parent.TryGetComponent(out Checkable check)) {
            foreach (Filter item in filter){
                if (item.ID == check.ReturnID()){
                    check.AssignPosition(item.position);
                    return;
                }
            }

            check.AssignPosition(defaultFilter.position);
        }
    }
}

[Serializable]
public class Filter{
    public int ID;
    public Transform position;
}
