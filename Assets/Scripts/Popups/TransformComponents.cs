using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Very basic ScriptableObject that stores all three components of a transform.
/// Pretty much just makes it so that I don't have to make three seperate
/// fields to store the reference to the popup's transform before being
/// brought into focus.
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TransformComponents", order = 1)]
public class TransformComponents : ScriptableObject
{
    [SerializeField]
    public Vector3 Pos;
    [SerializeField]
    public Quaternion Rot;
    [SerializeField]
    public Vector3 Scale;

    public void SetComponents(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Pos = pos;
        Rot = rot;
        Scale = scale;
    }
}
