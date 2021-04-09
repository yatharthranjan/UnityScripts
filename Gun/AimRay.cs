using Assets.Scripts.Gun;
using Assets.Scripts.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(GunProperties))]
public class AimRay : MonoBehaviour
{
    LineRenderer line;
    public Material lineMaterial;
    public Color originalColor = Color.green;
    public Color onEnemyColor = Color.red;
    public string[] EnemyTags = GlobalTagManager.EnemyTags;
    public GameObject Player;
    private PlayerProperties playerProperties;

    public float Distance;

    private void OnEnable()
    {
        Distance = gameObject.GetComponent<GunProperties>().Range;
        if (!Player) Player = GlobalObjectManager.Instance.Player;
        playerProperties = Player.GetComponent<PlayerProperties>();

        if (Distance == 0) Distance = 5;
        
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.GetComponent<Renderer>().material = lineMaterial;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.startColor = originalColor;
        line.endColor = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerProperties.mode != Mode.Idle)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            //Debug.DrawRay(transform.position, fwd * Distance, Color.red);
            if (Physics.Raycast(transform.position, fwd, out RaycastHit raycastHit, Distance))
            {
                handleRayCastHit(raycastHit);
            }
            else
            {
                ChangeLineColor(originalColor);
                DisplayLine(transform.position + fwd * Distance);
            }
        }
        else
        {
            Debug.Log("Line disabled coz of idle mode");
            line.enabled = false;
        }
    }

    private void handleRayCastHit(RaycastHit raycastHit)
    {
        //Debug.Log(raycastHit.collider.gameObject.tag);
        DisplayLine(raycastHit);
        //do something if hit Zombie
        if (EnemyTags.Contains(raycastHit.collider.gameObject.tag))
        {
            //Debug.Log("Close to enemy");
            ChangeLineColor(onEnemyColor);
            if (GetComponent<GunProperties>().ShootMode == ShootMode.AutoShoot)
            {
                GetComponent<GunProperties>().Shoot();
            }
        }
        else
        {
            ChangeLineColor(originalColor);
        }
    }

    void DisplayLine(RaycastHit hit)
    {
        line.enabled = true;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hit.point);//+ hit.normal);
        
    }

    void DisplayLine(Vector3 target)
    {
        line.enabled = true;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target);
    }

    void ChangeLineColor(Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }
}