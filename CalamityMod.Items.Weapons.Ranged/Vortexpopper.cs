using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Vortexpopper : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 92;
		base.Item.height = 36;
		base.Item.damage = 30;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 21;
		base.Item.useAnimation = 21;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.25f;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = SoundID.Item95;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 50f;
		base.Item.shoot = 444;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		float bulletSpeed = base.Item.shootSpeed;
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
		float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
		if (player.gravDir == -1f)
		{
			mouseYDist = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - realPlayerPos.Y;
		}
		float mouseDistance = (float)Math.Sqrt(mouseXDist * mouseXDist + mouseYDist * mouseYDist);
		if ((float.IsNaN(mouseXDist) && float.IsNaN(mouseYDist)) || (mouseXDist == 0f && mouseYDist == 0f))
		{
			mouseXDist = player.direction;
			mouseYDist = 0f;
			mouseDistance = bulletSpeed;
		}
		else
		{
			mouseDistance = bulletSpeed / mouseDistance;
		}
		mouseXDist *= mouseDistance;
		mouseYDist *= mouseDistance;
		Vector2 bulletVel = Vector2.Normalize(new Vector2(mouseXDist, mouseYDist)) * 40f * base.Item.scale;
		Collision.CanHit(realPlayerPos, 0, 0, realPlayerPos + bulletVel, 0, 0);
		float ai = Utils.ToRotation(new Vector2(mouseXDist, mouseYDist));
		float twoThirdsPi = (float)Math.PI * 2f / 3f;
		for (int i = 0; i < 6; i++)
		{
			float randVelMult = (float)Main.rand.NextDouble() * 0.2f + 0.05f;
			Vector2 bulletSpawnVelocity = Utils.RotatedBy(new Vector2(mouseXDist, mouseYDist), (double)(twoThirdsPi * (float)Main.rand.NextDouble() - twoThirdsPi / 2f), default(Vector2)) * randVelMult;
			int initialBullet = Projectile.NewProjectile(source, position.X, position.Y, bulletSpawnVelocity.X, bulletSpawnVelocity.Y, 444, damage, knockback, player.whoAmI, ai);
			Main.projectile[initialBullet].localAI[0] = type;
			Main.projectile[initialBullet].localAI[1] = 12f;
		}
		for (int j = 0; j < 6; j++)
		{
			((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-800, 801);
			realPlayerPos.Y -= 100 * j;
			mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
			mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
			if (mouseYDist < 0f)
			{
				mouseYDist *= -1f;
			}
			if (mouseYDist < 20f)
			{
				mouseYDist = 20f;
			}
			mouseDistance = (float)Math.Sqrt(mouseXDist * mouseXDist + mouseYDist * mouseYDist);
			mouseDistance = bulletSpeed / mouseDistance;
			mouseXDist *= mouseDistance;
			mouseYDist *= mouseDistance;
			float speedX4 = mouseXDist + (float)Main.rand.Next(-1000, 1001) * 0.02f;
			float speedY5 = mouseYDist + (float)Main.rand.Next(-1000, 1001) * 0.02f;
			int extraBullet = Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX4, speedY5, 444, damage, knockback, player.whoAmI, ai);
			Main.projectile[extraBullet].localAI[0] = type;
			Main.projectile[extraBullet].localAI[1] = 12f;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2797).AddIngredient(3456, 12).AddTile(412)
			.Register();
	}
}
