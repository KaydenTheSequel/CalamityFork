using CalamityMod.CalPlayer;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class ProfanedSoulShieldRecharge : CooldownHandler
{
	private static Color ringColorLerpStart;

	private static Color ringColorLerpEnd;

	public new static string ID => "ProfanedSoulShieldRecharge";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/ProfanedSoulShieldRecharge";

	public override string OutlineTexture => "CalamityMod/Cooldowns/ProfanedSoulShieldOutline";

	public override string OverlayTexture => "CalamityMod/Cooldowns/ProfanedSoulShieldOverlay";

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

	public override SoundStyle? EndSound => Providence.BurnStartSound;

	public override bool ShouldPlayEndSound => instance.player.Calamity().pSoulArtifact;

	public override void Tick()
	{
		if (instance.player.whoAmI == Main.myPlayer)
		{
			instance.player.Calamity().playedProfanedSoulShieldSound = false;
		}
	}

	public override void OnCompleted()
	{
		if (instance.player.whoAmI == Main.myPlayer)
		{
			CalamityPlayer modPlayer = instance.player.Calamity();
			if (modPlayer.pSoulShieldDurability <= 0)
			{
				modPlayer.pSoulShieldDurability = 1;
			}
		}
	}

	static ProfanedSoulShieldRecharge()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		ringColorLerpStart = new Color(217, 159, 78);
		ringColorLerpEnd = new Color(214, 185, 144);
	}
}
