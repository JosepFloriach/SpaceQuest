using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MissionSelectionUIBuilder : MonoBehaviour
{
    [SerializeField] private string levelsPath;
    [SerializeField] private GameObject levelUIPrefab;
    [SerializeField] private GameObject gridContainer;

    private GameObject backButton;

    private void Start()
    {
        BuildLevelsUI();
    }

    private void BuildLevelsUI()
    {
        DirectoryInfo dir = new DirectoryInfo(levelsPath);
        FileInfo[] levels = dir.GetFiles("*.unity");
        foreach (FileInfo level in levels)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(level.Name);
            var uiObject = GameObject.Instantiate(levelUIPrefab);
            uiObject.name = level.Name;
            uiObject.transform.parent = gridContainer.transform;
            uiObject.GetComponentInChildren<TextMeshProUGUI>().text = nameWithoutExtension;
            uiObject.GetComponent<SceneLoader>().SetScenePath(nameWithoutExtension);
        }
    }
}
