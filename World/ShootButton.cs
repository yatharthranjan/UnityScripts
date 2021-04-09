using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Assets.Scripts.Gun;
using Assets.Scripts.World;
using Assets.Scripts.World.Events;
using System.Collections;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GunProperties gunProperties;
    Animator CameraAnim; // Camera Shake on Shoot
    
    // Start is called before the first frame update
    private void Start()
    {
        
        CameraAnim = GlobalObjectManager.Instance.MainCamera.gameObject.GetComponent<Animator>();
        EventManager.RegisterListener(EventName.GunUpdated, new UnityAction(OnGunUpdate));
        gunProperties = GlobalObjectManager.Instance.CurrentGun.GunController.GetComponent<GunProperties>();
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            gunProperties.Shoot();
            CameraAnim.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void  Shoot()
    {
        gunProperties.Shoot();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(nameof(AutoShoot));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(nameof(AutoShoot));
        CameraAnim.enabled = false;

    }

    public void OnGunUpdate()
    {
        gunProperties = GlobalObjectManager.Instance.CurrentGun.GunController.GetComponent<GunProperties>();
    }
}
