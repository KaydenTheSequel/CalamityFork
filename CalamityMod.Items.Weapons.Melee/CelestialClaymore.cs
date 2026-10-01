using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class CelestialClaymore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 82;
		base.Item.damage = 50;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 24;
		base.Item.useTime = 24;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 5.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<CosmicSpiritBomb>();
		base.Item.shootSpeed = 0.1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float mouseXDist = (float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X;
		float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y;
		if (player.gravDir == -1f)
		{
			mouseYDist = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + realPlayerPos.Y;
		}
		Vector2 realPositionOrig = realPlayerPos;
		mouseXDist *= 0.8f;
		mouseYDist *= 0.8f;
		for (int i = 0; i < 2; i++)
		{
			realPlayerPos = realPositionOrig;
			realPlayerPos.X += Main.rand.Next(-75, 76);
			realPlayerPos.Y += Main.rand.Next(-75, 76);
			realPlayerPos += new Vector2(mouseXDist, mouseYDist);
			Projectile.NewProjectile(source, realPlayerPos, Vector2.Zero, type, damage, knockback, player.whoAmI, Main.rand.Next(3));
		}
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			int dustType = -1;
			switch (Main.rand.Next(3))
			{
			case 0:
				dustType = 15;
				break;
			case 1:
				dustType = 73;
				break;
			case 2:
				dustType = 244;
				break;
			}
			int swingDust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, dustType, player.direction * 2, 0f, 150, default(Color), 1.3f);
			Dust obj = Main.dust[swingDust];
			obj.velocity *= 0.2f;
		}
	}
}
