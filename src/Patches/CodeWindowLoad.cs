using HarmonyLib;

namespace CopyCode.Patches;

[HarmonyPatch(typeof(CodeWindow), nameof(CodeWindow.Load), [typeof(string)])]
public static class CodeWindowLoad
{
    // ReSharper disable once InconsistentNaming
    public static void Postfix(CodeWindow __instance)
    {
        CopyWindow.AddCopyOption(__instance);
    }
}