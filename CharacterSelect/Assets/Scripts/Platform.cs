using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private TextMesh indexValue;

    public bool middlePos, leftPos, rightPos;
    public void SetIndexValue(int value)
    {
        indexValue.text = $"0{value}";
    }
}
