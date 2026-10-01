using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityMod.Balancing;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

public abstract class BaseWings : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories.Wings";

	public virtual float BonusAscentWhileFalling => 0.5f;

	public virtual float BonusAscentWhileRising => 0.1f;

	public virtual float RisingSpeedThreshold => 0.5f;

	public virtual float MaxAscentSpeed => 1.5f;

	public virtual float BaseAscent => 0.1f;

	public override void SetDefaults()
	{
		base.Item.accessory = true;
	}

	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		if (base.Item.wingSlot != -1)
		{
			ascentWhenFalling = BonusAscentWhileFalling;
			ascentWhenRising = BonusAscentWhileRising;
			maxCanAscendMultiplier = RisingSpeedThreshold;
			maxAscentMultiplier = MaxAscentSpeed;
			constantAscend = BaseAscent;
			AdditionalFlightMovement(player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
		}
	}

	public virtual void AdditionalFlightMovement(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		if (base.Item.wingSlot == -1)
		{
			return;
		}
		WingStats stats = default(WingStats);
		stats = ((base.Item.type == ModContent.ItemType<MoonWalkers>()) ? ArmorIDs.Wing.Sets.Stats[MoonWalkers.wingSlot] : ((base.Item.type == ModContent.ItemType<VoidStriders>()) ? ArmorIDs.Wing.Sets.Stats[VoidStriders.wingSlot] : ((base.Item.type != ModContent.ItemType<SeraphTracers>()) ? ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] : ArmorIDs.Wing.Sets.Stats[SeraphTracers.wingSlot])));
		int time = stats.FlyTime;
		float run = stats.AccRunSpeedOverride;
		float rAcc = stats.AccRunAccelerationMult * 0.08f;
		bool hover = stats.HasDownHoverStats;
		float hSpeed = stats.DownHoverSpeedOverride;
		float hAcc = stats.DownHoverAccelerationMult * 0.08f;
		float baseJumpSpeed = (CalamityServerConfig.Instance.FasterJumpSpeed ? BalancingConstants.ConfigBoostedBaseJumpSpeed : 5.01f) + 1f;
		StringBuilder sb = new StringBuilder(512);
		sb.Append('\n');
		sb.Append(CalamityUtils.GetText("Common.WingStats").Format(time.FramesToSeconds(), run.ToMph(), (MaxAscentSpeed * baseJumpSpeed).ToMph()));
		sb.Append('\n');
		if (Main.keyState.PressingShift())
		{
			sb.Append(CalamityUtils.GetText("Common.WingStatsAcceleration").Format(rAcc.ToMphps(), BaseAscent.ToMphps(), (BaseAscent + BonusAscentWhileRising).ToMphps(), (RisingSpeedThreshold * baseJumpSpeed).ToMph(), (BaseAscent + BonusAscentWhileFalling).ToMphps()));
			if (hover)
			{
				sb.Append('\n');
				sb.Append(CalamityUtils.GetText("Common.WingStatsHover").Format(hSpeed.ToMph(), hAcc.ToMphps()));
			}
		}
		else
		{
			StringBuilder stringBuilder = sb;
			StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(11, 1, stringBuilder);
			handler.AppendLiteral("[c/B8B8B8:");
			handler.AppendFormatted(CalamityUtils.GetTextValue("UI.HoldShiftTooltipExtensionIndicator"));
			handler.AppendLiteral("]");
			stringBuilder.Append(ref handler);
		}
		TooltipLine wingTooltip = list.FirstOrDefault((TooltipLine x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
		if (wingTooltip != null)
		{
			wingTooltip.Text += sb.ToString();
		}
	}
}
