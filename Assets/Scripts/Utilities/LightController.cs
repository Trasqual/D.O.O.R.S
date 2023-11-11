using GamePlay.EventSystem;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light _light;

    private void Awake()
    {
        EventManager.Instance.AddListener<DoorSelectedEvent>(TurnLightOff);
        EventManager.Instance.AddListener<RoomSpawnAnimationFinishedEvent>(TurnLightOn);
    }

    private void TurnLightOn(object data)
    {
        ToggleLight(true);
    }

    private void TurnLightOff(object data)
    {
        ToggleLight(false);
    }

    private void ToggleLight(bool isOn)
    {
        _light.enabled = isOn;
    }
}
