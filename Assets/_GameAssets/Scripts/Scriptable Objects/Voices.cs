using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Voices")]
public class Voices : ScriptableObject
{
    public List<AudioClip> clips;
    public MinMax<float> timeAfterClip;
}