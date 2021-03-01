using System;

public class State
{
    public int[,] tiles;

    public enum Action
    {
        None, Up, Down, Left, Right
    }

    public State(int[,] state) => this.tiles = state;

    public State(State otherState) => this.tiles = otherState.tiles;

    public override bool Equals(Object obj)
    {
        if (obj is State)
        {
            State aState = (State)obj;
            for (int i = 0; i < this.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < this.tiles.GetLength(0); j++)
                {
                    if (this.tiles[i, j] != aState.tiles[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }

    public bool IsSolved(State state) => this.tiles.Equals(state);

    public override int GetHashCode() => ToString().GetHashCode();

    public State Copy() => new State(this);
}