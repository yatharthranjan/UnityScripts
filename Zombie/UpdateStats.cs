using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(ZombieProperties))]
public class UpdateStats : MonoBehaviour
{
    public Slider HealthBar;
    public Slider CurePercentage;
    private ZombieProperties zombieProperties;

    // Use this for initialization
    void Start()
    {
        zombieProperties = gameObject.GetComponent<ZombieProperties>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(UpdateStatBar));
    }

    IEnumerator UpdateStatBar()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            HealthBar.value = zombieProperties.Health / zombieProperties.MaxHealth * 100f;
            CurePercentage.value = zombieProperties.CuredPercentage;
        }
    }
}
