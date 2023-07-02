using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eternal : Singleton<Eternal>
{
    public float time = 20.0f;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
}
