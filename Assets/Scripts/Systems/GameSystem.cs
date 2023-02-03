using System;
using System.Collections;
using UnityEngine;

public class SystemException : Exception
{
    public SystemException() { }
    public SystemException(string message) : base(message) { }
    public SystemException(string message, Exception inner) : base(message, inner) { }
}

[System.Serializable]
public abstract class GameSystem : Singleton<GameSystem>
{
    /*Game System can be any system that handles a defined part of the game;
     the buy of items, the battle system, the resurrection, the leveling system and enchancting system, etc.

    You can Run, Pause, and Stop a system;
     */

    //This system will be very handy with turing itself on and off.
    public string SystemName => name;

    public static bool IsRunning { get; private set; }

    delegate void OnCheckStatus();
    public delegate void OnInitCallback();

    OnCheckStatus onCheckStatusCallback;
    public OnInitCallback onInitCallback;

    public PlayerEntity Player { get; private set; }

    SystemStatus status;
    bool isInitialized;

    public override Action OnAwake => WakeUp;

    void WakeUp()
    {
        onCheckStatusCallback = CheckStatus;
    }

    void Init()
    {
        if (isInitialized) return;

        Player = GameManager.Player;

        status = GameManager.GetSystemStatus(this);
        isInitialized = status != null;
        OnInit();
    }

    protected virtual void OnInit() { onInitCallback?.Invoke(); }

    void CheckStatus()
    {
        switch (status.systemStatus)
        {
            case Status.STOPPED: Stop(); break;
            case Status.PAUSED: Pause(); break;
            case Status.RUNNING: Run(); break;
            default: break;
        }
    }

    /// <summary>
    /// All the main functionalities of a system will be put in Main()
    /// </summary>
    protected virtual void Main()
    {
        //This is were our main code is going to go.
    }

    internal void Run()
    {
        Init();
        if (status.systemStatus == Status.RUNNING) return;
        status.OnStatusChange(Status.RUNNING);
        IsRunning = true;
        StartCoroutine(SystemRoutine());
    }

    private void Pause()
    {
        if (!isInitialized) return;
        if (status.systemStatus == Status.PAUSED) return;
        status.OnStatusChange(Status.PAUSED);
        IsRunning = false;
        StopCoroutine(SystemRoutine());
    }

    private void Stop()
    {
        if (!isInitialized) return;
        if (status.systemStatus == Status.STOPPED) return;
        status.OnStatusChange(Status.STOPPED);
        IsRunning = false;
        StopCoroutine(SystemRoutine());
    }

    private void Stop(Exception _exception)
    {
        Stop();
        Debug.LogException(_exception);
    }

    /// <summary>
    /// The main function of our system. We can pause the process temporarily, stop it
    /// (either meaning that there was something wrong, or that it's done being used completely)
    /// or running it.
    /// </summary>
    /// <returns>null value.</returns>
    protected virtual IEnumerator SystemRoutine()
    {
        //Main Loop
        while (true)
        {
            try
            {
                //Try calling main every fram
                Main();
            }
            catch (Exception e)
            {
                //We'll stop the process, and throw an exception.
                Stop(e);
            }
            yield return null;
        }
    }
}
