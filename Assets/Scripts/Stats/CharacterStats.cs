using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

    public int maxHealth,maxMana ;

    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat armor;
    public GameObject txtDamage;
    public Transform transDmg;
    public bool isDead;

    public int arm , dmg ;
    public event System.Action<int, int> OnHealthChanged;

    private void Awake() {
        currentHealth = maxHealth;
    }
    
    public void Update() {
        if(Input.GetButton("Stats")) {
            Stats();
        }
    }
    public void Healthmodifer(int hp) {
        int kontrol = currentHealth + hp;
        if (kontrol >= maxHealth) {
            currentHealth = maxHealth;
        }
        else {
            currentHealth += hp;
        }
        
    }

    public void Stats() {
        arm = armor.GetValue();
        dmg = damage.GetValue();
        //Debug.Log("armor = " + arm + " Damage = " + dmg);
    }
    public void TakeDamage(int damage,int a) {
        damage = damage * a;
        float maxdamage = damage + ((damage / 100f) * 15);
        int randDmg = (int)Random.Range(damage, maxdamage);
        damage = randDmg;
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);

        Debug.Log(damage + "Get Damaged" + this.gameObject.name);
        currentHealth -= damage;        //Debug.Log(transform.name + "Takes " + damage + "Damage");

        if (this.gameObject.name == "Player") {
            StaticMethods.FindInActiveObjectByName("CurHp").gameObject.GetComponent<Text>().text = currentHealth.ToString();
        }

        foreach (Canvas c in FindObjectsOfType<Canvas>()) {
            if (c.renderMode != RenderMode.WorldSpace) {
                GameObject txtDmgUI = Instantiate(txtDamage, transform.position, Quaternion.identity, c.transform) as GameObject;
                if (a > 1) {
                    txtDmgUI.GetComponent<Text>().fontSize = 36;
                    txtDmgUI.GetComponent<Text>().fontStyle = FontStyle.Bold;
                    txtDmgUI.GetComponent<Text>().color = new Color(236, 151, 97);
                }

                txtDmgUI.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);
                txtDmgUI.GetComponent<RectTransform>().position += new Vector3(0f, 50f, 0f);
                txtDmgUI.AddComponent<DamageUI>();
                txtDmgUI.GetComponent<Text>().text = damage.ToString();
                if (transform.name == "Player") {
                    txtDmgUI.GetComponent<Text>().color = Color.red;
                    txtDmgUI.GetComponent<RectTransform>().position += new Vector3(0f, 250f, 0f);
                }
            }
        }

        OnHealthChanged?.Invoke(maxHealth, currentHealth);


        if (currentHealth <= 0 && isDead == false) {
            currentHealth = 0;
            Die();
        }
        
    }
    public virtual void Die() {
        // Die in some way
        //This method  is meant to be overwritten
        isDead = true;
        Debug.Log(transform.name + " died.");
        int rand = (int)Random.Range(1f, 5f);
        FindObjectOfType<AudioManager>().Play("dead" + rand);
    }
}
