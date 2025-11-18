using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Animator m_Animator;
    public CapsuleCollider CapsuleCollider;
    private AudioSource m_AudioSource;
    public List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();
    private List<Collider> m_Colliders = new List<Collider>();
    private bool isRagdoll = false;

    private float minpitch = -1f;
    private float maxpitch = 1f;


    private void Awake()
    {
        TryGetComponent(out m_Animator);
        TryGetComponent(out CapsuleCollider);
        TryGetComponent(out m_AudioSource);
        if(m_Animator == null)
        {
            return;
        }
        GetComponentsInChildren(m_Rigidbodies);
        GetComponentsInChildren(m_Colliders);

        for(int i = 0;i < m_Rigidbodies.Count;i++)
        {
            m_Rigidbodies[i].isKinematic = true;
            m_Colliders[i]. isTrigger = true;
        }

        CapsuleCollider.isTrigger = false;
    }


    public void EnableRagdoll()
    {
        m_AudioSource.pitch = Random.Range(minpitch, maxpitch);
        m_AudioSource.Play();

        if(isRagdoll) {return; }

        isRagdoll = !isRagdoll;
        for (int i = 0; i < m_Rigidbodies.Count; i++)
        {
            m_Rigidbodies[i].isKinematic = false;
            m_Rigidbodies[i].linearVelocity = Vector3.zero;
            m_Colliders[i].isTrigger = false;
        }
        CapsuleCollider.isTrigger = true;
        m_Animator.enabled = false;
    }
}
