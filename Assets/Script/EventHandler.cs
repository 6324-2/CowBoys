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

    public static event Action startWaitingTimeEvent;
    public static void CallStartWaitingTime()
    {
        startWaitingTimeEvent.Invoke();
    }

    public static event Action startActingTimeEvent;
    public static void CallStartActingTime()
    {
        startActingTimeEvent.Invoke();
    }

    public static event Action<int> actEvent;
    public static void CallActEvent(int action)
    {
        actEvent.Invoke(action);
    }

    public static event Action gameStartEvent;
    public static void CallGameStart()
    {
        gameStartEvent.Invoke();
    }
}
