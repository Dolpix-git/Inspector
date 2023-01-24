using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour{
    private void OnTriggerEnter(Collider other){
        Debug.Log("Attempted kill");

       
        if (other.transform.parent.TryGetComponent(out Killable k)){
            k.Kill();
        }
    }
}
