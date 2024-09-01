using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platForm;
    [SerializeField] private RandomAnim characterModel;
    [SerializeField] private SOAnimHolder animHolder;

    [SerializeField] private List<Material> innerBodyMats;
    [SerializeField] private List<Material> outerBodyMats;

    private void Start()
    {
        SpawnModels();
    }

    private void SpawnModels()
    {
        var platformSpawnPosition = transform.position;
        platformSpawnPosition.x += 5.0f;
        
        for(var i = 0; i < animHolder.animations.Count; i++)
        {
            platformSpawnPosition.x -= 5.0f;
            var plat = Instantiate(platForm, platformSpawnPosition, quaternion.identity);

            var characterSpawnPosition = platformSpawnPosition;
            characterSpawnPosition.y += 0.2f;
            
            RandomAnim character = Instantiate(characterModel, characterSpawnPosition, quaternion.identity, plat.transform);
            
            character.innerBody.SetMaterials(new List<Material> { innerBodyMats[i] });
            character.outerBody.SetMaterials(new List<Material> { outerBodyMats[i] });
            
            character.PlaySpecificAnimation(i);
        }
    }
}
