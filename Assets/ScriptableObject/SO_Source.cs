using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Source",menuName = "SO/Source")]
public class SO_Source : ScriptableObject
{
    public Sprite m_SourceSprite;
    public string m_name;
    public int m_index;
    public string m_introduction;
}
