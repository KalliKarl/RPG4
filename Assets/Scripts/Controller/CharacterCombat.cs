using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour{

    public float attackSpeed ;
    private float attackCooldown,healingCooldown = 0f;
    public float attackDelay;
    public event System.Action OnAttack;
    CharacterStats myStats;
    Image healthSlider;
    public GameObject player;
    private void Start() {
        myStats = GetComponent<CharacterStats>();
        healthSlider = StaticMethods.FindInActiveObjectByName("MobHpTop").GetComponent<Image>();
        player = GameObject.Find("Player");
    }

    private void Update() {

        attackCooldown -= Time.deltaTime;
        healingCooldown -= Time.deltaTime;
        if (healingCooldown <= 0 && player.GetComponent<PlayerStats>().isDead != true) {

            int _cCurhp = this.GetComponent<CharacterStats>().currentHealth;

            int _cMaxhp = this.GetComponent<CharacterStats>().maxHealth;
            if (_cCurhp != _cMaxhp) {
                _cCurhp += (int)((_cMaxhp / 100f) * 5);
                if (_cCurhp >= _cMaxhp) {
                    _cCurhp = _cMaxhp;
                    this.GetComponent<CharacterStats>().Healthmodifer(_cCurhp);

                }

            }

        }
        if (gameObject.name == "Player") 
            this.GetComponent<Animator>().SetBool("isAttack", false);
        //if (gameObject.layer == 11)
            //this.GetComponent<Animator>().SetBool("isAttack", false);
    }
    public void Attack(CharacterStats targetStats,int a) {
        if(attackCooldown <= 0 && targetStats.isDead == false) {
            
            if(this.gameObject.name == "Player")
            Debug.Log("Attack in");
            StartCoroutine(DoDamage(targetStats,attackDelay,a));
            attackCooldown = 2.2f / attackSpeed;
            if (OnAttack != null)
                OnAttack();
            if (gameObject.name == "Player") {
                FaceTarget();
                
            }
            else if(gameObject.layer == 11) {
                this.GetComponent<Animator>().SetBool("isAttack", true);
            }
        }
    }

    IEnumerator DoDamage (CharacterStats targetStats , float delay,int a) {
        yield return new WaitForSeconds(delay);
        int _cDamage = myStats.damage.GetValue();
        targetStats.TakeDamage(_cDamage,a);
        if(this.gameObject.name == "Player") {
            Animator anim = this.GetComponent<Animator>();
            float randomAttack = (int)Random.Range(0f, 3f);
            anim.SetFloat("AttackType", randomAttack);
            anim.SetBool("isAttack", true);
            Debug.Log("taked damage");
        }
        
        int rand = (int)Random.Range(0f, 5f);
        //Debug.Log(rand +"DoDamage" + myStats.damage.GetValue());
        
        if (healthSlider != null && player.GetComponent<PlayerControl>().focus != null) {
            Interactable enemy =    player.GetComponent<PlayerControl>().focus;
            float healthPercent =(float) enemy.GetComponent<EnemyStats>().currentHealth / (float)enemy.GetComponent<EnemyStats>().maxHealth;
            StaticMethods.FindInActiveObjectByName("MobCHp").GetComponent<Text>().text = enemy.GetComponent<EnemyStats>().currentHealth.ToString();
            StaticMethods.FindInActiveObjectByName("MobMHp").GetComponent<Text>().text = " /" + enemy.GetComponent<EnemyStats>().maxHealth.ToString();
            Debug.Log(healthPercent);
            healthSlider.fillAmount = healthPercent;
        }
        FindObjectOfType<AudioManager>().Play("hit"+rand);
    }
    void FaceTarget() {
        Vector3 direction = (player.GetComponent<PlayerControl>().focus.transform.position - player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        player.transform.rotation = lookRotation;//Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }
}
