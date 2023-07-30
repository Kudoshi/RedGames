using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_SoundRepo", menuName = "ScriptableObject/SO_SoundRepo")]
public class SoundRepositorySO : ScriptableObject
{
    public Sound[] SoundList;
}
