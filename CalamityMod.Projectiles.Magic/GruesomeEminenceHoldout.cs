using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GruesomeEminenceHoldout : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<GruesomeEminence>();

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		StickToOwner();
		int congregationType = ModContent.ProjectileType<SpiritCongregation>();
		if (!Owner.channel)
		{
			base.Projectile.Kill();
		}
		else if (Owner.ownedProjectileCounts[congregationType] <= 0)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 spiritSpawnPosition = base.Projectile.Center - Vector2.UnitY * 12f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spiritSpawnPosition, -Vector2.UnitY * 10f, congregationType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalOpen, base.Projectile.Center);
		}
		Lighting.AddLight(base.Projectile.Center, 0.13f, 0f, 0.05f);
		Dust ghostlyMagic = Dust.NewDustPerfect(base.Projectile.Top + Main.rand.NextVector2Circular(5f, 5f) + Vector2.UnitX * (float)base.Projectile.direction * 7f, 267);
		ghostlyMagic.color = Color.Lerp(Color.DarkRed, Color.Fuchsia, Main.rand.NextFloat(0.7f));
		ghostlyMagic.color = Color.Lerp(ghostlyMagic.color, Color.Black, 0.5f);
		ghostlyMagic.velocity = -Vector2.UnitY.RotatedBy(0.4399999976158142) * Main.rand.NextFloat(0.8f, 2f);
		ghostlyMagic.noGravity = true;
		if (Main.rand.NextBool(8))
		{
			ghostlyMagic.velocity *= 1.7f;
		}
	}

	public void StickToOwner()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = 0f;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		Owner.ChangeDir(base.Projectile.direction);
		Vector2 centerDelta = Main.OffsetsPlayerOnhand[Owner.bodyFrame.Y / 56] * 2f;
		if (Owner.direction != 1)
		{
			centerDelta.X = (float)Owner.bodyFrame.Width - centerDelta.X;
		}
		if (Owner.gravDir != 1f)
		{
			centerDelta.Y = (float)Owner.bodyFrame.Height - centerDelta.Y;
		}
		if (Owner.heldProj == -1)
		{
			Owner.heldProj = base.Projectile.whoAmI;
		}
		centerDelta -= new Vector2((float)(Owner.bodyFrame.Width - Owner.width), (float)(Owner.bodyFrame.Height - 42)) / 2f;
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.position + centerDelta, reverseRotation: true) - base.Projectile.velocity;
		if (base.Projectile.spriteDirection == 1)
		{
			base.Projectile.position.X += 4f;
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemLocation = base.Projectile.Center;
		Owner.itemRotation = (float)Math.PI / 4f * (float)(-base.Projectile.direction);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
