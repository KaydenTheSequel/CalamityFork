using System.Linq;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class SunSpiritStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 48;
		base.Item.damage = 25;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.15f;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.UseSound = SoundID.Item44;
		base.Item.buffType = ModContent.BuffType<SolarSpirit>();
		base.Item.shoot = ModContent.ProjectileType<SunSpiritMinion>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool CanUseItem(Player player)
	{
		float minionSlotsAvailable = player.maxMinions;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile item = enumerator.Current;
			if (item.owner == player.whoAmI)
			{
				minionSlotsAvailable -= item.minionSlots;
			}
		}
		return minionSlotsAvailable >= 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.ownedProjectileCounts[type] > 0)
		{
			Projectile projectile = Main.projectile.First((Projectile x) => x.active && x.type == type && x.owner == player.whoAmI);
			projectile.ai[0]++;
			projectile.netUpdate = true;
			return false;
		}
		player.AddBuff(base.Item.buffType, 2);
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(607, 20).AddIngredient<StormlionMandible>(2).AddTile(16)
			.Register();
	}
}
