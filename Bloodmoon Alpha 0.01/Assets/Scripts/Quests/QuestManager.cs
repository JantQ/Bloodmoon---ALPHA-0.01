using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {
    [Header("Quests")]
    [SerializeField] private List<IQuest> allQuests;

    [Header("Progress")]
    [SerializeField] private List<IQuest> availableQuests;
    [SerializeField] private List<IQuest> currentQuests;
    [SerializeField] private List<IQuest> completedQuests;
}