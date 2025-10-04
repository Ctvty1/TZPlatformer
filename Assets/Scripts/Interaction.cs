using UnityEngine;
public class Interaction : MonoBehaviour
{
    public Chest chest;
    void Update()
    {
        if (chest != null)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                chest.Open();
                chest = null;
            }
        }
    }
}