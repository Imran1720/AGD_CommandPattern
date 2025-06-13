using Command.Commands;
using Command.Main;
using System.Collections.Generic;
using UnityEngine;

public class ReplayService
{
    private Stack<ICommand> replayCommandStack;
    private float delay = 1f;
    private float timer;

    public ReplayState ReplayState { get; private set; }

    // Constructor for the ReplayService. Initializes the replay state as "DEACTIVE."
    public ReplayService() => SetReplayState(ReplayState.DEACTIVE);

    /// Set the replay state to the specified state.
    public void SetReplayState(ReplayState stateToSet) => ReplayState = stateToSet;

    // Set the command stack for replay, providing a collection of commands to replay.
    public void SetCommandStack(Stack<ICommand> commandsToSet) => replayCommandStack = new Stack<ICommand>(commandsToSet);

    // Execute the next recorded command in the stack if there are commands left to replay.
    public void ExecuteNext()
    {
        if (replayCommandStack.Count > 0)
            GameService.Instance.PerformAction(replayCommandStack.Pop());
    }

    public void PlayReplay()
    {
        if (ReplayState != ReplayState.ACTIVE)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ExecuteNext();
            timer = delay;
        }

    }

    public void AddCommandToReplay(ICommand command) => replayCommandStack.Push(command);
}

public enum ReplayState
{
    DEACTIVE,
    ACTIVE
}