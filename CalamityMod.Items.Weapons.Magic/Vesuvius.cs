using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Vesuvius : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 62;
		base.Item.damage = 50;
		base.Item.mana = 7;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item88;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 20f;
		base.Item.shoot = ModContent.ProjectileType<AsteroidMolten>();
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.altFunctionUse == 2)
		{
			mult *= 1.5f;
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 1.33f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			int meteorAmt = Main.rand.Next(2, 4);
			for (int i = 0; i < meteorAmt; i++)
			{
				float SpeedX = velocity.X + (float)Main.rand.Next(-30, 31) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-30, 31) * 0.05f;
				float ai0 = Main.rand.Next(6);
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, ai0, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
			}
			return false;
		}
		float meteorSpeed = base.Item.shootSpeed;
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
			mouseDistance = meteorSpeed;
		}
		else
		{
			mouseDistance = meteorSpeed / mouseDistance;
		}
		for (int j = 0; j < 4; j++)
		{
			((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			realPlayerPos.Y -= 100 * j;
			mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X + (float)Main.rand.Next(-40, 41) * 0.03f;
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
			mouseDistance = meteorSpeed / mouseDistance;
			mouseXDist *= mouseDistance;
			mouseYDist *= mouseDistance;
			float meteorSpawnXOffset = mouseXDist;
			float meteorSpawnYOffset = mouseYDist + (float)Main.rand.Next(-40, 41) * 0.02f;
			float ai1 = Main.rand.Next(6);
			Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, meteorSpawnXOffset * 0.75f, meteorSpawnYOffset * 0.75f, type, damage, knockback, player.whoAmI, ai1, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
		}
		return false;
	}
}
