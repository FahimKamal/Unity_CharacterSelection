using System;
using UnityEngine;

public class UniversalSwipeDetector : MonoBehaviour
{
    private Vector2 startTouch;
    private float swipeThreshold = 50f;

    private ScrollViewManager _scrollViewManager;

    private void Awake()
    {
        _scrollViewManager = GetComponent<ScrollViewManager>();
    }

    void Update()
    {
        // Check for touch input (mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0]; // Get the first touch
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Vector2 endTouch = touch.position;
                HandleSwipe(startTouch, endTouch);
            }
        }

        // Check for mouse input (editor)
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 endTouch = Input.mousePosition;
            HandleSwipe(startTouch, endTouch);
        }
    }

    private void HandleSwipe(Vector2 startTouch, Vector2 endTouch)
    {
        float deltaX = endTouch.x - startTouch.x;
        float deltaY = endTouch.y - startTouch.y;

        if (Mathf.Abs(deltaX) > swipeThreshold || Mathf.Abs(deltaY) > swipeThreshold)
        {
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) // Horizontal swipe
            {
                if (deltaX > 0)
                {
                    Debug.Log("Swiped right!");
                    _scrollViewManager.LeftButtonClicked();
                }
                else
                {
                    Debug.Log("Swiped left!");
                    _scrollViewManager.RightButtonClicked();
                }
            }
            else // Vertical swipe
            {
                if (deltaY > 0)
                {
                    Debug.Log("Swiped up!");
                }
                else
                {
                    Debug.Log("Swiped down!");
                }
            }
        }
    }
}