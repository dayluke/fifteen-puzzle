using System;

public class State
{
    private int[,] state;

    public State(int[,] state) => this.state = state;

    public State(State otherState) => this.state = otherState.GetState();

    public int[,] GetState() => this.state;

    public override bool Equals(Object obj)
    {
        if (obj is State)
        {
            State aState = (State)obj;
            for (int i = 0; i < this.state.GetLength(0); i++)
            {
                for (int j = 0; j < this.state.GetLength(0); j++)
                {
                    if (this.state[i, j] != aState.state[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }

    public State Copy() => new State(this);
}