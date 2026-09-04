using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    [SerializeField] private GameObject bleedEffectPrefab; // 你的流血粒子预制体
    [SerializeField] private int poolSize = 20;

    private readonly Queue<ParticleSystem> pool = new Queue<ParticleSystem>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            var go = Instantiate(bleedEffectPrefab, transform);
            go.SetActive(false);
            pool.Enqueue(go.GetComponent<ParticleSystem>());
        }
    }

    public void PlayBleed(Transform target)
    {
        if (pool.Count == 0) return; // 池空了就先忽略，避免 null

        var ps = pool.Dequeue();

        // 位置跟随：挂到怪物身上，本地坐标归零
        ps.transform.SetParent(target, false);
        ps.transform.localPosition = Vector3.zero;

        ps.gameObject.SetActive(true);
        ps.Play();

        StartCoroutine(ReturnToPool(ps));
    }

    private IEnumerator ReturnToPool(ParticleSystem ps)
    {
        yield return new WaitUntil(() => ps ? !ps.isPlaying : true); // 等粒子播完

        if (ps)
        {
            ps.Stop();
            ps.gameObject.SetActive(false);
            ps.transform.SetParent(transform, false); // 回收时解除父子关系
            pool.Enqueue(ps);
        }
    }
}