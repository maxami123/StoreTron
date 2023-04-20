using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public Image cooldownBar;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = cooldownBar.GetComponent<RectTransform>();
        currentWidth = 0;
    }

    public float maxWidth = 100;
    public Movement movement;
    private float currentWidth;
    private float timerPercent;
    // Update is called once per frame
    void Update()
    {   
        if (movement.canDash)
            currentWidth = 0;
        else
        {
            timerPercent = movement.dashCooldownTimer / movement.maxCooldown;
            currentWidth = timerPercent * maxWidth;
        }
        rectTransform.sizeDelta = new Vector2(currentWidth, rectTransform.sizeDelta.y);   
    }


}
