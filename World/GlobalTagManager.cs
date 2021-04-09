using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTagManager : MonoBehaviour
{
    public static string PlayerTag = "Player";
    public static string[] EnemyTags = { "Enemy", "Zombie" };
    public static string[] ProjectileTags = { "Bullet", "Vaccine" };
    public static string BulletTag = "Bullet"; 
    public static string VaccineTag = "Vaccine";
    public static string GunTag = "Gun"; 
    public static string GameControllerTag = "GameController";
    public static string[] BulletCollisionTags = { "Zombie", "Chicken" };
    public static string SafePlaceTag = "SafePlace";
}
