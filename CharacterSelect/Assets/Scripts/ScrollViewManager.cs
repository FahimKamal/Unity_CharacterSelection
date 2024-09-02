using System.Collections;
using System.Collections.Generic;
using Alchemy.Inspector;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    [SerializeField] private Transform leftTransform;
    [SerializeField] private Transform middleTransform;
    [SerializeField] private Transform rightTransform;

    [SerializeField] private List<GameObject> platforms; // List to hold platform GameObjects
    [SerializeField] private float scrollSpeed = 0.5f; // Speed of the scrolling animation

    private int currentIndex = 0; // Index of the currently displayed middle platform
    private int maxIndex;

    private void Awake()
    {
        maxIndex = platforms.Count - 1;
        InitializePlatforms();
    }

    [Button]
    private void InitializePlatforms()
    {
        // Set initial positions based on currentIndex
        for (int i = 0; i < platforms.Count; i++)
        {
            if (i == currentIndex)
            {
                platforms[i].transform.position = middleTransform.position;
            }
            else if (i == currentIndex + 1 && currentIndex < maxIndex)
            {
                platforms[i].transform.position = rightTransform.position;
            }
            else
            {
                platforms[i].transform.position = leftTransform.position;
                platforms[i].SetActive(false); // Deactivate platforms off-screen
            }
        }
    }

    public void LeftButtonClicked()
    {
        if (currentIndex > 0) // Prevent going beyond the left edge
        {
            currentIndex--;
            StartCoroutine(ScrollLeft());
        }
    }

    private IEnumerator ScrollLeft()
    {
        // Move middle platform to the left
        yield return LMotion.Create(middleTransform.position, leftTransform.position, scrollSpeed)
            .BindToPosition(platforms[currentIndex].transform);

        // Move right platform to the middle
        yield return LMotion.Create(rightTransform.position, middleTransform.position, scrollSpeed)
            .BindToPosition(platforms[currentIndex + 1].transform);

        // Activate the new right platform
        if (currentIndex + 2 <= maxIndex)
        {
            platforms[currentIndex + 2].SetActive(true);
        }

        // Update platform positions for the next frame
        InitializePlatforms();
    }

    public void RightButtonClicked()
    {
        if (currentIndex < maxIndex - 1) // Prevent going beyond the right edge
        {
            currentIndex++;
            StartCoroutine(ScrollRight());
        }
    }

    private IEnumerator ScrollRight()
    {
        // Move left platform to the middle
        yield return LMotion.Create(leftTransform.position, middleTransform.position, scrollSpeed)
            .BindToPosition(platforms[currentIndex].transform);

        // Move middle platform to the right
        yield return LMotion.Create(middleTransform.position, rightTransform.position, scrollSpeed)
            .BindToPosition(platforms[currentIndex + 1].transform);

        // Deactivate the previous left platform
        if (currentIndex - 1 >= 0)
        {
            platforms[currentIndex - 1].SetActive(false);
        }

        // Update platform positions for the next frame
        InitializePlatforms();
    }
}