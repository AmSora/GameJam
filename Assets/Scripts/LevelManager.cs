using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    private void Awake()
    {
        main = this;
    }
}
