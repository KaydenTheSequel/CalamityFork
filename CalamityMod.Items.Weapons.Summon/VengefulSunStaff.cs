using System.Linq;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "SunGodStaff" })]
public class VengefulSunStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 72;
		base.Item.damage = 35;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.25f;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.UseSound = SoundID.Item44;
		base.Item.buffType = ModContent.BuffType<SolarGodSpiritBuff>();
		base.Item.shoot = ModContent.ProjectileType<VengefulSunSpiritMinion>();
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
		CreateRecipe().AddIngredient<SunSpiritStaff>().AddIngredient(900).AddIngredient(547, 3)
			.AddIngredient(548, 3)
			.AddIngredient(549, 3)
			.AddTile(134)
			.Register();
	}
}
