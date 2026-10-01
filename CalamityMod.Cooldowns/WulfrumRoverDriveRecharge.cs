using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class WulfrumRoverDriveRecharge : CooldownHandler
{
	private static Color ringColorLerpStart;

	private static Color ringColorLerpEnd;

	public new static string ID => "WulfrumRoverDriveRecharge";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/WulfrumRoverDrive";

	public override string OutlineTexture => "CalamityMod/Cooldowns/WulfrumRoverDriveOutline";

	public override string OverlayTexture => "CalamityMod/Cooldowns/WulfrumRoverDriveOverlay";

	public override bool SavedWithPlayer => false;

	public override bool PersistsThroughDeath => false;

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(194, 255, 67);
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

	public override SoundStyle? EndSound => RoverDrive.ActivationSound;

	public override bool ShouldPlayEndSound => instance.player.Calamity().roverDrive;

	public override void Tick()
	{
		if (instance.player.whoAmI == Main.myPlayer)
		{
			instance.player.Calamity().playedRoverDriveShieldSound = false;
		}
	}

	public override void OnCompleted()
	{
		if (instance.player.whoAmI == Main.myPlayer)
		{
			CalamityPlayer modPlayer = instance.player.Calamity();
			if (modPlayer.RoverDriveShieldDurability <= 0)
			{
				modPlayer.RoverDriveShieldDurability = 1;
			}
		}
	}

	static WulfrumRoverDriveRecharge()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		ringColorLerpStart = new Color(194, 255, 57);
		ringColorLerpEnd = new Color(92, 187, 99);
	}
}
