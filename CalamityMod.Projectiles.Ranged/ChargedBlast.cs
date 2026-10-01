using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ChargedBlast : ModProjectile, ILocalizedModType, IModType
{
	public Color baseColor;

	public bool outOfTime;

	public Vector2 baseVel;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 6;
		base.Projectile.timeLeft = 360;
	}

	public override void AI()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		_ = base.Projectile.ai[2];
		bool Infinity = base.Projectile.ai[2] == 1f;
		bool Svant = base.Projectile.ai[2] == 2f || base.Projectile.ai[2] == 3f || base.Projectile.ai[2] == 4f;
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (baseColor == Color.White)
		{
			baseColor = (Color)(Infinity ? new Color(229, 49, 39) : (Svant ? Color.DarkViolet : Color.DodgerBlue));
			if (base.Projectile.ai[2] == 3f)
			{
				baseColor = Color.DarkOrchid;
			}
			if (base.Projectile.ai[2] == 4f)
			{
				baseColor = Color.MediumOrchid;
			}
			baseVel = base.Projectile.velocity;
			if (Svant)
			{
				base.Projectile.ArmorPenetration = 200;
			}
			if (Infinity)
			{
				base.Projectile.ArmorPenetration = 20;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft == 2)
		{
			outOfTime = true;
		}
		if ((Svant | Infinity) && base.Projectile.timeLeft % 2 == 0 && targetDist < 1400f && base.Projectile.timeLeft < 340)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center - base.Projectile.velocity * 3f, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 5, 2f, baseColor * 0.65f));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		if (outOfTime)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item62, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, -baseVel.RotatedByRandom(0.8999999761581421) * Main.rand.NextFloat(0.6f, 0.7f), ModContent.ProjectileType<ChargedBlastSplit>(), (int)((float)base.Projectile.damage * ((base.Projectile.ai[2] > 0f) ? 0.3f : 0.5f)), base.Projectile.knockBack * 0.8f, Main.myPlayer, 0f, 0f, base.Projectile.ai[2]);
			}
		}
		for (int i = 0; i < 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, (-baseVel * 4f).RotatedByRandom(0.8999999761581421) * Main.rand.NextFloat(0.8f, 1.2f), affectedByGravity: false, Main.rand.Next(25, 33), Main.rand.NextFloat(1.5f, 2f), baseColor));
		}
		if (base.Projectile.ai[2] == 1f)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, (-baseVel * 5f).RotatedByRandom(0.8999999761581421) * Main.rand.NextFloat(0.8f, 1.2f), affectedByGravity: false, Main.rand.Next(35, 49), Main.rand.NextFloat(2.3f, 3f), baseColor));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		if (baseColor == Color.White)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLineBloom", (AssetRequestMode)2).Value;
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color val = baseColor * 0.7f;
		((Color)(ref val)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, val, 1, texture);
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		val = baseColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(texture, position, null, val, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 20f, targetHitbox);
	}

	public ChargedBlast()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		baseColor = Color.White;
		base._002Ector();
	}
}
