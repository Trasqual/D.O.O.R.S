using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void PopText(string text, Transform parent)
    {
        transform.SetParent(parent);
        _text.SetText(text);
        _text.transform.DOLocalMove(Vector3.up * 2, 2f).OnComplete(() => Destroy(gameObject));
    }
}
