using System;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float doubleJumpSpeed = 3f;
    public bool isGround = false;
    public int maxJumpCount = 1;
}