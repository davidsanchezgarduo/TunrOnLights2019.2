using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject goal;
    public float speed = 2;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Goal");
        UnitiesManager.instance.AddCivil(this);

    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, goal.transform.position);
        if (dist < 1) {
            GameManager.instance.RescueCivil();
            Destroy(this.gameObject);
        }
    }

    public void ActiveCivil() {
        //anim.enabled = true;
        anim.SetTrigger("Run");
        agent.SetDestination(new Vector3(goal.transform.position.x, 0.5f, goal.transform.position.z));
        agent.speed = speed;
    }
}
