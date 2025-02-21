using UnityEngine;

[CreateAssetMenu(fileName = "NewUser", menuName = "Scriptable Objects/User")]
public class User : ScriptableObject
{
    public string pName;
    public int health;
}
