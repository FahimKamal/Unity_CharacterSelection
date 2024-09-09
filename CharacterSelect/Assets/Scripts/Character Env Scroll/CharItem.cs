using System.Collections;
using System.Collections.Generic;
using Alchemy.Inspector;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class CharItem : MonoBehaviour
{
    [SerializeField] private List<BuildingAnimator> BuildingAnimatorList;
    [SerializeField] private Transform characterPlatform;

    public bool isEnlarged;

    private void CharacterEntre()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 0.35f)
            .WithEase(Ease.OutBounce)
            .BindToLocalScale(characterPlatform);
    }

    private void CharacterExit()
    {
        LMotion.Create(Vector3.one, Vector3.zero, 0.35f)
            .WithEase(Ease.OutBounce)
            .BindToLocalScale(characterPlatform);
    }

    [Button]
    public void Enlarge()
    {
        isEnlarged = true;
        characterPlatform.localPosition = Vector3.zero;
        CharacterEntre();
        foreach (var buildingAnimator in BuildingAnimatorList)
        {
            buildingAnimator.EnlargeItems();
        }
    }

    [Button]
    public void Shrink()
    {
        if (!isEnlarged)
        {
            return;
        }
        characterPlatform.localPosition = Vector3.zero;
        CharacterExit();
        
        foreach (var buildingAnimator in BuildingAnimatorList)
        {
            buildingAnimator.ShrinkItems();
        }
        isEnlarged = false;
    }

}
