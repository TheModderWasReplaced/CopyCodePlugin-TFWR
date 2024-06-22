using BepInEx;
using ModHelper.API;

namespace CopyCode;

[BepInPlugin("org.warpersan.plugins.copycode", "Copy Code", "1.0.0.0")]
public class CopyCodePlugin : FarmPlugin
{
    /// <inheritdoc />
    public override string Author => "WarperSan";

    /// <inheritdoc />
    protected override void OnAwake()
    {
        Harmony.PatchAll();
    }
}
