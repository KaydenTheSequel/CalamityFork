using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI;

public class AcidRainUI : InvasionProgressUI
{
	public override bool IsActive
	{
		get
		{
			if (AcidRainEvent.AcidRainEventIsOngoing)
			{
				return Main.LocalPlayer.Calamity().ZoneSulphur;
			}
			return false;
		}
	}

	public override float CompletionRatio => 1f - AcidRainEvent.AcidRainCompletionRatio;

	public override string InvasionName => CalamityUtils.GetTextValue("Events.AcidRain");

	public override Color InvasionBarColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return AcidRainEvent.TextColor;
		}
	}

	public override Texture2D IconTexture => ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/AcidRainIcon", (AssetRequestMode)2).Value;
}
