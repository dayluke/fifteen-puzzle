using System;
using System.Collections.Generic;

public class State
{
    public int[,] tiles;

    public enum Action { Up, Down, Left, Right }

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

    public List<Action> GetValidActions(State currentState)
    {
        List<Action> available = new List<Action>();
        int[,] board = currentState.tiles;
        int boardLength = board.GetLength(0);
        bool found = false;

        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                if (board[i, j] == 0)
                {
                    if (i > 0) available.Add(Action.Up);
                    if (j > 0) available.Add(Action.Left);
                    if (j < boardLength - 1) available.Add(Action.Right);
                    if (i < boardLength - 1) available.Add(Action.Down);

                    found = true;
                    break;
                }
            }

            if (found) break;
        }
        return available;
    }
}