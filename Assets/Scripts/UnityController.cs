using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityController : MonoBehaviour
{
    private enum UnitState
    {
        SEARCHING,
        ATTACKING,
        DIYING
    }

    public int type;
    public string typeName;
    public string socialName;
    public int forceAttack;
    public float lightRange;
    public float speedAttack;
    public int lives;
    public Vector2 myTextCoord;
    public Material topMaterial;
    public Material normalMaterial;

    public Transform areaCircle;
    public Image lifeImage;
    private EnemyController currentTarget;
    private int initialLives;

    private UnitState currentState;
    private float currentTimeAttack;
    private bool paused;
    public ParticleSystem fireParticles;

    public Animator anim;
    public GameObject unitFbx;
    private AudioSource myAudioSource;

    public AudioClip deadClip;
    public GameObject lifeBar;


    // Start is called before the first frame update
    void Start()
    {
        lifeBar.SetActive(false);
        unitFbx.SetActive(false);
        paused = false;
        currentTimeAttack = 0;
        currentState = UnitState.SEARCHING;
        initialLives = lives;
        currentTarget = null;
        //areaCircle.localScale = new Vector3(lightRange*2,0.01f,lightRange*2);
        myAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    public void UpdateUnit()
    {

        if (paused) {
            return;
        }

        if(lives == 0) {
            return;
        }

        if (currentState == UnitState.SEARCHING)
        {
            currentTarget = EnemyGenerator.instance.CheckEnemyInRange(transform.position, lightRange*56);
            if (currentTarget != null) {
                currentState = UnitState.ATTACKING;
                //transform.LookAt(currentTarget.transform);
            }
            
        }
        else if (currentState == UnitState.ATTACKING) {
            if (currentTarget.lives > 0)
            {
                currentTimeAttack += Time.deltaTime;
                //Debug.Log(currentTimeAttack+"  "+speedAttack);
                transform.LookAt(new Vector3(currentTarget.transform.position.x, 0.5f, currentTarget.transform.position.z));
                if (currentTimeAttack > speedAttack)
                {
                    ///Debug.Log("Shoot");
                    myAudioSource.Play();
                    anim.SetTrigger("Shoot");
                    currentTimeAttack = 0;
                    fireParticles.Play();
                    if (currentTarget.ReciveDamage(this, forceAttack))
                    {
                        currentTarget = null;
                        currentState = UnitState.SEARCHING;
                    }
                }
            }
            else {
                currentTimeAttack = 0;
                currentTarget = null;
                currentState = UnitState.SEARCHING;
            }
        }
    }

    public bool ReciveDamage(int damage) {
        lives-= damage;
        //Debug.Log("Unit Recive damage");
        if (lives <= 0) {
            myAudioSource.Stop();
            myAudioSource.clip = deadClip;
            myAudioSource.Play();
            anim.SetTrigger("Die");
            //Anim die
            //Remove light
            lifeImage.fillAmount = 0;
            currentState = UnitState.DIYING;
            UnitiesManager.instance.RemoveUnity(this);
            Destroy(this.gameObject,2f);
            return true;
        }
        else {
            lifeImage.fillAmount = (float)lives / (float)initialLives;
        }
        return false;
    }

    public void StablishUnit(UnitLevelData stadistics, string name) {
        unitFbx.SetActive(true);
        lifeBar.SetActive(true);
        //GetComponent<MeshRenderer>().material = normalMaterial;
        GetComponent<MeshRenderer>().enabled = false;
        typeName = name;
        forceAttack = stadistics.forceAttack;
        lightRange = stadistics.lightRange;
        speedAttack = stadistics.speedAttack;
        lives = stadistics.lives;
    }

    public void Paused(bool p) {
        paused = p;
    }
}
