using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Plaguebringer;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class PlaguebringerVisor : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.15f;

	public static int PlagueDashDamage = 50;

	public static float PlagueDashKnockback = 3f;

	public static int PlagueDashIFrames = 12;

	public static int BeeMinionDamage = 25;

	public static int BeePlagueDuration = CalamityUtils.SecondsToFrames(5);

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 9;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<PlaguebringerCarapace>())
		{
			return legs.type == ModContent.ItemType<PlaguebringerPistons>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalizedValue("SetBonus");
		player.Calamity().plaguebringerPatronSet = true;
		player.Calamity().DashID = PlaguebringerArmorDash.ID;
		player.dashType = 0;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<LilPlaguebringerBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<LilPlaguebringerBuff>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<PlaguebringerSummon>()] < 1)
			{
				int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(BeeMinionDamage);
				int p = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<PlaguebringerSummon>(), damage, 0f, player.whoAmI);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = BeeMinionDamage;
				}
			}
		}
		Lighting.AddLight(player.Center, 0f, 0.39f, 0.24f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2361).AddIngredient<InfectedArmorPlating>(4).AddIngredient<PlagueCellCanister>(4)
			.AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<PlaguebringerCarapace>())
			.Register();
	}
}
