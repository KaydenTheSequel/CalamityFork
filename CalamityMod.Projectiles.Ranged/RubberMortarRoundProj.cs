using System;
using CalamityMod.Items.Ammo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class RubberMortarRoundProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/RubberMortarRound";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (!(((Vector2)(ref base.Projectile.velocity)).Length() >= 8f))
		{
			return;
		}
		for (int d = 0; d < 2; d++)
		{
			float xOffset = 0f;
			float yOffset = 0f;
			if (d == 1)
			{
				xOffset = base.Projectile.velocity.X * 0.5f;
				yOffset = base.Projectile.velocity.Y * 0.5f;
			}
			int fire = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + xOffset, base.Projectile.position.Y + 3f + yOffset) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 6, 0f, 0f, 100);
			Main.dust[fire].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
			Dust obj = Main.dust[fire];
			obj.velocity *= 0.2f;
			Main.dust[fire].noGravity = true;
			int smoke = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + xOffset, base.Projectile.position.Y + 3f + yOffset) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
			Dust obj2 = Main.dust[smoke];
			obj2.velocity *= 0.05f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	private void Explode()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(MortarRound.HitboxBlastRadius);
		base.Projectile.maxPenetrate = (base.Projectile.penetrate = -1);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.knockBack *= 2f;
		base.Projectile.Damage();
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.ExplodeTiles(MortarRound.TileBlastRadius);
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		MortarRoundProj.SpawnDust(base.Projectile);
		MortarRoundProj.SpawnExplosionGores(base.Projectile);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.penetrate <= 1)
		{
			base.Projectile.Kill();
			base.Projectile.active = false;
			return false;
		}
		Rectangle origHitbox = base.Projectile.Hitbox;
		int origPen = base.Projectile.penetrate;
		Explode();
		base.Projectile.Hitbox = origHitbox;
		base.Projectile.penetrate = origPen;
		base.Projectile.penetrate--;
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.25f;
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		Explode();
	}
}
