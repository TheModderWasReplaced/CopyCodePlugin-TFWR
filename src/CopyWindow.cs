using ModHelper.API;
using UnityEngine;
using UnityEngine.UI;
using ModHelper.Extensions;

namespace CopyCode;

public static class CopyWindow
{
    private const string OptionTemplate = "MinimizeButton";
    private const string SaveButton = "SaveButton";

    private static void Copy(CodeWindow window)
    {
        var code =  window.CodeInput.text;
        GUIUtility.systemCopyBuffer = $"```py\n{code}\n```";
    }

    /// <summary>
    /// Adds a copy option to the given window
    /// </summary>
    /// <returns>Option added or null</returns>
    public static GameObject AddCopyOption(CodeWindow window)
    {
        var template = window.transform.Find(OptionTemplate);

        if (template == null)
        {
            FarmPlugin.Msg<CopyCodePlugin>($"The template '{OptionTemplate}' was not found.");
            return null;
        }
                    
        // Clone Minimize btn
        var btn = Object.Instantiate(template, template.parent);
        btn.name = SaveButton;
        btn.SetSiblingIndex(template.GetSiblingIndex() + 1);

        // Offset to the left
        btn.transform.localPosition -= new Vector3(ModHelper.Constants.WINDOW_ICON_OFFSET_X, 0);
        
        // Add description
        UpdateToolTip(window);

        // Set click event
        btn.GetComponent<ColoredButton>().SetListener(() =>
        {
            Copy(window);
            Saver.Inst.warningPopup.ShowPopup($"'{window.fileName}' was copied successfully!", [
                new WarningPopup.ButtonData("Ok", Saver.Inst.warningPopup.Close)
            ]);
        });

        // Set icon
        btn.Find("Image").GetComponent<Image>().LoadSprite<CopyCodePlugin>(
            "Resources.copy-icon.png", 
            64
        );
        
        return btn.gameObject;
    }

    public static void UpdateToolTip(CodeWindow window)
    {
        var btn = window.transform.Find(SaveButton);
        
        if (btn == null)
            return;
        
        // Update tooltip
        btn.GetComponent<ColoredButton>().tooltipDescription = $"Copy '{window.fileName}' to the clipboard";
    }
}