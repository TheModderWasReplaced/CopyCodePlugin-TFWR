using HarmonyLib;

namespace CopyCode.Patches;

[HarmonyPatch(typeof(CodeWindow), nameof(CodeWindow.OnNameTextEdited), [])]
public static class CodeWindowNameChanged
{
    // ReSharper disable once InconsistentNaming
    public static void Postfix(CodeWindow __instance)
    {
        CopyWindow.UpdateToolTip(__instance);
    }
}