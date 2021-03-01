using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public int length = 4;
    private State currentState;
    private State goal;
    public GridLayoutGroup grid;
    public GameObject numberPrefab;
    private List<Tile> tiles = new List<Tile>();

    /// <summary>
    /// TODO:
    /// * FIT GRID TO SCREEN (based on cell size and constrained count)
    /// </summary>
    private void Start()
    {
        grid.constraintCount = length;
        currentState = NewGame();
        goal = GoalState();
        BuildGame();
        UpdateState();
    }

    private void BuildGame()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Tile tile = Instantiate(numberPrefab, grid.transform).GetComponent<Tile>();
                tiles.Add(tile);
            }
        }
    }

    private void UpdateState()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                int value = currentState.tiles[i,j];
                if (value == 0) continue;

                int index = (i * length) + j;
                tiles[index].StateUpdated(value);
            }
        }
    }

    private State NewGame()
    {
        Random rand = new Random();
        bool solvable = false;
        int[,] board = new int[length, length];

        while (!solvable)
        {
            for (int i = 0; i < 5; i++)
            {
                List<int> usedTiles = new List<int>();
                for (int j = 0; j < length * length; j++)
                {
                    int random;

                    do
                    {
                        random = rand.Next(0, length * length);
                    }
                    while (usedTiles.Contains(random));

                    usedTiles.Add(random);
                    board[j % length, j / length] = random;
                }
            }

            solvable = IsSolvable(board);
        }

        return new State(board);
    }

    private bool IsSolvable(int[,] state)
    {
        int[] boardState = ToIntArray(state);
        int allInversions = Inversions(boardState);
        int blankRow = FindBlankRow(boardState);
        if (length % 2 == 1)
        {
            return allInversions % 2 == 0;
        }
        else
        {
            return (allInversions + length - blankRow + 1) % 2 == 0;
        }
    }

    private int[] ToIntArray(int[,] doubleArray)
    {
        int size = doubleArray.GetLength(0);
        int[] array = new int[size * size];
        int count = 0;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                array[count] = doubleArray[i, j];
                count++;
            }
        }
        return array;
    }

    private int Inversions(int[] boardState)
    {
        int inversions = 0;
        for (int i = 0; i < boardState.Length - 1; i++)
        {
            if (boardState[i] == 0)
                continue;

            int value = boardState[i];
            for (int j = i; j < boardState.Length; j++)
            {
                if (boardState[j] != 0 && value > boardState[j])
                {
                    inversions++;
                }
            }
        }

        return inversions;
    }

    private int FindBlankRow(int[] boardState)
    {
        for (int i = 0; i < boardState.Length; i++)
        {
            if (boardState[i] == 0)
                return i / length;
        }
        return -1;
    }

    private State GoalState()
    {
        int[,] goal = new int[length,length];
        int count = 1;
        int max = length * length;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                goal[i, j] = count % max;
                count++;
            }
        }
        return new State(goal);
    }

    public State GetCurrentState() => currentState;

    public void SetCurrentState(State state) => currentState = state;

    public State GetGoalState() => goal;
}