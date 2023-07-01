using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler
{
    public static event Action<int, int> playerInputEvent;
    public static void CallPlayerInputEvent(int playerID, int action)
    {
        playerInputEvent.Invoke(playerID, action);
    }
}
