using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
}
