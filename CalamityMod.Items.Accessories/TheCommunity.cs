using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class TheCommunity : ModItem, ILocalizedModType, IModType
{
	private static readonly int TotalCountedBosses = 42;

	public const float DamageMultiplier = 0.5f;

	public const float CritMultiplier = 25f;

	public const float HealthMultiplier = 50f;

	public const float DRMultiplier = 0.25f;

	public const float DefenseMultiplier = 50f;

	public const float RegenMultiplier = 10f;

	public const float SpeedMultiplier = 0.5f;

	public const float FlightMultiplier = 1f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 10));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 64;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 7;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().community = true;
	}

	public override bool CanEquipAccessory(Player player, int slot, bool modded)
	{
		return !player.Calamity().shatteredCommunity;
	}

	internal static float CalculatePower(bool killsOnly = false)
	{
		float bossDownedRatio = (float)(0 + NPC.downedSlimeKing.ToInt() + DownedBossSystem.downedDesertScourge.ToInt() + NPC.downedBoss1.ToInt() + DownedBossSystem.downedCrabulon.ToInt() + NPC.downedBoss2.ToInt() + (DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator).ToInt() + NPC.downedQueenBee.ToInt() + NPC.downedBoss3.ToInt() + NPC.downedDeerclops.ToInt() + DownedBossSystem.downedSlimeGod.ToInt() + Main.hardMode.ToInt() + NPC.downedQueenSlime.ToInt() + DownedBossSystem.downedCryogen.ToInt() + NPC.downedMechBoss1.ToInt() + DownedBossSystem.downedAquaticScourge.ToInt() + NPC.downedMechBoss2.ToInt() + DownedBossSystem.downedBrimstoneElemental.ToInt() + NPC.downedMechBoss3.ToInt() + DownedBossSystem.downedCalamitasClone.ToInt() + NPC.downedPlantBoss.ToInt() + DownedBossSystem.downedLeviathan.ToInt() + DownedBossSystem.downedAstrumAureus.ToInt() + NPC.downedGolemBoss.ToInt() + DownedBossSystem.downedPlaguebringer.ToInt() + NPC.downedFishron.ToInt() + NPC.downedEmpressOfLight.ToInt() + DownedBossSystem.downedRavager.ToInt() + NPC.downedAncientCultist.ToInt() + DownedBossSystem.downedAstrumDeus.ToInt() + NPC.downedMoonlord.ToInt() + DownedBossSystem.downedGuardians.ToInt() + DownedBossSystem.downedDragonfolly.ToInt() + DownedBossSystem.downedProvidence.ToInt() + DownedBossSystem.downedCeaselessVoid.ToInt() + DownedBossSystem.downedStormWeaver.ToInt() + DownedBossSystem.downedSignus.ToInt() + DownedBossSystem.downedPolterghast.ToInt() + DownedBossSystem.downedBoomerDuke.ToInt() + DownedBossSystem.downedDoG.ToInt() + DownedBossSystem.downedYharon.ToInt() + DownedBossSystem.downedExoMechs.ToInt() + DownedBossSystem.downedCalamitas.ToInt()) / (float)TotalCountedBosses;
		if (!killsOnly)
		{
			return MathHelper.Lerp(0.05f, 0.2f, bossDownedRatio);
		}
		return bossDownedRatio;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine ThankYouTooltip = list.FirstOrDefault((TooltipLine x) => x.Name == "Tooltip2" && x.Mod == "Terraria");
		if (ThankYouTooltip != null)
		{
			ThankYouTooltip.OverrideColor = Main.DiscoColor;
		}
		float power = CalculatePower();
		string statList = this.GetLocalization("StatsList").Format((0.5f * power * 100f).ToString("N1"), (int)(25f * power), (int)(50f * power), (0.25f * power * 100f).ToString("N2"), (int)(50f * power), (0.5f * (1f + (float)(int)(10f * power))).ToString("n1"), (0.5f * power * 100f).ToString("N1"), (1f * power * 100f).ToString("N1"), (CalculatePower(killsOnly: true) * 100f).ToString("N0"));
		list.FindAndReplace("[STATS]", statList);
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.7f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
