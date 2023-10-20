using UnityEngine;

public class HealthPopUp : MonoBehaviour
{
    [SerializeField] private TextPopUp _textPopUpPrefab;
    [SerializeField] private HealthManager _healthManager;

    private void Awake()
    {
        _healthManager.OnDamageTaken += PopDamageTakenText;
    }

    private void OnDestroy()
    {
        _healthManager.OnDamageTaken -= PopDamageTakenText;
    }

    private void PopDamageTakenText(float damage)
    {
        var textPopUp = Instantiate(_textPopUpPrefab);
        textPopUp.PopText(damage.ToString(), transform);
        textPopUp.transform.localPosition = Vector3.up;
    }
}
