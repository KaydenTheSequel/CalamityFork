using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class PlanetaryAnnihilation : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 102;
		base.Item.damage = 66;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item75;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PlanetaryAnnihilationProj>();
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
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
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		float arrowSpeed = base.Item.shootSpeed;
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
			mouseDistance = arrowSpeed;
		}
		else
		{
			mouseDistance = arrowSpeed / mouseDistance;
		}
		((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
		realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
		realPlayerPos.Y -= 100f;
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
		mouseDistance = arrowSpeed / mouseDistance;
		mouseXDist *= mouseDistance;
		mouseYDist *= mouseDistance;
		if (CalamityUtils.CheckWoodenAmmo(type, player))
		{
			for (int i = 0; i < 7; i++)
			{
				float speedX4 = mouseXDist + (float)Main.rand.Next(-120, 121) * 0.02f;
				float speedY5 = mouseYDist + (float)Main.rand.Next(-120, 121) * 0.02f;
				Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX4, speedY5, ModContent.ProjectileType<PlanetaryAnnihilationProj>(), damage, knockback, player.whoAmI, 0f, i);
			}
		}
		else
		{
			for (int j = 0; j < 7; j++)
			{
				float speedX5 = mouseXDist + (float)Main.rand.Next(-120, 121) * 0.02f;
				float speedY6 = mouseYDist + (float)Main.rand.Next(-120, 121) * 0.02f;
				int num121 = Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX5, speedY6, type, damage, knockback, player.whoAmI);
				Main.projectile[num121].noDropItem = true;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<VernalBolter>().AddIngredient(3029).AddIngredient(3467, 5)
			.AddIngredient<LifeAlloy>(5)
			.AddIngredient(3456, 5)
			.AddTile(134)
			.Register();
	}
}
