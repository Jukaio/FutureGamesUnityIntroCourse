using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControllerUI : MonoBehaviour
{
    [SerializeField]
    private HealthController healthController;

    private Text healthUIText;

    void Awake()
    {
        healthUIText = GetComponent<Text>();
    }

    void Update()
    {
        healthUIText.text = $"x{healthController.GetCurrentHealth()}";
    }
}
