using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private TextMesh indexValue;
    [SerializeField] private TextMesh danceMoveName;
    
    [SerializeField] private Vector3 enlargeSize = new Vector3(1.3f, 1.3f, 1.3f);
    
    public bool IsSelected { get; set; }
    public void SetDanceName(int value, string danceMove)
    {
        indexValue.text = $"0{value}";
        danceMoveName.text = danceMove.ToString();
        MakeTextsHidden();
    }

    private void MakeTextsVisible()
    {
        indexValue.gameObject.SetActive(true);
        danceMoveName.gameObject.SetActive(true);
    }

    private void MakeTextsHidden()
    {
        indexValue.gameObject.SetActive(false);
        danceMoveName.gameObject.SetActive(false);
    }

    public void Enlarge()
    {
        LMotion.Create(Vector3.one, enlargeSize, 0.2f)
            .BindToLocalScale(transform);
        MakeTextsVisible();
        IsSelected = true;
    }

    public void Small()
    {
        if (!IsSelected)
        {
            return;
        }
        LMotion.Create(enlargeSize, Vector3.one, 0.2f)
            .BindToLocalScale(transform);
        MakeTextsHidden();
        IsSelected = false;
    }
}
