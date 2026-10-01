using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class BloodBath : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 50;
		base.Item.damage = 24;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 15;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.75f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item21;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BloodBeam>();
		base.Item.shootSpeed = 9f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		float bloodSpeed = base.Item.shootSpeed;
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float mouseXPos = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
		float mouseYPos = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
		if (player.gravDir == -1f)
		{
			mouseYPos = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - realPlayerPos.Y;
		}
		float mouseDistance = (float)Math.Sqrt(mouseXPos * mouseXPos + mouseYPos * mouseYPos);
		if ((float.IsNaN(mouseXPos) && float.IsNaN(mouseYPos)) || (mouseXPos == 0f && mouseYPos == 0f))
		{
			mouseXPos = player.direction;
			mouseYPos = 0f;
			mouseDistance = bloodSpeed;
		}
		else
		{
			mouseDistance = bloodSpeed / mouseDistance;
		}
		int bloodAmt = 2;
		if (Main.rand.NextBool(3))
		{
			bloodAmt++;
		}
		for (int i = 0; i < bloodAmt; i++)
		{
			((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			realPlayerPos.Y -= 100 * i;
			mouseXPos = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
			mouseYPos = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
			if (mouseYPos < 0f)
			{
				mouseYPos *= -1f;
			}
			if (mouseYPos < 20f)
			{
				mouseYPos = 20f;
			}
			mouseDistance = (float)Math.Sqrt(mouseXPos * mouseXPos + mouseYPos * mouseYPos);
			mouseDistance = bloodSpeed / mouseDistance;
			mouseXPos *= mouseDistance;
			mouseYPos *= mouseDistance;
			float speedX4 = mouseXPos + (float)Main.rand.Next(-30, 31) * 0.02f;
			float speedY5 = mouseYPos + (float)Main.rand.Next(-30, 31) * 0.02f;
			Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX4, speedY5, type, damage, knockback, player.whoAmI, 0f, Main.rand.Next(15));
		}
		return false;
	}
}
