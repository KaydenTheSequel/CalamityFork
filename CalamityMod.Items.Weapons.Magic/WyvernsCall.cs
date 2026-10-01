using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class WyvernsCall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 74;
		base.Item.damage = 65;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 23;
		base.Item.useAnimation = 23;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.75f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item102;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<WyvernFeatherPurple>();
		base.Item.shootSpeed = 35f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		float projSpeed = base.Item.shootSpeed;
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
			mouseDistance = projSpeed;
		}
		else
		{
			mouseDistance = projSpeed / mouseDistance;
		}
		for (int i = 0; i < 3; i++)
		{
			float damageMult = 1f;
			float kbMult = 1f;
			if (Main.rand.NextBool(10))
			{
				type = ModContent.ProjectileType<WyvernProjectile>();
			}
			else if (Main.rand.NextBool(3))
			{
				type = ModContent.ProjectileType<WyvernFeatherGreen>();
			}
			else if (Main.rand.NextBool())
			{
				type = ModContent.ProjectileType<WyvernFeatherPink>();
			}
			if (type == ModContent.ProjectileType<WyvernProjectile>())
			{
				damageMult = 15f;
				kbMult = 1.5f;
			}
			if (type == ModContent.ProjectileType<WyvernFeatherPink>())
			{
				kbMult = 2f;
			}
			((Vector2)(ref realPlayerPos))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			realPlayerPos.X = (realPlayerPos.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			realPlayerPos.Y -= 100 * i;
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
			mouseDistance = projSpeed / mouseDistance;
			mouseXDist *= mouseDistance;
			mouseYDist *= mouseDistance;
			Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, mouseXDist, mouseYDist, type, (int)((float)damage * damageMult), (int)(knockback * kbMult), player.whoAmI, 0f, Main.rand.Next(15));
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SkyGlaze>().AddRecipeGroup("AnyMythrilBar", 5).AddIngredient(575, 15)
			.AddTile(134)
			.Register();
	}
}
