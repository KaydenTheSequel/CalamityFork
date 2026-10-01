using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ChargedBlastSplit : ModProjectile, ILocalizedModType, IModType
{
	public Color baseColor;

	public int tileHits;

	public Vector2 startVel;

	public bool goToMouse;

	private bool direction;

	public float homeSpeed;

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
		base.Projectile.penetrate = 3;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 240;
		base.Projectile.scale = 0.7f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		_ = base.Projectile.ai[2];
		bool Infinity = base.Projectile.ai[2] == 1f;
		bool Svant = base.Projectile.ai[2] == 2f || base.Projectile.ai[2] == 3f || base.Projectile.ai[2] == 4f;
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
			startVel = base.Projectile.velocity;
			if (Infinity)
			{
				base.Projectile.timeLeft = 340;
				base.Projectile.ArmorPenetration = 20;
			}
			if (Svant)
			{
				base.Projectile.timeLeft = 640;
				base.Projectile.ArmorPenetration = 200;
			}
			direction = Main.rand.NextBool();
			homeSpeed = Main.rand.NextFloat(5f, 8f);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if ((base.Projectile.timeLeft < 290) & Infinity)
		{
			base.Projectile.velocity.X = MathHelper.Lerp(base.Projectile.velocity.X, 0f - startVel.X, 0.02f);
			base.Projectile.velocity.Y = MathHelper.Lerp(base.Projectile.velocity.Y, 0f - startVel.Y, 0.02f);
		}
		if (((base.Projectile.timeLeft <= 500) & Svant) && goToMouse && base.Projectile.timeLeft % base.Projectile.extraUpdates == 0)
		{
			Vector2 mouseSpot = (player.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * homeSpeed;
			base.Projectile.velocity.X = MathHelper.Lerp(base.Projectile.velocity.X, mouseSpot.X, 0.085f);
			base.Projectile.velocity.Y = MathHelper.Lerp(base.Projectile.velocity.Y, mouseSpot.Y, 0.085f);
			if (Vector2.Distance(base.Projectile.Center, player.ClampedMouseWorld()) < 80f)
			{
				base.Projectile.timeLeft = 300;
				base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * homeSpeed / 2f;
				goToMouse = false;
			}
		}
		if (!goToMouse)
		{
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.175f / (float)Math.PI) * 1.2f;
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * (float)(direction ? 1 : (-1));
			Projectile projectile = base.Projectile;
			projectile.Center += offset;
		}
		if (Infinity | Svant)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.penetrate = -1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		bool Svant = base.Projectile.ai[2] == 2f || base.Projectile.ai[2] == 3f || base.Projectile.ai[2] == 4f;
		for (int k = 0; k < (Svant ? 2 : 3); k++)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, (base.Projectile.velocity * 5f).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.8f, 1.2f), affectedByGravity: false, Main.rand.Next(8, 12), Main.rand.NextFloat(0.5f, 1f), baseColor));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
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
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 10f, targetHitbox);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		tileHits--;
		if (tileHits <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public ChargedBlastSplit()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		baseColor = Color.White;
		tileHits = 4;
		goToMouse = true;
		base._002Ector();
	}
}
