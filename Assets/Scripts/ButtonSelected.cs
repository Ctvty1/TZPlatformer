using UnityEngine;
using UnityEngine.UI;
public class ButtonSelected : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().Select();
    }
}