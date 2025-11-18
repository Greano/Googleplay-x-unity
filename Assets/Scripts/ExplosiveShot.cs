using UnityEngine;

public class ExplosiveShot : Shot
{
    public GameObject ExplosionVFXprefab;
    public float explosionradius = 5f;
    public float explosionForce = 30f;
    public float upwardForce = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(ExplosionVFXprefab, transform.position, transform.rotation);
        var rb = Physics.OverlapSphere(collision.contacts[0].point, explosionradius);

        foreach(var rigidbody in rb)
        {
            if (rigidbody.CompareTag("Target"))
            {
                var ragdoll = rigidbody.GetComponent<Ragdoll>();
                if (ragdoll != null)
                {
                    ragdoll.EnableRagdoll();
                    foreach(var _rb in ragdoll.m_Rigidbodies)
                    {
                        _rb.AddExplosionForce(explosionForce, collision.contacts[0].point, explosionradius, upwardForce, ForceMode.Impulse);
                    }
                }
                rigidbody.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collision.contacts[0].point, explosionradius, upwardForce, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);

    }
}
