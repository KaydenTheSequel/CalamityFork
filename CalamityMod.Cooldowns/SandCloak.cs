using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class SandCloak : CooldownHandler
{
	public new static string ID => "SandCloak";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/SandCloak";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(209, 176, 114);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return new Color(100, 64, 44);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(132, 95, 54);
		}
	}
}
