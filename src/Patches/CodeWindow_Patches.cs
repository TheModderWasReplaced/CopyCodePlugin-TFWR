using CopyCode.Helpers;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming

namespace CopyCode.Patches;

[HarmonyPatch(typeof(CodeWindow))]
internal static class CodeWindow_Patches
{
	private const string BUTTON_NAME = "CopyBtn";

	[HarmonyPatch(nameof(CodeWindow.OnNameTextEdited))]
	[HarmonyPostfix]
	public static void OnNameTextEdited_Postfix(CodeWindow __instance) {
		UpdateToolTip(__instance);
	}

	[HarmonyPatch(nameof(CodeWindow.Load))]
	[HarmonyPostfix]
	public static void Load_Postfix(CodeWindow __instance) {
		AddButton(__instance);
	}

	private static void UpdateToolTip(CodeWindow window) {
		Transform? btn = window.transform.Find(BUTTON_NAME);

		if (btn == null) return;

		if (!btn.TryGetComponent(out ColoredButton cb)) return;

		// Update tooltip
		cb.tooltipDescription = $"Copy '{window.fileName}' to the clipboard";
	}

	private static void AddButton(CodeWindow window) {
		Transform? template = window.transform.Find("MinimizeButton");

		if (template == null) {
			Log.Warning("Couldn't find the button to copy.");
			return;
		}

		Transform option = UnityEngine.Object.Instantiate(template, template.parent);
		option.name = BUTTON_NAME;
		option.SetAsLastSibling();
		option.localPosition -= new Vector3(50, 0, 0);

		// Add description
		UpdateToolTip(window);

		if (option.TryGetComponent(out Button btn)) btn.onClick = new Button.ButtonClickedEvent();

		if (option.TryGetComponent(out ColoredButton coloredBtn)) {
			coloredBtn.OnClick.AddListener(() =>
				{
					string? code = window.CodeInput.text;
					GUIUtility.systemCopyBuffer = $"```py\n{code}\n```";

					MainSim.Inst.warningPopup.ShowPopup(
						$"'{window.fileName}' was copied successfully!", [
							new WarningPopup.ButtonData("Ok", MainSim.Inst.warningPopup.Close),
						]
					);
				}
			);
		}

		Transform? image = option.Find("Image");
		
		if (image != null && image.TryGetComponent(out Image icon)) {
			Texture2D? texture = Asset.GetTexture("CopyCode.Resources.copy-icon.png");

			if (texture != null) {
				icon.sprite = Sprite.Create(
					texture,
					new Rect(
						Vector2.zero,
						Vector2.one * 64
					), Vector2.zero
				);
			} else
				icon.sprite = null;
		}
	}
}