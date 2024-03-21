using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class HealthNumber : MonoBehaviour
{
    public Image healthbarImage;
    public PlayerScript player;

    float health;
    TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.Health;
        textBox.text = health.ToString();
        healthbarImage.fillAmount = health / 100;
    }
}
