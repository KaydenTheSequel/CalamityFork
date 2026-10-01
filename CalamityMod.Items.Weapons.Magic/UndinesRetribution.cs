using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class UndinesRetribution : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 64;
		base.Item.damage = 44;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useTime = 18;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item66;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<UndinesRetributionSpear>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		float spearSpeed = base.Item.shootSpeed;
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
		}
		else
		{
			mouseDistance = spearSpeed / mouseDistance;
		}
		for (int i = 0; i < 3; i++)
		{
			((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(51) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y + 600f);
			realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-50, 51);
			realPlayerPos.Y += 100 * i;
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
			mouseDistance = spearSpeed / mouseDistance;
			mouseXDist *= mouseDistance;
			mouseYDist *= mouseDistance;
			float speedX6 = mouseXDist + (float)Main.rand.Next(-60, 61) * 0.02f;
			float speedY7 = mouseYDist + (float)Main.rand.Next(-60, 61) * 0.02f;
			float ai1 = Main.rand.NextFloat() + 0.5f;
			Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX6, 0f - speedY7, type, damage, knockback, player.whoAmI, 0f, ai1);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Voidstone>(30).AddIngredient<DepthCells>(30).AddTile(134)
			.Register();
	}
}
