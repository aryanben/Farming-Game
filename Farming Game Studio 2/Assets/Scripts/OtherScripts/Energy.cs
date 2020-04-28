using UnityEngine;
using UnityEngine.UI;
public class Energy : MonoBehaviour
{
    public static Energy Instance;
    public int startingEnergy = 100;
    public float currentEnergy;
    public Slider EnergySlider;
    public float cantWalkFor5Seconds;
    public bool cantWalk;
    public float energyPoint = 0.01f;
    public float destroyPlayerTime = 2f;
    public bool destroyPlayer;
    public bool canMove;
    private void Awake()
    {
        currentEnergy = startingEnergy;
        EnergySlider.maxValue = currentEnergy;
        EnergySlider.value = currentEnergy;
    }

    void Start()
    {
        Instance = this;
    }

    public void DecreaseEnergy(float amount)
    {
      currentEnergy -= amount;

        EnergySlider.value = Energy.Instance.currentEnergy;

        if (currentEnergy <= 0)
        {
            canMove = false;
            cantWalk = true;
        }
    }

    public void IncreaseEnergy(float amount)
    {
        Instance.currentEnergy += amount;

        Instance.EnergySlider.value = Energy.Instance.currentEnergy;

        if (Instance.currentEnergy >= 100)
        {
            Instance.currentEnergy = 100;
        }
    }
}
