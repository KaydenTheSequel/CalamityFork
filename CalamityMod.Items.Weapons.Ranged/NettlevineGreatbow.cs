using System;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "NettlelineGreatbow" })]
public class NettlevineGreatbow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 64;
		base.Item.damage = 73;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 18;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 16f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float tenthPi = (float)Math.PI / 10f;
		Vector2 arrowVel = velocity;
		((Vector2)(ref arrowVel)).Normalize();
		arrowVel *= 40f;
		bool arrowHitsTiles = Collision.CanHit(vector2, 0, 0, vector2 + arrowVel, 0, 0);
		for (int i = 0; i < 4; i++)
		{
			float piOffsetAmt = (float)i - 1.5f;
			Vector2 offsetSpawn = arrowVel.RotatedBy(tenthPi * piOffsetAmt);
			if (!arrowHitsTiles)
			{
				offsetSpawn -= arrowVel;
			}
			int arrowSpawn = Projectile.NewProjectile(source, vector2.X + offsetSpawn.X, vector2.Y + offsetSpawn.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			Main.projectile[arrowSpawn].noDropItem = true;
		}
		for (int j = 0; j < 2; j++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-40, 41) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-40, 41) * 0.05f;
			switch (Main.rand.Next(2))
			{
			case 0:
				type = 282;
				break;
			case 1:
				type = 225;
				break;
			}
			int index = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			Main.projectile[index].noDropItem = true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(12).AddTile(134).Register();
	}
}
