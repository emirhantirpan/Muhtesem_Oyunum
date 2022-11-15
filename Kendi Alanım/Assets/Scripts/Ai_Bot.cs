using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
public class Ai_Bot : MonoBehaviour
{
    NavMeshAgent bot;
    [SerializeField] Transform target;
    [SerializeField] float lookDistance = 25f;
    public Animator anim;

    private IEnumerator _attackCoroutine;
    [SerializeField] private Player _player;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;

    void Start()
    {
        bot = GetComponent<NavMeshAgent>();
        bot.speed = 20f;
        
    }
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        bot.SetDestination(target.position);
        Vector3 relativePos = target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(relativePos);
        Quaternion LookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookrotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = LookAtRotationY;

        // z fix

        if (distance <= lookDistance)
        {
            bot.speed = 28f;
            
        }
        else
        {
            bot.speed = 22f;
        }
  
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookDistance);
    }
   private void OnTriggerEnter(Collider col)
   {
        if(col.gameObject.tag == "PlayerMeleeRange")
        {
            anim.SetBool("IsRun", false);
            anim.SetBool("IsAttack", true);
            bot.speed = 0;
            _attackCoroutine = Attack();
            StartCoroutine(_attackCoroutine);
        }
        
   }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PlayerMeleeRange")
        {
            anim.SetBool("IsAttack", false);
            anim.SetBool("IsRun", true);
            bot.speed = 15f;
            StopCoroutine(_attackCoroutine);

        }
    }

   private IEnumerator Attack()
   {
        while(true)
        {
            _player.TakeDamage(_damage);
            yield return new WaitForSeconds(_attackSpeed);
        }
   }


}
