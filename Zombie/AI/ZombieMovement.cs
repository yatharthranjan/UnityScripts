using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public int Speed;
    public float Randomness;
    public string PlayerTag = GlobalTagManager.PlayerTag;
    public float RandomnessThreshold;
    private System.Random random = new System.Random();
    private float MovementThreashold = 0.5f;
    private ZombieProperties zombieProperties;

    // Start is called before the first frame update
    void Start()
    {
        zombieProperties = gameObject.GetComponent<ZombieProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        if(zombieProperties.CuredPercentage >= 100)
        {
            // Don't follow the player if the zombie is already cured
            return;
        }

        navigateToPlayer();
    }

    private void navigateToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);
        if(player == null)
        {
            // Player either dead or not available.
            return;
        }
        float step = Speed * Time.deltaTime;
        Vector3 targetDirection = pseudoRandomMovementPattern(transform.position, player.transform.position);
        if (targetDirection == transform.position)
        {
            // No movement needed
            return;
        }
        float singleStep = Speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    Vector3 pseudoRandomMovementPattern(Vector3 currentPosition, Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - currentPosition;
        targetDirection.y = targetDirection.y * 0.2f;
/*        if (targetDirection.x < MovementThreashold || targetDirection.z < MovementThreashold)
        {
            return currentPosition;
        }
*/
        if (targetDirection.x > RandomnessThreshold && targetDirection.y > RandomnessThreshold)
        {
            int next = random.Next(1, 4);
            switch (next)
            {
                case 1:
                    targetDirection = targetDirection + new Vector3(Randomness, 0, 0);
                    break;
                case 2:
                    targetDirection = targetDirection + new Vector3(0, 0, Randomness);
                    break;
                case 3:
                    targetDirection = targetDirection + new Vector3(Randomness, 0, Randomness);
                    break;
                case 4:
                    break;
            }
        }
        return targetDirection;
    }
}
