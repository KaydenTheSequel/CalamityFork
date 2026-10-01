using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class IcicleStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 42;
		base.Item.damage = 10;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 6;
		base.Item.useTime = 7;
		base.Item.useAnimation = 14;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.UseSound = SoundID.Item8;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<IcicleStaffProj>();
		base.Item.shootSpeed = 11f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		int i = Main.myPlayer;
		float icicleSpeed = ((Vector2)(ref velocity)).Length();
		float playerKnockback = knockback;
		playerKnockback = player.GetWeaponKnockback(base.Item, playerKnockback);
		player.itemTime = base.Item.useTime;
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float mouseXDist = (float)Main.mouseX - Main.screenPosition.X - realPlayerPos.X;
		float mouseYDist = (float)Main.mouseY - Main.screenPosition.Y - realPlayerPos.Y;
		if (player.gravDir == -1f)
		{
			mouseYDist = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - realPlayerPos.Y;
		}
		float mouseDistance = (float)Math.Sqrt(mouseXDist * mouseXDist + mouseYDist * mouseYDist);
		if ((float.IsNaN(mouseXDist) && float.IsNaN(mouseYDist)) || (mouseXDist == 0f && mouseYDist == 0f))
		{
			mouseXDist = player.direction;
			mouseYDist = 0f;
			mouseDistance = icicleSpeed;
		}
		else
		{
			mouseDistance = icicleSpeed / mouseDistance;
		}
		((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
		realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
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
		mouseDistance = icicleSpeed / mouseDistance;
		mouseXDist *= mouseDistance;
		mouseYDist *= mouseDistance;
		float speedX4 = mouseXDist;
		float speedY5 = mouseYDist + (float)Main.rand.Next(-40, 41) * 0.02f;
		Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX4, speedY5, ModContent.ProjectileType<IcicleStaffProj>(), damage, playerKnockback, i, 0f, Main.rand.Next(10));
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyIceBlock", 25).AddIngredient(2358, 3).AddTile(16)
			.Register();
	}
}
