using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Source", menuName = "SO/Transformation")]

public class SO_Transformation : ScriptableObject
{
    [SerializeField]
    public List<SO_Source> sources;
    [SerializeField]
    public int EnvironmentID;
}
