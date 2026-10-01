using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Cooldowns;

public class DivineBless : CooldownHandler
{
	public new static string ID => "DivineBless";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/DivineBless";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(233, 192, 68);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(177, 105, 33);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(233, 192, 68);
		}
	}

	public override void OnCompleted()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (instance.player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(new EntitySource_Parent(instance.player), instance.player.Center, Vector2.Zero, ModContent.ProjectileType<AllianceTriangle>(), 0, 0f, instance.player.whoAmI);
		}
	}
}
