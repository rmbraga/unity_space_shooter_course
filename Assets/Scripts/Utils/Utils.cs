using UnityEngine;
using UnityEditor;

public class Utils
{
    public static void CheckIfGameObjectIsNull(Object obj)
    {
        if (obj == null)
        {
            Debug.LogError(obj.name + " is NULL!");
        }
    }
}