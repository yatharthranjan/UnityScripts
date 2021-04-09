using Assets.Scripts.Animals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationUpdater : MonoBehaviour
{
    private AnimalProperties properties;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        properties = GetComponent<AnimalProperties>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(navMeshAgent.speed < 0.3f)
        {
            //Play idle animation
            return;
        }    

        if(navMeshAgent.speed <= properties.WalkSpeed)
        {
            // Play walk animation
            return;
        }

        if(navMeshAgent.speed >= properties.RunSpeed)
        {
            // play run animation
            return;
        }

        // if reached here, means speed is somewhere between walk and run, so chose animation appropriately.
    }
}
