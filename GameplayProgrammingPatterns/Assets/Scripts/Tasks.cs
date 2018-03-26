using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// 
/// General Purpose Tasks
///

//simple action task
public class ActionTask : Task
{

    public Action Action { get; private set; }

    public ActionTask(Action action)
    {
        Action = action;
    }

    protected override void Init()
    {
        Action();
        SetStatus(TaskStatus.Success);
    }

}

//base class for tasks that track time
public abstract class TimedTask : Task
{
    public float Duration
    {
        get; private set;
    }
    public float StartTime
    {
        get; private set;
    }

    protected TimedTask(float duration)
    {
        Debug.Assert(duration > 0, "Cannot create a timed task with duration less than 0");
        Duration = duration;
    }

    protected override void Init()
    {
        StartTime = Time.time;
    }

    internal override void Update()
    {
        var now = Time.time;
        var elapsed = now - StartTime;
        var t = Mathf.Clamp01(elapsed / Duration);
        if (t >= 1)
        {
            OnElapsed();
        }
        else
        {
            OnTick(t);
        }
    }

    //t is the normalized time for the task, meaning if half the task's duration has elapsed then t == 0.5
    //this is where subclasses will do most of their work
    protected virtual void OnTick(float t)
    {

    }

    //default to being successful if we get to the end of the duration
    protected virtual void OnElapsed()
    {
        SetStatus(TaskStatus.Success);
    }

}

//very simple wait task
public class Wait : TimedTask
{
    public Wait(float duration) : base(duration)
    {

    }
}


public class WaitTask : Task
{
    // Get the timestamp in floating point milliseconds from the Unix epoch   

    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

    private static double GetTimestamp()
    {
        return (DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
    }

    private readonly double _duration;
    private double _startTime;

    public WaitTask(double duration)
    {
        this._duration = duration;
    }

    protected override void Init()
    {
        _startTime = GetTimestamp();
    }

    internal override void Update()
    {
        var now = GetTimestamp();
        var durationElapsed = (now - _startTime) > _duration;

        if (durationElapsed)
        {
            SetStatus(TaskStatus.Success);
        }
    }
}

/// 
/// Game Object Tasks
///

//base classes for tasks that operate on game objects
//since c# doesn't allow for multiple inheritance we'll make two versions, one timed and one untimed

public abstract class GOTask : Task
{
    protected readonly GameObject gameObject;

    protected GOTask(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}

public abstract class TimedGOTask : TimedTask
{
    protected readonly GameObject gameObject;

    protected TimedGOTask(GameObject gameObject, float duration) : base(duration)
    {
        this.gameObject = gameObject;
    }
}

//task for teleporting a gameobject
public class SetPos : GOTask
{
    private readonly Vector3 _pos;

    public SetPos(GameObject gameObject, Vector3 pos) : base(gameObject)
    {
        _pos = pos;
    }

    protected override void Init()
    {
        gameObject.transform.position = _pos;
        SetStatus(TaskStatus.Success);
    }
}

//task for lerping a gameobject's position
public class Move : TimedGOTask
{
    public Vector3 Start
    {
        get;
        private set;
    }
    public Vector3 End
    {
        get;
        private set;
    }

    public Move(GameObject gameObject, Vector3 start, Vector3 end, float duration) : base(gameObject, duration)
    {
        Start = start;
        End = end;
    }

    protected override void OnTick(float t)
    {
        gameObject.transform.position = Vector3.Lerp(Start, End, t);
    }
}

//task for lerping a gameobject's scale
public class Scale : TimedGOTask
{
    public Vector3 Start
    {
        get;
        private set;
    }
    public Vector3 End
    {
        get;
        private set;
    }

    public Scale(GameObject gameObject, Vector3 start, Vector3 end, float duration) : base(gameObject, duration)
    {
        Start = start;
        End = end;
    }

    protected override void OnTick(float t)
    {
        gameObject.transform.localScale = Vector3.Lerp(Start, End, t);
    }
}

public class ChangePhase : Task
{
    public readonly BossEnemyCow boss;
    public readonly Phase phase;
    public ChangePhase(BossEnemyCow _boss, Phase _phase)
    {
        boss = _boss;
        phase = _phase;
    }

	protected override void Init()
	{
        Debug.Log("Changing phase to: " + phase);
        boss.currentPhase = phase;
        Debug.Log("Phase now = " + boss.currentPhase);

	}
}