using UnityEngine;
using UnityEngine.UI;
public class HpMpStatsUI : MonoBehaviour
{
    public GameObject player;
    public int level;
    public float exp, sp;
    public Image healthSlider;
    public Text CurHp, MaxHp, Level , Exp , Sp;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<CharacterStats>().OnHealthChanged += OnHealthChaned;
        level = player.GetComponent<Player>().level;
        exp = player.GetComponent<Player>().experience;
        sp = player.GetComponent<Player>().skillPoint;


        MaxHp.text = "/ " + player.GetComponent<CharacterStats>().maxHealth.ToString();
        CurHp.text = player.GetComponent<CharacterStats>().currentHealth.ToString();

        Level.text = "Level : " + level.ToString();
        Exp.text = "Expereince : " + exp.ToString();
        Sp.text = "SkillPoint : " + sp.ToString();
    }
    void OnHealthChaned(int maxHealth, int currentHealth) {
        if (healthSlider != null) {
            float healthPercent = currentHealth / (float)maxHealth;

            healthSlider.fillAmount = healthPercent;
            MaxHp.text = "/ " + maxHealth.ToString();
            CurHp.text = currentHealth.ToString();

            
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
