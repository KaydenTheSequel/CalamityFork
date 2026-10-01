using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.ExtraJumps;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Statigel;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "StatigelHood" })]
public class StatigelHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.05f;

	public static float SetBonusSummonDamageBoost = 0.15f;

	public static int SlimeDamage = 18;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 4;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<StatigelArmor>())
		{
			return legs.type == ModContent.ItemType<StatigelGreaves>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusSummonDamageBoost.ToPercent(), StatigelArmor.SetBonusJumpSpeedBoost.ToJumpSpeedPercent());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.statigelSet = true;
		calamityPlayer.slimeGod = true;
		player.GetJumpState<StatigelJump>().Enable();
		Player.jumpHeight += (int)(StatigelArmor.SetBonusJumpHeightPercentBoost * 15f);
		player.jumpSpeedBoost += StatigelArmor.SetBonusJumpSpeedBoost;
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_Accessory(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<BabySlimeGodBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<BabySlimeGodBuff>(), 3600);
			}
			int minionID = -1;
			int minionDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(SlimeDamage);
			if (WorldGen.crimson && player.ownedProjectileCounts[ModContent.ProjectileType<CrimsonSlimeGodMinion>()] < 1)
			{
				minionID = Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<CrimsonSlimeGodMinion>(), minionDamage, 0f, Main.myPlayer);
			}
			else if (!WorldGen.crimson && player.ownedProjectileCounts[ModContent.ProjectileType<CorruptionSlimeGodMinion>()] < 1)
			{
				minionID = Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<CorruptionSlimeGodMinion>(), minionDamage, 0f, Main.myPlayer);
			}
			if (Main.projectile.IndexInRange(minionID))
			{
				Main.projectile[minionID].originalDamage = SlimeDamage;
			}
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(5).AddIngredient<BlightedGel>(5).AddTile(220)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<StatigelHeadRogue>())
			.Register();
	}
}
