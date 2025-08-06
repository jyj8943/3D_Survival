using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    
    Condition health
    {
        get { return uiCondition.health; }
    }

    Condition hunger
    {
        get { return uiCondition.hunger; }
    }

    Condition stamina
    {
        get { return uiCondition.stamina; }
    }

    public float noHungerHealthDecay;

    public event Action onTakeDamage;
    
    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    
    public void Die()
    {
        Debug.Log("죽었다!");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}
