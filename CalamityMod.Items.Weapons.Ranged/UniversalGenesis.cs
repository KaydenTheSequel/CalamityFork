using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class UniversalGenesis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 158;
		base.Item.height = 60;
		base.Item.damage = 180;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 26);
		base.Item.knockBack = 6.5f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.shoot = 14;
		base.Item.shootSpeed = 20f;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item38;
		base.Item.useStyle = 5;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-50f, -8f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = velocity.SafeNormalize(Vector2.UnitX * (float)player.direction);
		Vector2 gunTip = position + shootDirection * base.Item.scale * 100f;
		gunTip.Y -= 10f;
		float tightness = 1f;
		if (type == 14)
		{
			type = ModContent.ProjectileType<UniversalGenesisStarcaller>();
		}
		for (float i = (0f - tightness) * 5f; i <= tightness * 5f; i += tightness * 2f)
		{
			Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(i));
			Projectile.NewProjectile(source, gunTip, perturbedSpeed, type, damage, knockback, player.whoAmI);
		}
		float speed = base.Item.shootSpeed;
		int starAmt = 5;
		int starDmg = (int)((float)damage * 0.5f);
		Vector2 spawnPos = default(Vector2);
		Vector2 vel = default(Vector2);
		for (int j = 0; j < starAmt; j++)
		{
			((Vector2)(ref spawnPos))._002Ector(player.Center.X + (float)(Main.rand.Next(201) * -player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			spawnPos.X = (spawnPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			spawnPos.Y -= 100 + j;
			((Vector2)(ref vel))._002Ector((float)Main.mouseX + Main.screenPosition.X - spawnPos.X, MathF.Abs((float)Main.mouseY + Main.screenPosition.Y - spawnPos.Y));
			if (vel.Y < 20f)
			{
				vel.Y = 20f;
			}
			vel = vel.SafeNormalize(Vector2.UnitY) * speed;
			vel.X += Main.rand.NextFloat(-0.6f, 0.6f);
			vel.Y += Main.rand.NextFloat(-0.6f, 0.6f);
			int star = Projectile.NewProjectile(source, spawnPos, vel, ModContent.ProjectileType<UniversalGenesisStar>(), starDmg, knockback, player.whoAmI, j, 1f);
			Main.projectile[star].extraUpdates = 2;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ConferenceCall>().AddIngredient(532, 3).AddIngredient<CosmiliteBar>(5)
			.AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
