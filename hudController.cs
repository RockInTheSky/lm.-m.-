using UnityEngine;
using UnityEngine.UIElements;

public class hudController : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;
    public VisualElement ui;
    public UIDocument UIDocument;

    public Label hpLabel;
    public string hpText;
    public int hpMax = 5;
    public int hp;

    public Label ammoLabel;
    public string ammoText;
    public int ammoMax;
    public int ammo;

    public Label moneyLabel;
    public string moneyText;
    public int money;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        ui = GetComponent<UIDocument>().rootVisualElement;

        Health();
        Ammo();
        Money();
        ammoMax = (int)gun.GetComponent<Gun>().magSize;
    }

    // Update is called once per frame
    void Update()
    {
        Health();
        Ammo();
        Money();
    }

    void Health()
    {
	hpLabel = ui.Q<Label>("hp-label");
	hp = (int)player.GetComponent<Health>().health;
    hpText = "";
	for(int i = 0; i < hpMax; i++)
	{
	    hpText += (i <= hp-1) ? "♥" : "♡";
	}
	hpLabel.text = hpText;
    }

    void Ammo()
    {
	ammoLabel = ui.Q<Label>("ammo-label");
	ammo = (int)gun.GetComponent<Gun>().ammo;

	ammoText = "⁍ " + ammo.ToString() + " / " + ammoMax.ToString();
	ammoLabel.text = ammoText;
    }

    void Money()
    {
	moneyLabel = ui.Q<Label>("money-label");
	money = (int)player.GetComponent<PlayerBehaviour>().money;

	moneyText = "$ " + money.ToString();
	moneyLabel.text = moneyText;
    }
}
