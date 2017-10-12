using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();

        if (!DialogueManager.dialogueStarted)
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        else if (FindObjectOfType<DialogueManager>().writing)
            FindObjectOfType<DialogueManager>().SkipWriting();
        else
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position);
            if (dist > radius && DialogueManager.dialogueStarted)
            {
                FindObjectOfType<DialogueManager>().EndDialogue();
            }
        }
    }
}
