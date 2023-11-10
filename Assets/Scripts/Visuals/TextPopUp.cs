using DG.Tweening;
using Lean.Pool;
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
        _textMovementTween = _text.transform.DOLocalMove(Vector3.up * 2, 1f).OnUpdate(() => _text.transform.rotation = Quaternion.LookRotation(_text.transform.position - Camera.main.transform.position)).OnComplete(() => ResetText());
    }

    private void ResetText()
    {
        _text.transform.localPosition = Vector3.zero;
        _textMovementTween?.Kill();
        LeanPool.Despawn(this);
    }
}
