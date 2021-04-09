using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.World.Events;

[RequireComponent(typeof(PlayerProperties))]
public class UpdateStatBars : MonoBehaviour
{
    public Slider HealthBar;
    public Slider Armour;
    private PlayerProperties playerProperties;

    private void OnEnable()
    {
        playerProperties = gameObject.GetComponent<PlayerProperties>();
        UpdateStatBar();
        EventManager.RegisterListener(EventName.PlayerHit, new UnityEngine.Events.UnityAction(UpdateStatBar));
    }

    void UpdateStatBar()
    {
        HealthBar.value = playerProperties.Health / playerProperties.MaxHealth * 100f;
        Armour.value = playerProperties.Armour / playerProperties.MaxArmour * 100f;
    }
}
