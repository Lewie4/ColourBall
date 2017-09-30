using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    private List<Block> m_blocks;
    private int m_blockCount;

    private bool m_destroyed = false;

    [SerializeField] private int m_scoreMod = 1;

    private void Start()
    {
        m_blocks = new List<Block>();
        Block[] childBlocks = this.transform.GetComponentsInChildren<Block>();
        for (int i = 0; i < childBlocks.Length; i++)
        {
            m_blocks.Add(childBlocks[i]);
        }
        m_blockCount = m_blocks.Count;
    }

    public void DestoryGroup()
    {
        if (!m_destroyed)
        {
            m_destroyed = true;
            for (int i = 0; i < m_blockCount; i++)
            {
                Destroy(m_blocks[i].gameObject);
            }
            ScoreManager.Instance.AddScore(m_blockCount * m_scoreMod);
        }
    }
}
