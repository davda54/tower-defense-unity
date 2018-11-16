using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildMenuItemScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    public Sprite BaseSprite;
    public Sprite HoverSprite;
    public Sprite ClickSprite;
    public GameObject Prototype;

    private Text text;
    private TurretScript turret;
    private BuildLocationScript parent;
    private Image image;
    private bool pressed = false;
    private bool disabled = false;

    void Update()
    {
        if(disabled && GameManager.Instance.GetMoney() >= turret.Cost)
        {
            disabled = false;
            text.color = new Color(0, 0, 0, 1);
        }
        else if(!disabled && GameManager.Instance.GetMoney() < turret.Cost)
        {
            disabled = true;
            text.color = new Color(0, 0, 0, 0.25f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (disabled || pressed) return;

        gameObject.transform.Translate(0, -3f, 0);
        image.sprite = ClickSprite;
        pressed = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (disabled) return;

        image.sprite = HoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (disabled || !pressed) return;

        gameObject.transform.Translate(0, 3f, 0);
        image.sprite = BaseSprite;
        pressed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (disabled || !pressed) return;

        gameObject.transform.Translate(0, 3f, 0);
        image.sprite = BaseSprite;
        pressed = false;
    }

    // Use this for initialization
    void Start ()
    {
        parent = GetComponentInParent<BuildLocationScript>();
        image = GetComponent<Image>();
        turret = Prototype.GetComponent<TurretScript>();
        text = transform.Find("Name").gameObject.GetComponent<Text>();
	}
}
