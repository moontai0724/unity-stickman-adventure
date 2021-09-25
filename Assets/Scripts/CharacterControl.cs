using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
  Rigidbody2D character;
  Animator animator;
  GameObject[] enemies;
  bool standing = true;

  void Start()
  {
    character = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.name == "Platform" && Math.Abs(animator.GetInteger("status")) > 1)
    {
      standing = true;
      animator.SetInteger("status", 0);
    }
    Debug.Log(collision.collider.name);
    if (collision.collider.tag == "Enemy")
    {
      Debug.Log("Die.");
      SceneController.reloadScene();
    }
    if (collision.collider.name == "Final")
      SceneController.nextScene();
  }

  void Update()
  {
    if (Input.GetButton("Horizontal"))
      move();
    else if (Math.Abs(animator.GetInteger("status")) < 2)
      animator.SetInteger("status", 0);

    if (Input.GetButtonDown("Jump"))
      jump();

    if (Input.GetMouseButtonDown(0))
      attack();
  }

  void move()
  {
    float moveSpeed = 10f;

    float horizontalInput = Input.GetAxisRaw("Horizontal");

    character.transform.Translate(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    if (Math.Abs(animator.GetInteger("status")) < 2)
      animator.SetInteger("status", (int)horizontalInput);
  }

  void jump()
  {
    float jumpSpeed = 750f;
    if (standing)
    {
      standing = false;
      character.AddForce(Vector2.up * jumpSpeed);
    }
    if (Math.Abs(animator.GetInteger("status")) == 1)
      animator.SetInteger("status", animator.GetInteger("status") * 2);
    else
      animator.SetInteger("status", 2);
  }

  void attack()
  {
    animator.SetTrigger("attack");
    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies)
      if (Math.Abs(enemy.transform.position.y - transform.position.y) < 1)
        DestroyImmediate(enemy, false);
  }
}
