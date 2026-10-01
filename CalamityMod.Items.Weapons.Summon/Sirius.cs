using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class Sirius : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 1f;
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 62);
		base.Item.damage = 90;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.mana = 10;
		base.Item.knockBack = 10f;
		base.Item.buffType = ModContent.BuffType<SiriusBuff>();
		base.Item.shoot = ModContent.ProjectileType<SiriusMinion>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item44;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
	}

	public override bool CanUseItem(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.ownedProjectileCounts[type] > 0)
		{
			Projectile sirius = null;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile item = enumerator.Current;
				if (item.type == type && item.owner == player.whoAmI && item.type == type)
				{
					sirius = item;
				}
			}
			if (sirius != null)
			{
				sirius.ai[1]++;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<VengefulSunStaff>().AddIngredient<Lumenyl>(5).AddIngredient<RuinousSoul>(2)
			.AddIngredient<ExodiumCluster>(12)
			.AddTile(134)
			.Register();
	}
}
