using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SourceInventory",menuName ="SO/SourceInventory")]
public class SO_SourceList : ScriptableObject
{
    [SerializeField]
    public List<SO_Source> Sources;
    [SerializeField]
    public List<List<SO_Transformation>> transformations;
}
