using System.Collections;
using System.Collections.Generic;
using Alchemy.Inspector;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    [SerializeField] private TestSpawner testSpawner;
    [SerializeField] private float scrollSpeed = 0.5f; // Speed of the scrolling animation

    [SerializeField] private float scrollDistance = 5f;
    [SerializeField] private int currentIndex = 0; // Index of the currently displayed middle platform
    [SerializeField] private Ease easeType = Ease.Linear;
    private int maxIndex;
    
    public List<GameObject> spawnedObjects = new List<GameObject>();

    private Coroutine _scrollCoroutine;
    private bool _scrolling;
    private void Awake()
    {
        spawnedObjects = testSpawner.SpawnModels();
        maxIndex = spawnedObjects.Count-1;
        currentIndex = Mathf.Clamp(currentIndex, 0, maxIndex);
        InitializePlatforms();
    }

    [Button]
    private void InitializePlatforms()
    {
        if (_scrollCoroutine == null)
        {
            _scrollCoroutine = StartCoroutine(ScrollPlatforms(scrollDistance * currentIndex));
        }
        
    }

    public void LeftButtonClicked()
    {
        if (currentIndex == 0) return;

        if (_scrollCoroutine == null)
        {
            currentIndex--;
            _scrollCoroutine = StartCoroutine(ScrollPlatforms(-scrollDistance));
        }
    }
    
    public void RightButtonClicked()
    {
        if (currentIndex == maxIndex) return;
        
        if (_scrollCoroutine == null)
        {
            currentIndex++;
            _scrollCoroutine = StartCoroutine(ScrollPlatforms(scrollDistance));
        }
        
    }

    /// <summary>
    /// Call the function to scroll the platforms left or right.
    /// Left  : Give negative value. 
    /// Right : Give positive value.
    /// </summary>
    /// <param name="scrollDistance"></param>
    private IEnumerator ScrollPlatforms(float scrollDistance)
    {
        for (var i = 0; i < spawnedObjects.Count; i++)
        {
            var platform = spawnedObjects[i].GetComponent<Platform>();
            var presentPlatformPos = platform.transform.position;
            var targetPlatformPos = presentPlatformPos;
            targetPlatformPos.x = presentPlatformPos.x + scrollDistance;

            yield return LMotion.Create(presentPlatformPos, targetPlatformPos, scrollSpeed)
                .WithEase(easeType)
                .BindToPosition(platform.transform);

            if (i == currentIndex)
            {
                platform.Enlarge();
            }
            else
            {
                platform.Small();
            }
        }

        yield return new WaitForSeconds(scrollSpeed);
        _scrollCoroutine = null;
    }
    
} // Class end