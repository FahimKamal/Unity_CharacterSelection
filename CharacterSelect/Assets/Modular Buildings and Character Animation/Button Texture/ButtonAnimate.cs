using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimate : MonoBehaviour
{
    [SerializeField] private RawImage buttonBG;

    [SerializeField] private float animationSpeed = 0.2f;
    private Vector2 imageUV;
    private void Update()
    {
        imageUV.x += animationSpeed * Time.deltaTime;
        imageUV.y += animationSpeed * Time.deltaTime;
        buttonBG.uvRect = new Rect(imageUV.x, imageUV.y, 1f, 1f);
    }
}
