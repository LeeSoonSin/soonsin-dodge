﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRigidBody;// 이동에 사용할 리지드바디 컴포넌트
    public float speed = 8f; // 이동속도 

    public int hp = 100;
    public HPBar hpbar;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        playerRigidBody.velocity = newVelocity;
    }
    public void Die()
    {
        //자신의 게임 오브젝트를 비활성화
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
    public void GetDamage(int damage)
    {
        hp -= damage;
        hpbar.SetHP(hp);
        if(hp <= 0)
        {
            Die();
        }
    }
    public void GetHeal(int heal)
    {
        hp += heal;
        if(hp > 100)
        {
            hp = 100;
        }
        hpbar.SetHP(hp);
    }
}
