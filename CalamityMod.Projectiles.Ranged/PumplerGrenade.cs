using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PumplerGrenade : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float State => ref base.Projectile.ai[0];

	public static int MaxTime => 180;

	public override string Texture => "CalamityMod/Projectiles/Ranged/PumplerGrenade";

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = MaxTime;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	private void Explode(bool NPCHit = false)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		int size = (NPCHit ? 60 : 90);
		base.Projectile.scale = (float)size / (float)base.Projectile.height * base.Projectile.scale;
		Projectile projectile = base.Projectile;
		projectile.position -= Vector2.One * (float)(size - base.Projectile.width) * 0.5f;
		base.Projectile.height = (base.Projectile.width = size);
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 6;
		base.Projectile.hide = true;
		base.Projectile.velocity = Vector2.Zero;
		State = 1f;
	}

	public override void AI()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (State == 0f)
		{
			if (base.Projectile.timeLeft == 1)
			{
				Explode();
				return;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			Point tileCoords = base.Projectile.Bottom.ToTileCoordinates();
			if (Main.tile[tileCoords.X, tileCoords.Y + 1].HasUnactuatedTile && WorldGen.SolidTile(Main.tile[tileCoords.X, tileCoords.Y + 1]) && base.Projectile.timeLeft < 165)
			{
				Explode();
			}
			else if (base.Projectile.timeLeft < MaxTime - 8)
			{
				base.Projectile.velocity.Y += 0.4f;
				if (base.Projectile.velocity.Y > 16f)
				{
					base.Projectile.velocity.Y = 16f;
				}
			}
		}
		else if (base.Projectile.timeLeft == 5)
		{
			SmokeBoom();
		}
	}

	private void SmokeBoom()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 15; i++)
		{
			SmallSmokeParticle smallSmokeParticle = new SmallSmokeParticle(base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f), Vector2.Zero, Color.Orange, new Color(40, 40, 40), Main.rand.NextFloat(0.8f, 1.6f), 145 - Main.rand.Next(30));
			smallSmokeParticle.Velocity = (smallSmokeParticle.Position - base.Projectile.Center) * 0.2f + base.Projectile.velocity;
			GeneralParticleHandler.SpawnParticle(smallSmokeParticle);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= -1f;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (State == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
			Explode(NPCHit: true);
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (State == 0f)
		{
			return null;
		}
		float blastRadius = (float)base.Projectile.height / 2f;
		float x = base.Projectile.Center.X;
		Rectangle hitbox = target.Hitbox;
		float distanceX = Math.Abs(x - (float)((Rectangle)(ref hitbox)).Center.X);
		float y = base.Projectile.Center.Y;
		hitbox = target.Hitbox;
		float distanceY = Math.Abs(y - (float)((Rectangle)(ref hitbox)).Center.Y);
		if (distanceX > (float)target.Hitbox.Width / 2f + blastRadius || distanceY > (float)target.Hitbox.Height / 2f + blastRadius)
		{
			return false;
		}
		if (distanceX <= (float)target.Hitbox.Width / 2f || distanceY <= (float)target.Hitbox.Height / 2f)
		{
			return null;
		}
		if ((double)(float)(Math.Pow(distanceX - (float)target.Hitbox.Width / 2f, 2.0) + Math.Pow(distanceY - (float)target.Hitbox.Height / 2f, 2.0)) <= Math.Pow(blastRadius, 2.0))
		{
			return null;
		}
		return false;
	}
}
