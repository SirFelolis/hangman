using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    public float radius = 96f;

    [SerializeField]
    private Vector2 interactTransform = Vector2.zero;

    public Transform player;

    /// <summary>
    /// Interaction method meant to be overridden for multiple different implementations.
    /// </summary>
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    public void CheckInteraction( Transform player )
    {
        this.player = player;
        float dist = Vector2.Distance(player.position, (Vector3)interactTransform + transform.position);
        if (dist < radius)
        {
            Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere((Vector3)interactTransform + transform.position, radius);
    }
}
