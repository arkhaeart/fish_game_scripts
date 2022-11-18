using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : Zone
{
    public static System.Action OnPlayerWon;
    public enum Type
    { 
        START,
        END
    }
    public Type type;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (type == Type.END)
                OnPlayerWon?.Invoke();
            else
                Fail();

        }
    }
}
