using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class PermafrostConcoction : CooldownHandler
{
	public new static string ID => "PermafrostConcoction";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/PermafrostConcoction";

	public override Color OutlineColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0, 218, 255);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(144, 184, 205);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(232, 246, 254);
		}
	}
}
