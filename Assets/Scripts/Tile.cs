using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Text number = null;

    public void StateUpdated(int newNumber)
    {
        number.text = newNumber.ToString();
    }
}