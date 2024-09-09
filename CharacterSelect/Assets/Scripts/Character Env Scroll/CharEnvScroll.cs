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
    [SerializeField] private int currentIndex = 0; 
    private int maxIndex;
    
    public CharItem currentlySelectedCharItem;
    
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
        foreach (var charItem in charItemList)
        {
            charItem.transform.position = Vector3.zero;
            charItem.gameObject.SetActive(false);
        }
        currentlySelectedCharItem = charItemList[currentIndex];
        currentlySelectedCharItem.gameObject.SetActive(true);
        currentlySelectedCharItem.Enlarge();
    }

    [Button]
    public void LeftButtonClicked()
    {
        if (currentIndex == 0) return;

        if (scrollCoroutine != null) return;
        
        currentIndex--;
        scrollCoroutine = StartCoroutine(ScrollItems(currentIndex));
    }

   
    [Button]
    public void RightButtonClicked()
    {
        if (currentIndex == maxIndex) return;

        if (scrollCoroutine != null) return;
        
        currentIndex++;
        scrollCoroutine = StartCoroutine(ScrollItems(currentIndex));
    }

    private IEnumerator ScrollItems(int index)
    {
        currentlySelectedCharItem.Shrink();
        yield return new WaitForSeconds(scrollSpeed);
        currentlySelectedCharItem.gameObject.SetActive(false);
        currentlySelectedCharItem = charItemList[index];
        currentlySelectedCharItem.gameObject.SetActive(true);
        currentlySelectedCharItem.Enlarge();
        yield return new WaitForSeconds(scrollSpeed);
        scrollCoroutine = null;
    }
    
    private IEnumerator ScrollPlatforms(float scrollDistance)
    {
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
        scrollCoroutine = null;
    }
}
