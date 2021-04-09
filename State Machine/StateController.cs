using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public abstract class StateController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public bool aiActive;

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetupAI(true);
    }

    public void Update()
    {
        if (!aiActive)
            return;
    }

    public void SetupAI(bool isAIActive)
    {
        if(navMeshAgent == null)
        {
            return;
        }

        aiActive = isAIActive;
        if (aiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }
}
