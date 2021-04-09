using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySelector : EnemySelector
{
    private System.Random random = new System.Random();
    private List<GameObject> Enemies;

    public RandomEnemySelector(List<GameObject> enemies)
    {
        Enemies = enemies;
    }

    public override GameObject NextEnemy() => Enemies[random.Next(0, Enemies.Count)];

    public override List<GameObject> NextEnemies(int count)
    {
        List<GameObject> enemiesSelected = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            enemiesSelected.Add(Enemies[random.Next(Enemies.Count)]);
        }

        return enemiesSelected;
    }
}