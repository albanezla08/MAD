using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void enter();
    public void execute();
    public void exit();
}

public class StateMachine
{
    IState current_state;

    public void change_state(IState new_state) {
        if (current_state != null) {
            current_state.exit();
        }
        current_state = new_state;
        current_state.enter();
    }

    public void update() {
        if (current_state != null) {
            current_state.execute();
        }
    }
}
