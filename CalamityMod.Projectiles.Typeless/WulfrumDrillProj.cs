using System;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WulfrumDrillProj : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<WulfrumDrill>();

	public override string Texture => "CalamityMod/Items/Tools/WulfrumDrill";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.aiStyle = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.scale = 0.93f;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 54f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * bladeLength, 24f, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.channel)
		{
			base.Projectile.Kill();
			base.Projectile.active = false;
			return;
		}
		base.Projectile.timeLeft = 2;
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.Item22, Owner.Center);
			base.Projectile.soundDelay = 30;
		}
		base.Projectile.velocity = (Owner.Calamity().mouseWorld - Owner.MountedCenter).SafeNormalize(Vector2.One);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() * Owner.gravDir - (float)Math.PI / 2f);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() * Owner.gravDir - (float)Math.PI / 2f - (float)Math.PI / 8f * (float)Owner.direction);
		Owner.SetDummyItemTime(2);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Center = Owner.MountedCenter + base.Projectile.velocity * 12f;
		base.Projectile.velocity.X *= 1f + (float)Main.rand.Next(-3, 4) * 0.01f;
		if (Main.rand.NextBool(6))
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position + base.Projectile.velocity * (float)Main.rand.Next(6, 10) * 0.1f, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 80, default(Color), 1.4f);
			dust.position.X -= 4f;
			dust.noGravity = true;
			dust.velocity *= 0.2f;
			dust.velocity.Y = (float)(-Main.rand.Next(7, 13)) * 0.15f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.active)
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(9f, (float)tex.Height / 2f);
		Vector2 shake = Main.rand.NextVector2Circular(1f, 1f) * ((float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.25f + 0.75f);
		SpriteEffects effect = (SpriteEffects)0;
		if ((float)Owner.direction * Owner.gravDir < 0f)
		{
			effect = (SpriteEffects)2;
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + shake, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, effect);
		return false;
	}
}
