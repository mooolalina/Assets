using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Friend : MonoBehaviour
{
    public int _health; 
    [SerializeField] private TextMeshProUGUI healthText;


    void Start()
    {
        _health = Random.Range(1, 11); 
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = _health.ToString(); 
        }
        else
        {
            Debug.LogError("healthText не присоединен в инспекторе!");
        }
    }

    void Update()
    {
    }
}
