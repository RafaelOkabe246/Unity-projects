using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField]
    private NPCConversation conversation;

    private void Start()
    {
        conversation = GetComponent<NPCConversation>();
    }

    public void PlayDialogue()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }
}
