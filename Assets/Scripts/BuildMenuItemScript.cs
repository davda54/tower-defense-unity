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
    private Text price;
    private GameObject parent;
    private Image image;
    private bool pressed = false;
    private bool disabled = false;

    void Update()
    {
        var enoughMoney = GameManager.Instance.EnoughMoneyForTurret(Prototype.tag);
        price.text = "$" + GameManager.Instance.MoneyForTurret(Prototype.tag);

        if(disabled && enoughMoney)
        {
            disabled = false;
            text.color = new Color(0, 0, 0, 1);
            price.color = new Color(0, 0, 0, 1);
        }
        else if(!disabled && !enoughMoney)
        {
            disabled = true;
            text.color = new Color(0, 0, 0, 0.25f);
            price.color = new Color(0, 0, 0, 0.25f);
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
        if (disabled) return;

        image.sprite = BaseSprite;

        if (!pressed) return;

        gameObject.transform.Translate(0, 3f, 0);
        pressed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (disabled || !pressed) return;
       
        var instance = Instantiate(Prototype, parent.transform.position, Quaternion.identity);
        GameManager.Instance.TurretBuilt(instance);

        parent.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        parent = GetComponentInParent<BuildLocationScript>().gameObject;
        image = GetComponent<Image>();
        text = transform.Find("Name").gameObject.GetComponent<Text>();
        price = transform.Find("Price").gameObject.GetComponent<Text>();
    }
}
