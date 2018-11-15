using UnityEngine;
using UnityEngine.UI;

public class HealthDrawerScript : MonoBehaviour
{
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    public GameObject[] HeartObjects;
    private Image[] Hearts;

    public void Draw(int lives)
    {
        for(int i = 0; i < lives; i++)
        {
            Hearts[i].sprite = FullHeart;
        }

        for(int i = lives; i < Hearts.Length; i++)
        {
            Hearts[i].sprite = EmptyHeart;
        }
    }

    // Use this for initialization
    void OnEnable ()
    {
        Hearts = new Image[HeartObjects.Length];

        for(int i = 0; i < 5; i++)
        {
            Hearts[i] = HeartObjects[i].GetComponent<Image>();
        }

        Draw(Hearts.Length);
    }
}
