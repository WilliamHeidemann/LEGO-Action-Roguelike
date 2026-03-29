using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "Game Config", order = 0)]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float _longPressTimeInSeconds = 1f;
    public float LongPressTimeInSeconds => _longPressTimeInSeconds;
}