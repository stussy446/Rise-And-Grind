using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Booster : MonoBehaviour
{
    Player_Controller playerController;
    Rigidbody2D _playerRigidBody;
    Object_BoostValueHolder boostValue;
    Vector2 boostVector = new Vector2(0, 1); 
    Player_Controller _playerController;
    SoundManager soundManager;

    int beansLayerMask = 8;
    int goldenBeanLayerMask = 14;
    [SerializeField] float secondsToWait = 1f;

    public UnityEvent GoldenBeanTouched;



    void Start()
    {
        if (GoldenBeanTouched == null)
        {
            GoldenBeanTouched = new UnityEvent();
        }

        playerController = GetComponent<Player_Controller>();
        _playerRigidBody = GetComponentInParent<Rigidbody2D>();
        _playerController = GetComponentInParent<Player_Controller>();
        StartCoroutine(DelaySingleton());

    }



IEnumerator DelaySingleton()
{
    yield return new WaitForSeconds(.25f);
    soundManager = FindObjectOfType<SoundManager>();
}


/// <summary>
/// if the player collides with a trigger object other than an enemy give player a vertical boost based
/// on that beans boostspeed bonus
/// </summary>
/// <param name="collision"></param>
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == beansLayerMask)
        {
            soundManager.Play("BeanBoost");
            boostValue = collision.gameObject.GetComponent<Object_BoostValueHolder>();
            ProcessBoost(boostValue);
        }
        else if (collision.gameObject.layer == goldenBeanLayerMask)
        {
            GoldenBeanTouched?.Invoke();
        }
        else
        {
            return;
        }

    }

    /// <summary>
    /// if the player collides with an object other than an enemy give player a vertical boost based
    /// on that beans boostspeed bonus
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == beansLayerMask)
        {
            soundManager.Play("BeanBoost");
            boostValue = collision.gameObject.GetComponent<Object_BoostValueHolder>();
            ProcessBoost(boostValue);
        }
        else if(collision.gameObject.layer == goldenBeanLayerMask)
        {
            GoldenBeanTouched?.Invoke();
        }
        else
        {
            return;
        }
    }


    /// <summary>
    /// takes in a boost value to boost player by and boosts the player based
    /// on that value 
    /// </summary>
    /// <param name="collisonBoost"></param>
    private void ProcessBoost(Object_BoostValueHolder collisonBoost)
    {
        if (collisonBoost != null)
        {
            playerController.BeanTouched = true;
            _playerRigidBody.velocity = boostVector * collisonBoost.GetBoostSpeed();
            StartCoroutine(EndBeanTouch());

        }

    }

    IEnumerator EndBeanTouch()
    {
        yield return new WaitForSeconds(secondsToWait);
        playerController.BeanTouched = false;
    }
    
}
