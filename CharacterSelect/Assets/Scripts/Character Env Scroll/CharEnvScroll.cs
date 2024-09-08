using System;
using System.Collections;
using System.Collections.Generic;
using Alchemy.Inspector;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class CharEnvScroll : MonoBehaviour
{
    [SerializeField] private List<CharItem> charItemList;
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private float scrollDistance = 30f;
    [SerializeField] private int currentIndex = 0; // Index of the currently displayed middle platform
    private int maxIndex;
    
    [SerializeField] private Ease easeType = Ease.Linear;
    
    private Coroutine scrollCoroutine;
    private bool scrolling;

    private void Awake()
    {
        maxIndex = charItemList.Count-1;
        currentIndex = Mathf.Clamp(currentIndex, 0, maxIndex);
        InitializePlatforms();
    }

    private void InitializePlatforms()
    {
        scrollCoroutine = StartCoroutine(ScrollPlatforms(-scrollDistance * currentIndex));
    }

    [Button]
    public void LeftButtonClicked()
    {
        if (currentIndex == 0) return;

        if (scrollCoroutine == null)
        {
            charItemList[currentIndex].Shrink();
            currentIndex--;
            scrollCoroutine = StartCoroutine(ScrollPlatforms(scrollDistance));
        }
    }

   
    [Button]
    public void RightButtonClicked()
    {
        if (currentIndex == maxIndex) return;
        
        if (scrollCoroutine == null)
        {
            charItemList[currentIndex].Shrink();
            currentIndex++;
            scrollCoroutine = StartCoroutine(ScrollPlatforms(-scrollDistance));
        }
    }
    
    private IEnumerator ScrollPlatforms(float scrollDistance)
    {
        // charItemList[currentIndex].Shrink();
        yield return new WaitForSeconds(scrollSpeed);
        for (var index = 0; index < charItemList.Count; index++)
        {
            var charItem = charItemList[index];

            var presentPosition = charItem.transform.position;
            var targetPosition = charItem.transform.position;
            targetPosition.x = presentPosition.x + scrollDistance;

            yield return LMotion.Create(presentPosition, targetPosition, scrollSpeed)
                .WithEase(easeType)
                .BindToPosition(charItem.transform);
            
            yield return new WaitForSeconds(scrollSpeed);
            if (index == currentIndex)
            {
                charItem.Enlarge();
            }
        }

        // yield return new WaitForSeconds(scrollSpeed);
        scrollCoroutine = null;
    }
}
