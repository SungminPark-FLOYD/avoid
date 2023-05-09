
using Mono.Cecil.Cil;
using System.Net;
using UnityEngine;
using System.Collections;
using System;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private GameObject[] imageHP;
    private int currentHP;

    [SerializeField]
    private float invincibilityDuration;    //무적 지속 시간
    private bool isInvincibility = false;   //무적 여부

    private SoundController soundController;
    private SpriteRenderer spriteRenderer;

    private Color originColor;

    private void Awake()
    {
        soundController = GetComponentInChildren<SoundController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHP = imageHP.Length;
        originColor = spriteRenderer.color;
    }

    public bool TakeDamage()
    {
        //무적 상태일 때는 체력이 감소하지 않는다
        if (isInvincibility == true) return false;

        if(currentHP > 1)
        {
            soundController.Play(0);
            StartCoroutine(nameof(OnInvinibility));

            currentHP--;
            imageHP[currentHP].SetActive(false);
        }
        else
        {
            return true;
        }
        return false;
    }

    private IEnumerator OnInvinibility()
    {
        isInvincibility = true;

        float current = 0;
        float percent = 0;
        float colorSpeed = 10;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / invincibilityDuration;

            spriteRenderer.color = Color.Lerp(originColor, Color.red, Mathf.PingPong(Time.time * colorSpeed, 1));
            
            yield return null;
        }

        spriteRenderer.color = originColor;
        isInvincibility = false; 
    }

    public void RecoveryHP()
    {
        if(currentHP < imageHP.Length)
        {
            soundController.Play(1);
            imageHP[currentHP].SetActive(true);
            currentHP++;
        }
    }
}
