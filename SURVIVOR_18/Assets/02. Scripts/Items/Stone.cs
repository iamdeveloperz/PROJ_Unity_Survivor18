using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IHitable
{
    private int _hp = 3;

    public void Hit(float hit)
    {
        Debug.Log("Stone Hit");
        --_hp;
        if(_hp == 0)
            Destroy(gameObject, 1.0f);
    }
}
