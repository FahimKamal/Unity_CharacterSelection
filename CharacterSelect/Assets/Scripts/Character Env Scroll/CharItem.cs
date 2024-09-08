using System.Collections;
using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEngine;

public class CharItem : MonoBehaviour
{
    [SerializeField] private List<BuildingAnimator> BuildingAnimatorList;

    [Button]
    public void Enlarge()
    {
        foreach (var buildingAnimator in BuildingAnimatorList)
        {
            buildingAnimator.EnlargeItems();
        }
    }

    [Button]
    public void Shrink()
    {
        foreach (var buildingAnimator in BuildingAnimatorList)
        {
            buildingAnimator.ShrinkItems();
        }
    }

}
