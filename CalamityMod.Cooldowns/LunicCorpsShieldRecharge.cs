using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.LunicCorps;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class LunicCorpsShieldRecharge : CooldownHandler
{
	private static Color ringColorLerpStart;

	private static Color ringColorLerpEnd;

	public new static string ID => "MasterChefShieldRecharge";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/LunicCorpsShieldRecharge";

	public override string OutlineTexture => "CalamityMod/Cooldowns/LunicCorpsShieldOutline";

	public override string OverlayTexture => "CalamityMod/Cooldowns/LunicCorpsShieldOverlay";

	public override bool SavedWithPlayer => false;

	public override bool PersistsThroughDeath => false;

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(57, 195, 237);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(ringColorLerpStart, ringColorLerpEnd, instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(ringColorLerpStart, ringColorLerpEnd, instance.Completion);
		}
	}

	public override SoundStyle? EndSound => LunicCorpsHelmet.ActivationSound;

	public override bool ShouldPlayEndSound => instance.player.Calamity().lunicCorpsSet;

	public override void Tick()
	{
		if (instance.player.whoAmI == Main.myPlayer)
		{
			instance.player.Calamity().playedLunicCorpsShieldSound = false;
		}
	}

	public override void OnCompleted()
	{
		if (instance.player.whoAmI == Main.myPlayer)
		{
			CalamityPlayer modPlayer = instance.player.Calamity();
			if (modPlayer.LunicCorpsShieldDurability <= 0)
			{
				modPlayer.LunicCorpsShieldDurability = 1;
			}
		}
	}

	static LunicCorpsShieldRecharge()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		ringColorLerpStart = new Color(179, 212, 242);
		ringColorLerpEnd = new Color(113, 178, 222);
	}
}
