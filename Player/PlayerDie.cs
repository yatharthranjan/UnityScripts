using UnityEngine;
using System.Collections;
using Assets.Scripts.World.Events;

[RequireComponent(typeof(PlayerProperties))]
public class PlayerDie : MonoBehaviour
{
    private PlayerProperties playerProperties;

    // Use this for initialization
    void Start()
    {
        playerProperties = gameObject.GetComponent<PlayerProperties>();
    }

    private void OnEnable()
    {
        EventManager.RegisterListener(EventName.PlayerDead, new UnityEngine.Events.UnityAction(UpdateToDead));
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener(EventName.PlayerDead, new UnityEngine.Events.UnityAction(UpdateToDead));
    }

    // Update is called once per frame
    void UpdateToDead()
    {
        if(playerProperties.Health <= 0)
        {
            // TODO: play dead animation
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
