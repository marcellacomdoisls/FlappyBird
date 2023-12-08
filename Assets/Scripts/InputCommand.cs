using System;
using System.Collections.Generic;

public class InputCommand
{
    private readonly Dictionary<InputCommandAction, Func<bool>> actions;

    public InputCommand()
    {
        actions = new Dictionary<InputCommandAction, Func<bool>>();
        actions.Add(InputCommandAction.JUMP, null);
        actions.Add(InputCommandAction.PAUSE, null);
        actions.Add(InputCommandAction.RESUME, null);
        actions.Add(InputCommandAction.CLOSE, null);
        actions.Add(InputCommandAction.KILL, null);
    }

    public InputCommand(Dictionary<InputCommandAction, Func<bool>> actions)
    {
        this.actions = actions;
    }

    public InputCommand(Func<bool> jump, Func<bool> pause, Func<bool> resume, Func<bool> close, Func<bool> kill)
    {
        actions = new Dictionary<InputCommandAction, Func<bool>>();

        actions.Add(InputCommandAction.JUMP, jump);
        actions.Add(InputCommandAction.PAUSE, pause);
        actions.Add(InputCommandAction.RESUME, resume);
        actions.Add(InputCommandAction.CLOSE, close);
        actions.Add(InputCommandAction.KILL, kill);
    }

    public void SetAction(InputCommandAction action, Func<bool> func)
    {
        actions[action] = func;
    }

    public void ExecuteCommand(InputCommandAction action, bool condition)
    {
        if (condition)
        {
            actions[action].Invoke();
        }
    }
}

public enum InputCommandAction
{
    JUMP, PAUSE, RESUME, CLOSE, KILL
}
