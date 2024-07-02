using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MobAttack
{
    private PlayerController _controller;
    [SerializeField] private PlayerStatus _Status;

    public void ComboReset()
    {
        _Status._combo = 1;
    }
}
