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

    private int lives;
    private int money;
    private HealthDrawerScript healthDrawer;
    private MoneyDrawer moneyDrawer;

    // Use this for initialization
    void Start ()
    {
        lives = MaxLives;
        money = InitialMoney;

        healthDrawer = GetComponent<HealthDrawerScript>();
        moneyDrawer = GetComponent<MoneyDrawer>();

        moneyDrawer.Draw(InitialMoney);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void EnemyEscaped(GameObject enemy)
    {
        lives--;
        CameraShaker.Instance.Shake();
        healthDrawer.Draw(lives);
    }

    public void EnemyKilled(GameObject enemy)
    {
        money += enemy.GetComponent<EnemyScript>().Money;
        moneyDrawer.Draw(money);
    }

    public int GetMoney()
    {
        return money;
    }

    public void TurretBuilt(GameObject turret)
    {
        money -= turret.GetComponent<TurretScript>().Cost;
        moneyDrawer.Draw(money);
    }
}
