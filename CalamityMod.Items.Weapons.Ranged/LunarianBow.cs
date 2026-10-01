using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class LunarianBow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 62;
		base.Item.damage = 27;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item75;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<LunarBolt>();
		base.Item.shootSpeed = 8f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo spawnSource, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		Vector2 source = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float piOver10 = (float)Math.PI / 10f;
		int projAmt = 2;
		((Vector2)(ref velocity)).Normalize();
		velocity *= 15f;
		bool canHit = Collision.CanHit(source, 0, 0, source + velocity, 0, 0);
		for (int i = 0; i < projAmt; i++)
		{
			float offsetAmt = (float)i - ((float)projAmt - 1f) / 2f;
			Vector2 offset = velocity.RotatedBy(piOver10 * offsetAmt);
			if (!canHit)
			{
				offset -= velocity;
			}
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				Projectile.NewProjectile(spawnSource, source + offset, velocity, ModContent.ProjectileType<LunarBolt>(), damage, knockback, player.whoAmI);
				continue;
			}
			int proj = Projectile.NewProjectile(spawnSource, source + offset, velocity, type, damage, knockback, player.whoAmI);
			Main.projectile[proj].noDropItem = true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(44).AddIngredient(2888).AddIngredient(120)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.Register();
		CreateRecipe().AddIngredient(796).AddIngredient(2888).AddIngredient(120)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.Register();
	}
}
