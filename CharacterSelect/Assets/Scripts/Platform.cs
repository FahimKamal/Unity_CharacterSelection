using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private TextMesh indexValue;
    [SerializeField] private TextMesh danceMoveName;
    
    public bool IsSelected { get; set; }
    public void SetIndexValue(int value, string danceMove)
    {
        indexValue.text = $"0{value}";
        danceMoveName.text = danceMove.ToString();
    }

    public void Enlarge()
    {
        LMotion.Create(Vector3.one, new Vector3(1.3f, 1.3f, 1.3f), 0.2f)
            .BindToLocalScale(transform);
        IsSelected = true;
    }

    public void Small()
    {
        if (!IsSelected)
        {
            return;
        }
        LMotion.Create(new Vector3(1.3f, 1.3f, 1.3f),Vector3.one, 0.2f)
            .BindToLocalScale(transform);
        IsSelected = false;
    }
}
