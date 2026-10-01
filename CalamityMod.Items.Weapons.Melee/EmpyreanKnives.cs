using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class EmpyreanKnives : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 20;
		base.Item.damage = 130;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 1;
		base.Item.useTime = 15;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item39;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<EmpyreanKnife>();
		base.Item.shootSpeed = 15f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		float knifeSpeed = base.Item.shootSpeed;
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
			mouseDistance = knifeSpeed;
		}
		else
		{
			mouseDistance = knifeSpeed / mouseDistance;
		}
		mouseXDist *= mouseDistance;
		mouseYDist *= mouseDistance;
		int knifeAmt = 4;
		if (Main.rand.NextBool())
		{
			knifeAmt++;
		}
		if (Main.rand.NextBool(4))
		{
			knifeAmt++;
		}
		if (Main.rand.NextBool(8))
		{
			knifeAmt++;
		}
		if (Main.rand.NextBool(16))
		{
			knifeAmt++;
		}
		for (int i = 0; i < knifeAmt; i++)
		{
			float knifeSpawnXPos = mouseXDist;
			float knifeSpawnYPos = mouseYDist;
			float randOffsetDampener = 0.05f * (float)i;
			knifeSpawnXPos += (float)Main.rand.Next(-25, 26) * randOffsetDampener;
			knifeSpawnYPos += (float)Main.rand.Next(-25, 26) * randOffsetDampener;
			mouseDistance = (float)Math.Sqrt(knifeSpawnXPos * knifeSpawnXPos + knifeSpawnYPos * knifeSpawnYPos);
			mouseDistance = knifeSpeed / mouseDistance;
			knifeSpawnXPos *= mouseDistance;
			knifeSpawnYPos *= mouseDistance;
			float x4 = realPlayerPos.X;
			float y4 = realPlayerPos.Y;
			Projectile.NewProjectile(source, x4, y4, knifeSpawnXPos, knifeSpawnYPos, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1569).AddIngredient<MonstrousKnives>().AddIngredient<TwistingNether>(3)
			.AddTile(134)
			.Register();
	}
}
