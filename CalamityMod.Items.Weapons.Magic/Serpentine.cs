using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Serpentine : ModItem, ILocalizedModType, IModType
{
	public static int ArmorPenetration = 15;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.damage = 10;
		base.Item.ArmorPenetration = ArmorPenetration;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useTime = 35;
		base.Item.useAnimation = 35;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SerpentineHead>();
		base.Item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		int owner = player.whoAmI;
		float projSpeed = base.Item.shootSpeed;
		player.itemTime = base.Item.useTime;
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Vector2 val = Vector2.UnitX.RotatedBy(player.fullRotation);
		Vector2 projSpawnPos = Main.MouseWorld - realPlayerPos;
		float velX = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
		float velY = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
		if (player.gravDir == -1f)
		{
			velY = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - realPlayerPos.Y;
		}
		float dist = (float)Math.Sqrt(velX * velX + velY * velY);
		if ((float.IsNaN(velX) && float.IsNaN(velY)) || (velX == 0f && velY == 0f))
		{
			velX = player.direction;
			velY = 0f;
			dist = projSpeed;
		}
		else
		{
			dist = projSpeed / dist;
		}
		if (Vector2.Dot(val, projSpawnPos) > 0f)
		{
			player.ChangeDir(1);
		}
		else
		{
			player.ChangeDir(-1);
		}
		int curr = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SerpentineHead>(), damage, knockback, owner);
		int prev = curr;
		curr = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SerpentineBody>(), damage, knockback, owner, prev);
		prev = curr;
		curr = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SerpentineBody>(), damage, knockback, owner, prev);
		prev = curr;
		curr = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SerpentineBody>(), damage, knockback, owner, prev);
		prev = curr;
		curr = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SerpentineTail>(), damage, knockback, owner, prev);
		Main.projectile[prev].localAI[1] = curr;
		Main.projectile[prev].netUpdate = true;
		return false;
	}
}
