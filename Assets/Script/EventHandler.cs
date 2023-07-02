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

    public static event Action<int, int> actEvent;
    public static void CallActEvent(int action, int playerID)
    {
        actEvent.Invoke(action, playerID);
    }

    public static event Action gameStartEvent;
    public static void CallGameStart()
    {
        gameStartEvent.Invoke();
    }

    public static event Action mangaBeginEvent;
    public static void CallMangaBeginEvent()
    {
        mangaBeginEvent.Invoke();
    }

    public static event Action playerJoinedEvent;
    public static void CallPlayerJoined()
    {
        playerJoinedEvent.Invoke();
    }

    public static event Action gameEndEvent;
    public static void CallGameEndEvent()
    {
        gameEndEvent.Invoke();
    }

    public static event Action<float> manMoveEvent;
    public static void CallManMoveEvent(float time)
    {
        manMoveEvent.Invoke(time);
    }
}
