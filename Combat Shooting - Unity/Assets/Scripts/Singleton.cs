using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject singletonGO = new GameObject(typeof(T).Name);
                    instance = singletonGO.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}