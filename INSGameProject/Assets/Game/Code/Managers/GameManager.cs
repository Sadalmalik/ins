using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    public override void Init()
    {
        // must be empty
    }
    
    public void Start()
    {
        BaseManager.InitAll();
    }
}
