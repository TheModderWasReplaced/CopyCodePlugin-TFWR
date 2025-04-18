using AgriCore.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace CopyCode;

internal static class CopyWindow
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
    public static void AddCopyOption(CodeWindow window)
    {
        var template = window.transform.Find(OptionTemplate);

        if (template == null)
        {
            Log.Warning($"The template '{OptionTemplate}' was not found.");
            return;
        }
                    
        // Clone Minimize btn
        var btn = Object.Instantiate(template, template.parent);
        btn.name = SaveButton;
        btn.SetAsLastSibling();

        // Add description
        UpdateToolTip(window);

        // Set click event
        btn.GetComponent<ColoredButton>().SetListener(() =>
        {
            Copy(window);
            MainSim.Inst.warningPopup.ShowPopup($"'{window.fileName}' was copied successfully!", [
                new WarningPopup.ButtonData("Ok", MainSim.Inst.warningPopup.Close)
            ]);
        });

        // Set icon
        btn.Find("Image").GetComponent<Image>().LoadSprite<Plugin>(
            "CopyCode.Resources.copy-icon.png", 
            64
        );
    }

    internal static void UpdateToolTip(CodeWindow window)
    {
        var btn = window.transform.Find(SaveButton);
        
        if (btn == null)
            return;
        
        if (!btn.TryGetComponent(out ColoredButton cb))
            return;
        
        // Update tooltip
        cb.tooltipDescription = $"Copy '{window.fileName}' to the clipboard";
    }
}