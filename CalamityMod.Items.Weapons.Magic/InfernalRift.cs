using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class InfernalRift : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.damage = 50;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useTime = 2;
		base.Item.useAnimation = 30;
		base.Item.reuseDelay = base.Item.useAnimation + 6;
		base.Item.useLimitPerAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item9;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<InfernalBlade>();
		base.Item.shootSpeed = 28f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 25f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float projSpeed = base.Item.shootSpeed;
		float mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
		float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
		float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
		float lowerLerpValue = 20f;
		float upperLerpValue = 60f;
		Vector2 projSpawnOffset = realPlayerPos + f.ToRotationVector2() * MathHelper.Lerp(lowerLerpValue, upperLerpValue, Main.rand.NextFloat());
		for (int i = 0; i < 50; i++)
		{
			projSpawnOffset = realPlayerPos + f.ToRotationVector2() * MathHelper.Lerp(lowerLerpValue, upperLerpValue, Main.rand.NextFloat());
			if (Collision.CanHit(realPlayerPos, 0, 0, projSpawnOffset + (projSpawnOffset - realPlayerPos).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
			{
				break;
			}
			f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
		}
		Vector2 projSpawnPos = player.ClampedMouseWorld() - projSpawnOffset;
		Vector2 projVelocity = Utils.SafeNormalize(new Vector2(mouseXDist, mouseYDist), Vector2.UnitY) * projSpeed;
		projSpawnPos = projSpawnPos.SafeNormalize(projVelocity) * projSpeed;
		projSpawnPos = Vector2.Lerp(projSpawnPos, projVelocity, 0.25f);
		Projectile.NewProjectile(source, projSpawnOffset, projSpawnPos, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3787).AddIngredient<ScoriaBar>(10).AddTile(134)
			.Register();
	}
}
