using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class EndoHydraStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 60;
		base.Item.damage = 190;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.mana = 10;
		base.Item.knockBack = 3f;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item60;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<EndoHydraBuff>();
		base.Item.shoot = ModContent.ProjectileType<EndoHydraBody>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		bool bodyExists = false;
		int bodyIndex = -1;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == type && p.owner == player.whoAmI)
			{
				bodyIndex = p.whoAmI;
				bodyExists = true;
				break;
			}
		}
		if (bodyExists)
		{
			Projectile.NewProjectileDirect(source, player.Center, Main.rand.NextVector2Unit(), ModContent.ProjectileType<EndoHydraHead>(), damage, knockback, player.whoAmI, bodyIndex).originalDamage = base.Item.damage;
		}
		else
		{
			bodyIndex = Projectile.NewProjectile(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
			if (Main.projectile.IndexInRange(bodyIndex))
			{
				Main.projectile[bodyIndex].originalDamage = base.Item.damage;
			}
			Projectile.NewProjectileDirect(source, player.Center, Main.rand.NextVector2Unit(), ModContent.ProjectileType<EndoHydraHead>(), damage, knockback, player.whoAmI, bodyIndex).originalDamage = base.Item.damage;
			for (int i = 0; i < 72; i++)
			{
				Dust dust = Dust.NewDustPerfect(Main.projectile[bodyIndex].Center, 113);
				dust.velocity = ((float)Math.PI * 2f * Vector2.Dot(((float)i / 72f * ((float)Math.PI * 2f)).ToRotationVector2(), player.velocity.SafeNormalize(Vector2.UnitY).RotatedBy((float)i / 72f * ((float)Math.PI * -2f)))).ToRotationVector2();
				dust.velocity = dust.velocity.RotatedBy((float)i / 36f * ((float)Math.PI * 2f)) * 8f;
				dust.noGravity = true;
				dust.scale = 1.9f;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1572).AddIngredient<CosmiliteBar>(8).AddIngredient<EndothermicEnergy>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
