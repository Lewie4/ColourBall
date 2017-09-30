using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [System.Serializable]
    public enum BlockType
    {
        Red,
        Blue,
        Green,
        Yellow}

    ;

    [SerializeField] private BlockType m_blockType;

    private void Awake()
    {
        switch (m_blockType)
        {
            case BlockType.Red:
                {
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }

            case BlockType.Blue:
                {
                    this.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                }

            case BlockType.Green:
                {
                    this.GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                }

            case BlockType.Yellow:
                {
                    this.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                }

            default:
                {
                    Debug.Log("No Colour Set??");
                    break;
                }
        }
    }

    public void BlockHit()
    {
        this.GetComponentInParent<BlockGroup>().DestoryGroup();
    }
}
