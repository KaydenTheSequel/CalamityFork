using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Swordsplosion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 90;
		base.Item.damage = 48;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 30;
		base.Item.useTime = 30;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 7f;
		base.Item.UseSound = SoundID.Item60;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<SwordsplosionBlue>();
		base.Item.shootSpeed = 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		switch (Main.rand.Next(4))
		{
		case 0:
			type = ModContent.ProjectileType<SwordsplosionBlue>();
			break;
		case 1:
			type = ModContent.ProjectileType<SwordsplosionGreen>();
			break;
		case 2:
			type = ModContent.ProjectileType<SwordsplosionPurple>();
			break;
		case 3:
			type = ModContent.ProjectileType<SwordsplosionRed>();
			break;
		}
		float projSpeed = Main.rand.Next(22, 30);
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float mouseXDist = (float)Main.mouseX + Main.screenPosition.X + realPlayerPos.X;
		float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y + realPlayerPos.Y;
		if (player.gravDir == -1f)
		{
			mouseYDist = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + realPlayerPos.Y;
		}
		float mouseDistance = (float)Math.Sqrt(mouseXDist * mouseXDist + mouseYDist * mouseYDist);
		if ((float.IsNaN(mouseXDist) && float.IsNaN(mouseYDist)) || (mouseXDist == 0f && mouseYDist == 0f))
		{
			mouseXDist = player.direction;
			mouseYDist = 0f;
			mouseDistance = projSpeed;
		}
		else
		{
			mouseDistance = projSpeed / mouseDistance;
		}
		for (int i = 0; i < 16; i++)
		{
			((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
			realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f;
			realPlayerPos.Y -= 100 * i;
			mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
			mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
			mouseDistance = (float)Math.Sqrt(mouseXDist * mouseXDist + mouseYDist * mouseYDist);
			mouseDistance = projSpeed / mouseDistance;
			mouseXDist *= mouseDistance;
			mouseYDist *= mouseDistance;
			float speedX4 = mouseXDist + (float)Main.rand.Next(-360, 361) * 0.02f;
			float speedY5 = mouseYDist + (float)Main.rand.Next(-360, 361) * 0.02f;
			Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX4, speedY5, type, damage, knockback, player.whoAmI, 2f);
		}
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			int swingDust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, player.direction * 2, 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
			Dust obj = Main.dust[swingDust];
			obj.velocity *= 0.2f;
			Main.dust[swingDust].noGravity = true;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LifeAlloy>(10).AddIngredient<EffulgentFeather>(15).AddTile(134)
			.Register();
	}
}
