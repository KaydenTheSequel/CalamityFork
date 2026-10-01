using System;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "StardustStaff" })]
public class EidolonStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 56;
		base.Item.damage = 180;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useTime = 18;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.UseSound = SoundID.Item43;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<Starblast>();
		base.Item.shootSpeed = 12f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float projSpeed = base.Item.shootSpeed;
		float mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
		float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
		float mouseDistance = (float)Math.Sqrt(mouseXDist * mouseXDist + mouseYDist * mouseYDist);
		int stardustAmt = 5;
		float mouseXCopy = mouseXDist;
		float mouseYCopy = mouseYDist;
		mouseDistance = (float)Math.Sqrt(mouseXCopy * mouseXCopy + mouseYCopy * mouseYCopy);
		mouseDistance = projSpeed / mouseDistance;
		mouseXCopy *= mouseDistance;
		mouseYCopy *= mouseDistance;
		float x2 = realPlayerPos.X;
		float y2 = realPlayerPos.Y;
		Projectile.NewProjectile(source, x2, y2, mouseXCopy, mouseYCopy, ModContent.ProjectileType<IceCluster>(), damage, knockback, player.whoAmI);
		for (int i = 0; i < stardustAmt; i++)
		{
			mouseXCopy = mouseXDist;
			mouseYCopy = mouseYDist;
			float randOffsetDampener = 0.05f * (float)i;
			mouseXCopy += (float)Main.rand.Next(-90, 91) * randOffsetDampener;
			mouseYCopy += (float)Main.rand.Next(-90, 91) * randOffsetDampener;
			mouseDistance = (float)Math.Sqrt(mouseXCopy * mouseXCopy + mouseYCopy * mouseYCopy);
			mouseDistance = projSpeed / mouseDistance;
			mouseXCopy *= mouseDistance;
			mouseYCopy *= mouseDistance;
			x2 = realPlayerPos.X;
			y2 = realPlayerPos.Y;
			Projectile.NewProjectile(source, x2, y2, mouseXCopy, mouseYCopy, ModContent.ProjectileType<Starblast>(), damage, knockback, player.whoAmI);
		}
		return false;
	}
}
