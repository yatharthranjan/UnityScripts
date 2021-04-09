using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySelector
{ 
    public abstract GameObject NextEnemy();

    public abstract List<GameObject> NextEnemies(int count);

}