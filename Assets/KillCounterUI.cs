using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounterUI : MonoBehaviour
{
    [SerializeField]
    private int maxKilledUnits;

    [SerializeField]
    private int counter;
    private Image icon;


    public void CountUp()
    {
        counter = counter + 1;
    }

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    private void Update()
    {
        float fraction = (float)counter / maxKilledUnits;
        icon.fillAmount = fraction;
    }
}
