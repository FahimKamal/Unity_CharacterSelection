using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAnimHolder", menuName = "ScriptableObjects/SOAnimHolder")]  
public class SOAnimHolder : ScriptableObject
{
    [Serializable]
    public struct Anim
    {
        public string name;
        public AnimationClip Clip;
    }

    public List<Anim> animations;

}

