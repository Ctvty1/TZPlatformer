using System.Collections;
using UnityEngine;
public class TrainingScript : MonoBehaviour
{
    public float time;

    public SpriteRenderer sr;

    public Sprite sprite1;
    public Sprite sprite2;
    void Start()
    {
        StartCoroutine(Training());
    }
    IEnumerator Training()
    {
        while (true)
        {
            sr.sprite = sprite1;
            yield return new WaitForSeconds(time);
            sr.sprite = sprite2;
            yield return new WaitForSeconds(time);
        }
    }
}