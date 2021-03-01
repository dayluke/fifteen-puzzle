using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int length = 4;
    private State currentState;
    private State goal;
    public bool gameOn { get; set; }
    public GridLayoutGroup grid;
    public GameObject numberPrefab;

    /// <summary>
    /// TODO:
    /// * FIT GRID TO SCREEN (based on cell size and constrained count)
    /// </summary>
    private void Start()
    {
        grid.constraintCount = length;
        currentState = GoalState();
        goal = GoalState();
        BuildGame();
        gameOn = false;
    }

    private void BuildGame()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (i == length - 1 && j == length - 1) continue;

                int value = (i * length) + j + 1;
                
                GameObject number = Instantiate(numberPrefab, grid.transform);
                number.GetComponentInChildren<Text>().text = value.ToString();
            }
        }
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

    public bool IsGoalState() => goal.Equals(currentState);

    public State GetCurrentState() => currentState;

    public void SetCurrentState(State state) => currentState = state;

    public State GetGoalState() => goal;
}