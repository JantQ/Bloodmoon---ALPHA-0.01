using UnityEngine;
using System.Collections.Generic;
using System;

public class IQuest : MonoBehaviour {
    public enum QuestType {
        Story,
        Extra
    }
    [Header("Quest Settings")]
    [SerializeField] protected string questName;
    [SerializeField] protected List<Step> nodes;
    [SerializeField] protected QuestType qt = QuestType.Story;

    protected QuestManager qm;

    protected virtual void ProgressQuest(){
        foreach (var node in nodes) {
            if (node.done == true) {
                continue;
            }
            else {
                node.done = true;
                if (node.id >= nodes.Count) {
                    CompleteQuest();
                }
                break;
            }
        }

    }
    void OnEnable() {
        qm = FindAnyObjectByType<QuestManager>();
    }

    [ContextMenu("Test")]
    public void GetCurrentProgress() {
        int prog = 0;
        foreach (var node in nodes) {
            if (node.done == true) {
                prog += 1;
            }
            else {
                int count = nodes.Count;
                Debug.Log($"Current Progress for the Quest: {questName}, is {prog}/{count} done!");
                break;
            }
        }
    }
    
    protected virtual void CompleteQuest() {

    }
}

[System.Serializable]
public class Step {
    public int id = 1;
    public string name = "Quest";
    public bool done = false;
    public List<Requirement> reqs;
}

public enum RequirementType
{
    Item,
    Kill,
    Talk
}

[System.Serializable]
public class Requirement {
    public RequirementType type;
    public string targetId;
    public int neededAmount = 1;
    public int currentAmount = 0;

    public bool IsMet() => currentAmount >= neededAmount;
}