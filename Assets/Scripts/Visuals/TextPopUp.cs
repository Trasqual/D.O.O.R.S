using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Tween _textMovementTween;

    public void PopText(string text, Transform parent)
    {
        transform.SetParent(parent);
        _text.SetText(text);
        _textMovementTween = _text.transform.DOLocalMove(Vector3.up * 2, 2f).OnUpdate(() => _text.transform.rotation = Quaternion.LookRotation(_text.transform.position - Camera.main.transform.position)).OnComplete(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        _textMovementTween?.Kill();
    }
}
