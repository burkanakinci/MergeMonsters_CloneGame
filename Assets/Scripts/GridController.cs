using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public bool isEmpty;
    public List<SoldierController> soldiersOnGrid=new List<SoldierController>();
    
    private void OnEnable()
    {
        isEmpty = true;
    }
}
