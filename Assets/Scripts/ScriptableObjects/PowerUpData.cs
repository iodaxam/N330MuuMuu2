using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Power Up")]
public class PowerUpData : ScriptableObject
{
    [Header("Function Call")] public new string functionName;
    [Header("Power")] public float amount;
}
