using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;
    public static GameManager Instance
    {
        get
        {
            if (gameManagerInstance == null)
                gameManagerInstance = FindObjectOfType<GameManager>();

            return gameManagerInstance;
        }
    }

    public int MaxLives;
    public int InitialMoney;

    public int InitialTurretPrice;
    public int InitialRocketPrice;
    public int TurretPriceAddition;
    public int RocketPriceAddition;

    private int turretPrice;
    private int rocketPrice;

    private int lives;
    private int money;
    private HealthDrawerScript healthDrawer;
    private MoneyDrawer moneyDrawer;

    // Use this for initialization
    void Start ()
    {
        lives = MaxLives;
        money = InitialMoney;

        turretPrice = InitialTurretPrice;
        rocketPrice = InitialRocketPrice;

        healthDrawer = GetComponent<HealthDrawerScript>();
        moneyDrawer = GetComponent<MoneyDrawer>();

        moneyDrawer.Draw(InitialMoney);
    }

    public void EnemyEscaped(GameObject enemy)
    {
        lives--;
        CameraShaker.Instance.Shake();
        healthDrawer.Draw(lives);
    }

    public void EnemyKilled(GameObject enemy)
    {
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int value)
    {
        money += value;
        moneyDrawer.Draw(money);
    }

    public void TurretBuilt(GameObject turret)
    {
        if (turret.CompareTag("turretTower"))
        {
            money -= turretPrice;
            turretPrice += TurretPriceAddition;
        }
        else
        {
            money -= rocketPrice;
            rocketPrice += RocketPriceAddition;
        }

        moneyDrawer.Draw(money);
    }

    public void CoinCollected(GameObject coin)
    {
        money += CoinScript.Value;
        moneyDrawer.Draw(money);
    }

    public bool EnoughMoneyForTurret(string tag)
    {
        if(tag == "turretTower")
            return money >= turretPrice;

        return money >= rocketPrice;
    }

    public int MoneyForTurret(string tag)
    {
        return tag == "turretTower" ? turretPrice : rocketPrice;
    }
}
