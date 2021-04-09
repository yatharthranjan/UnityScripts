using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Assets.Scripts.World.Events
{
    public enum EventName
    {
        PlayerDead, GunUpdated, OnClickGrenade,
        ZombieDead, AnimalDead, ZombieCured,
        ZombieCloseCall, PlayerHit, EnemyLevelUp
    }

    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private Dictionary<EventName, UnityEvent> eventDictionary;

        private void Awake()
        {
            Instance = this;
            eventDictionary = new Dictionary<EventName, UnityEvent>();

        }

        public static void RegisterListener(EventName eventName, UnityAction action)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.AddListener(action);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(action);
                Instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void UnregisterListener(EventName eventName, UnityAction listener)
        {
            if (Instance == null) return;
            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(EventName eventName)
        {
            if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}