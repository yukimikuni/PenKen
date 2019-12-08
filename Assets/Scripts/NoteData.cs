
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NoteData : ScriptableObject
{
    public float timing;
    public int lineNum;
    public int type;
    public int comboType;
    public int comboMax;
}
