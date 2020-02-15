using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState {
        RUNNING,
        SEARCHING,
        ATTACKING,
        DIYING
    }
    private NavMeshAgent agent;
    private GameObject goal;

    public float speed;
    public int attackForce;
    public float attackSpeed;
    public int lives;
    public float visionRange;

    public Image lifeImage;

    private UnityController currentTarget;
    private int initialLives;
    private EnemyState currentState;
    private float distToAttack = 2f;
    private float currentTimeAttack;

    public Transform areaCircle;
    private bool paused;

    public Animator anim;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        areaCircle.localScale = new Vector3(visionRange * 2, 0.01f, visionRange * 2);
        initialLives = lives;
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Goal");

        agent.SetDestination(new Vector3(goal.transform.position.x,0.5f, goal.transform.position.z));
        agent.speed = speed;
        //agent.stoppingDistance = distToAttack;
        currentTarget = null;
        currentState = EnemyState.RUNNING;
        currentTimeAttack = 0;
        audio = GetComponent<AudioSource>();
        //Debug.Log("Enter start enemy"+ goal);
    }

    // Update is called once per frame
    void Update()
    {

        if (paused) {
            return;
        }
        if(lives == 0) {
            return;
        }
        //Buscar unidades cercanas para atacarlas
        if (currentState == EnemyState.RUNNING)
        {
            currentTarget = UnitiesManager.instance.SearchUnit(transform.position, visionRange);
            if (currentTarget != null)
            {
                //anim.SetTrigger("Idle");
                currentState = EnemyState.SEARCHING;
                agent.SetDestination(new Vector3(currentTarget.transform.position.x, 0.5f, currentTarget.transform.position.z));
            }
            else {

                float dist = Vector3.Distance(transform.position, goal.transform.position);
                if (dist <= distToAttack)
                {
                    ActivateEnd();
                }
            }
        }
        else if (currentState == EnemyState.SEARCHING) {
            if (currentTarget.lives > 0)
            {
                float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
                //Debug.Log("In Search "+dist+" "+distToAttack);
                if (dist <= distToAttack)
                {
                    anim.SetTrigger("Idle");
                    agent.speed = 0;
                    currentState = EnemyState.ATTACKING;
                }
            }
            else {
                anim.SetTrigger("Run");
                currentTimeAttack = 0;
                currentState = EnemyState.RUNNING;
                agent.speed = speed;
                agent.SetDestination(new Vector3(goal.transform.position.x, 0.5f, goal.transform.position.z));
            }
        }
        else if (currentState == EnemyState.ATTACKING)
        {
            if (currentTarget.lives > 0)
            {
                currentTimeAttack += Time.deltaTime;
                if (currentTimeAttack > attackSpeed)
                {
                    audio.Play();
                    anim.SetTrigger("Attack");
                    currentTimeAttack = 0;
                    bool died = currentTarget.ReciveDamage(attackForce);
                    if (died)
                    {
                        anim.SetTrigger("Run");
                        currentState = EnemyState.RUNNING;
                        agent.speed = speed;
                        agent.SetDestination(new Vector3(goal.transform.position.x, 0.5f, goal.transform.position.z));
                    }
                }
            }
            else {
                anim.SetTrigger("Run");
                currentTimeAttack = 0;
                currentState = EnemyState.RUNNING;
                agent.speed = speed;
                agent.SetDestination(new Vector3(goal.transform.position.x, 0.5f, goal.transform.position.z));
            }
        }

    }

    public void ActivateEnd() {
        lives = 0;
        anim.SetTrigger("Idle");
        anim.SetTrigger("Attack");
        GameManager.instance.LoseLive();
        EnemyGenerator.instance.RemoveEnemy(this,false);
        Destroy(this.gameObject, 0.1f);
    }

    public bool ReciveDamage(UnityController attacker, int damage) {
        lives-=damage;
        //Debug.Log("Enemy recive damage");
        if (lives <= 0)
        {
            //Anim die
            agent.speed = 0;
            anim.SetTrigger("Die");
            lifeImage.fillAmount = 0;
            currentState = EnemyState.DIYING;
            EnemyGenerator.instance.RemoveEnemy(this,true);
            Destroy(this.gameObject,2f);
            return true;
        }
        else {
            lifeImage.fillAmount = (float)lives / (float)initialLives;
        }
        return false;
    }

    public void Paused(bool p)
    {
        paused = p;
    }
}
