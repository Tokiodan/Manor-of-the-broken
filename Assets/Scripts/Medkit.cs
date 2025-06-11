using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private float recoverAmount = 25f; // default value for testing
    public float GetRecoverAmount() => recoverAmount;   // public getter
}