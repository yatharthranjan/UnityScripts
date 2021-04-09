using Assets.Scripts.World;
using Assets.Scripts.World.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.RegisterListener(EventName.PlayerDead, new UnityAction(ShowDeadView));
    }

    private void OnDestroy()
    {
        EventManager.UnregisterListener(EventName.PlayerDead, new UnityAction(ShowDeadView));
    }

    private void ShowDeadView()
    {
        // TODO: Can maybe move camera around like a cinematic view
        transform.SetParent(GameObject.FindGameObjectWithTag(GlobalTagManager.GameControllerTag).transform);
        gameObject.SetActive(true);
    }
}
