using System;
using System.Collections;
using System.Collections.Generic;
using Alchemy.Inspector;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class BuildingAnimator : MonoBehaviour
{
    [Serializable]
    public class ScaleHolder
    {
        public Transform scaleTransform;
        public Vector3 initialScale;
    }
    
    [SerializeField] private Ease easeType = Ease.Linear;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float animationSpeed;
    
    [SerializeField] private List<ScaleHolder> buildingParts = new List<ScaleHolder>();

    private Coroutine enlargeBuildingCoroutine;
    private void Awake()
    {
        GetAllChildren();
        SetSizeZero();
    }

    [Button]
    public void EnlargeItems()
    {
        if (enlargeBuildingCoroutine != null)
        {
            enlargeBuildingCoroutine = null;
        }

        enlargeBuildingCoroutine = StartCoroutine(EnlargeBuildingItems(animationSpeed, easeType));
    }

    [Button]
    public void ShrinkItems()
    {
        if (enlargeBuildingCoroutine != null)
        {
            Debug.Log("Enlarge Building Items not complete.");
            return;
        }

        Debug.Log("Shrink Building Items starting.");
        enlargeBuildingCoroutine = StartCoroutine(ShrinkBuildingItems(animationSpeed, easeType));
    }

    private IEnumerator ShrinkBuildingItems(float animationSpeed, Ease easeType)
    {
        for (var index = buildingParts.Count-1; index > -1; index--)
        {
            var buildingPart = buildingParts[index];
            if (buildingPart.scaleTransform.GetComponent<BuildingAnimator>())
            {
                yield return StartCoroutine(buildingPart.scaleTransform.GetComponent<BuildingAnimator>()
                    .ShrinkBuildingItems(animationSpeed, easeType));
            }
            else
            {
                yield return LMotion.Create(buildingPart.initialScale, Vector3.zero, animationSpeed)
                    .WithEase(easeType)
                    .BindToLocalScale(buildingPart.scaleTransform);
            }
        }

        enlargeBuildingCoroutine = null;
        yield return null;
    }


    private IEnumerator EnlargeBuildingItems(float animationSpeed, Ease easeType)
    {
        foreach (var buildingPart in buildingParts)
        {
            if (buildingPart.scaleTransform.GetComponent<BuildingAnimator>())
            {
                yield return StartCoroutine(buildingPart.scaleTransform.GetComponent<BuildingAnimator>().EnlargeBuildingItems(animationSpeed, easeType));
            }
            else
            {
                yield return LMotion.Create(Vector3.zero, buildingPart.initialScale, animationSpeed)
                    .WithEase(easeType)
                    .BindToLocalScale(buildingPart.scaleTransform);
            }
        }

        enlargeBuildingCoroutine = null;
        yield return null;
    }

    private void GetAllChildren()
    {
        var buildingParts = GetComponentInChildren<Transform>();

        foreach (Transform buildingPart in buildingParts)
        {
            var buildingScale = new ScaleHolder
            {
                scaleTransform = buildingPart,
                initialScale = buildingPart.localScale
            };
            this.buildingParts.Add(buildingScale);
        }
    }

    // [Button]
    private void SetSizeZero()
    {
        foreach (var buildingPart in buildingParts)
        {
            if (buildingPart.scaleTransform.GetComponent<BuildingAnimator>())
            {
                buildingPart.scaleTransform.GetComponent<BuildingAnimator>().SetSizeZero();
            }
            else
            {
                buildingPart.scaleTransform.localScale = Vector3.zero;
            }
        }
    }
}
