using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class RedSun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 62;
		base.Item.damage = 350;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTime = 10;
		base.Item.useAnimation = 40;
		base.Item.reuseDelay = 0;
		base.Item.useLimitPerAnimation = 6;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<RSSolarFlare>();
		base.Item.shootSpeed = 15f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		float flareSpeed = base.Item.shootSpeed;
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
			mouseDistance = flareSpeed;
		}
		else
		{
			mouseDistance = flareSpeed / mouseDistance;
		}
		for (int i = 0; i < 3; i++)
		{
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
			mouseDistance = flareSpeed / mouseDistance;
			mouseXDist *= mouseDistance;
			mouseYDist *= mouseDistance;
			float speedX4 = mouseXDist + (float)Main.rand.Next(-1000, 1001) * 0.02f;
			float speedY5 = mouseYDist + (float)Main.rand.Next(-1000, 1001) * 0.02f;
			Projectile.NewProjectile(source, realPlayerPos.X, realPlayerPos.Y, speedX4, speedY5, ModContent.ProjectileType<RSSolarFlare>(), (int)((double)damage * 0.7), knockback, player.whoAmI, 0f, Main.rand.Next(10));
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
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 64);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(189, 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 300);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3772).AddIngredient<ForsakenSaber>().AddIngredient<ShadowspecBar>(5)
			.AddIngredient<EssenceofSunlight>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
