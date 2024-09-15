using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera flyCamera;
    [SerializeField] private CinemachineVirtualCamera characterSelectCamera;

    [SerializeField]
    private Volume mainMenuVolume;
    
    [SerializeField] private Canvas mainMenuCanvas;
    
    [SerializeField] private float waitTime = 0.5f;

    private void Start()
    {
        StartCoroutine(ShowMainMenuUI());
    }

    private IEnumerator ShowMainMenuUI()
    {
        yield return new WaitForSeconds(waitTime);
        mainMenuVolume.weight = 1f;
        mainMenuCanvas.gameObject.SetActive(true);
        yield return null;
    }

    public void ShowSelectCharacterUI()
    {
        characterSelectCamera.gameObject.SetActive(true);
        mainMenuVolume.weight = 0f;
    }

    public void HideSelectCharacterUI()
    {
        characterSelectCamera.gameObject.SetActive(false);
        mainMenuVolume.weight = 1f;
    }
    
    
    
    
}
