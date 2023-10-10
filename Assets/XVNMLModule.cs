using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using XVNML.Core.Dialogue;
using XVNML.Utility.Dialogue;
using XVNML.XVNMLUtility;
using XVNML.XVNMLUtility.Tags;

public enum XVNMLFileMode
{
    Proxy,
    Source
}

public enum ActionResult
{
    Unknown = -1,
    Ok,
    Error
}

public sealed class XVNMLModule : MonoBehaviour
{
    [Header("Basic Configuration")]
    [SerializeField]
    private string xvnmlFileName;

    [SerializeField]
    private string projectRootName;

    [SerializeField]
    private string dialogueName;

    [SerializeField]
    private XVNMLFileMode fileMode;

    [Header("Flags")]
    [SerializeField]
    private bool _addFileModeSuffix = true;

    [SerializeField]
    private bool _playOnBuildFinish = true;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI TMP_Text;

    private bool _isFinished = false;
    
    private readonly Queue<Func<ActionResult>?> _actionQueue = new();

    private const string ProxySuffixString = ".main";
    private const string SourceSuffixString = ".source";
    private const string XVNMLExtensionString = ".xvnml";
    private const int SingleProcess = 0;


    private void Start()
    {
        StringBuilder sb = new();

        sb.Append(Application.streamingAssetsPath);
        sb.Append(@$"\{projectRootName}\");
        sb.Append(xvnmlFileName);

        if (_addFileModeSuffix)
        {
            sb.Append(fileMode switch
            {
                XVNMLFileMode.Proxy => ProxySuffixString,
                XVNMLFileMode.Source => SourceSuffixString,
                _ => string.Empty
            });
        }

        sb.Append(XVNMLExtensionString);

        var targetPath = sb.ToString();

        XVNMLObj.Create(targetPath, PlayDialogue);
    }

    private void PlayDialogue(XVNMLObj dom)
    {
        if (dom == null) return;

        // Basic Setup
        DialogueScript script = dom.Root.GetElement<Dialogue>(dialogueName)?.dialogueOutput!;

        DialogueWriter.AllocateChannels(1);
        DialogueWriter.OnLineSubstringChange![SingleProcess] += UpdateText;
        DialogueWriter.OnNextLine![SingleProcess] += ClearText;
        DialogueWriter.OnLinePause![SingleProcess] += WaitForResponse;
        DialogueWriter.OnDialogueFinish![SingleProcess] += Finish;

        if (_playOnBuildFinish == false) return;

        DialogueWriter.Write(script, SingleProcess);

        StartCoroutine(QueueCycle());
    }

    private IEnumerator QueueCycle()
    {
        var result = ActionResult.Unknown;
        while (_isFinished == false && result != ActionResult.Error)
        {
            if (_actionQueue?.Count == 0)
            {
                yield return null;
                continue;
            }

            for (int i = 0; i < _actionQueue.Count; i++)
            {
                _actionQueue.TryDequeue(out var action);
                if (action == null) continue;

                if ((result = action.Invoke()) == ActionResult.Unknown) SendNewAction(action);

                if (result == ActionResult.Error)
                {
                    Debug.LogError($"An error occured within {action?.Method.Name}");
                }
            }

            yield return null;
        }
    }

    private void Finish(DialogueWriterProcessor sender)
    {
        SendNewAction(() =>
        {
            TMP_Text.text = string.Empty;
            _isFinished = true;
            return ActionResult.Ok;
        });
    }

    private void WaitForResponse(DialogueWriterProcessor sender)
    {
        SendNewAction(() =>
        {
            if (sender.IsPass)
            {
                DialogueWriter.MoveNextLine(sender);
                return ActionResult.Ok;
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                TMP_Text.text = string.Empty;
                DialogueWriter.MoveNextLine(sender);
                return ActionResult.Ok;
            }
            return ActionResult.Unknown;
        });
    }

    private void ClearText(DialogueWriterProcessor sender)
    {
        SendNewAction(() =>
        {
            TMP_Text.text = string.Empty;
            return ActionResult.Ok;
        });
    }

    private void UpdateText(DialogueWriterProcessor sender)
    {
        SendNewAction(() =>
        {
            TMP_Text.text = sender.DisplayingContent;
            return ActionResult.Ok;
        });
    }

    private void SendNewAction(Func<ActionResult>? action)
    {
        _actionQueue.Enqueue(action);
    }
}
