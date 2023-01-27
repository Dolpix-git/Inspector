using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Checkable{
    public int ReturnID();
    public bool IsChecked();
    public void SetChecked(bool check);
    public void AssignPosition(Transform pos);
    public void ChangeState(PedestrianStates s);
}