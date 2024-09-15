using System.Collections;
using UnityEngine;

public class RandomAnim : MonoBehaviour
{
    [SerializeField] private SOAnimHolder animHolder;
    [SerializeField] public int animationIndex = -1; // -1 for random, otherwise index of animation to play
    [SerializeField] private bool loopAnimation = false; // Flag to loop the animation

    public SkinnedMeshRenderer innerBody;
    public SkinnedMeshRenderer outerBody;

    private Animator _animator;
    private Coroutine animationCoroutine; 

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        StartAnimation();
    }

    private void StartAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        if (animationIndex == -1)
        {
            animationCoroutine = StartCoroutine(PlayRandomAnimationsCoroutine());
        }
        else
        {
            animationCoroutine = StartCoroutine(PlaySpecificAnimationCoroutine(animationIndex));
        }
    }

    public void PlayRandomAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(PlayRandomAnimationsCoroutine());
    }

    public void PlaySpecificAnimation(int index)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(PlaySpecificAnimationCoroutine(index));
    }

    private IEnumerator PlayRandomAnimationsCoroutine()
    {
        while (true)
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            // Error handling: Check if animHolder is assigned and has animations
            if (animHolder == null || animHolder.animations.Count == 0)
            {
                Debug.LogError("Error: AnimHolder not assigned or has no animations!");
                yield break; // Stop the coroutine if there's an error
            }

            // Select a random animation index
            int randomIndex = Random.Range(0, animHolder.animations.Count);

            // Play the selected animation
            _animator.Play(animHolder.animations[randomIndex].name);

            // Wait for the animation to finish
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length); 
        }
    }

    private IEnumerator PlaySpecificAnimationCoroutine(int index)
    {
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        while (true)
        {
            // Error handling: Check if index is within valid range
            if (index < 0 || index >= animHolder.animations.Count)
            {
                Debug.LogError("Error: Invalid animation index!");
                yield break; // Stop the coroutine if there's an error
            }

            // Play the animation
            _animator.Play(animHolder.animations[index].name);

            // Wait for the animation to finish (if not looping)
            if (!loopAnimation) 
            {
                yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else
            {
                yield return null; // Wait for the next frame
            }
        }
    }
}