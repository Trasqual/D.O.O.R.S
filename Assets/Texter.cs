using GamePlay.Utilities;
using TMPro;
using UnityEngine;

public class Texter : Singleton<Texter>
{
    [SerializeField] private TMP_Text _textPrefab;

    public void CreateText(string textInfo, Vector3 position)
    {
        var text = Instantiate(_textPrefab);
        text.transform.position = position;
        text.SetText(textInfo);
    }
}
