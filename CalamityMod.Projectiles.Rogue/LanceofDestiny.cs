using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class LanceofDestiny : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle Hitsound = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerSegmentBreak2")
	{
		Volume = 0.6f,
		PitchVariance = 0.3f
	};

	public bool posthit;

	public int Time;

	public int hitsDust = 7;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/LanceofDestiny";

	public override void SetDefaults()
	{
		base.Projectile.width = 25;
		base.Projectile.height = 25;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 6;
		base.Projectile.timeLeft = 900;
		base.Projectile.aiStyle = 0;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 6;
	}

	public override void AI()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		Vector3 DustLight = default(Vector3);
		((Vector3)(ref DustLight))._002Ector(0.255f, 0.252f, 0.1f);
		Lighting.AddLight(base.Projectile.Center, DustLight * 3f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.02f;
		if (!base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.extraUpdates = 1;
		}
		if (base.Projectile.timeLeft % 2 == 0)
		{
			for (int i = 0; i < 4; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), -base.Projectile.velocity, Color.PaleGoldenrod, Color.Goldenrod, Main.rand.NextFloat(0.65f, 0.95f), 8, 0f, 2.5f));
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		bool defaultsonce = true;
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.aiStyle = 0;
			base.Projectile.extraUpdates = 2;
			if (base.Projectile.ai[0] == 0f && defaultsonce)
			{
				base.Projectile.penetrate = 10;
				base.Projectile.localNPCHitCooldown = 60;
				defaultsonce = false;
			}
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (hitsDust > 1)
		{
			hitsDust--;
		}
		for (int i = 0; i <= hitsDust; i++)
		{
			Vector2 sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.6f, 1.5f);
			Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity, Main.rand.NextBool(3) ? 130 : 133, sparkVelocity, 0, default(Color), Main.rand.NextFloat(1.2f, 1.5f)).noGravity = true;
		}
		SoundEngine.PlaySound(in Hitsound, base.Projectile.Center);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.Calamity().stealthStrike && !posthit)
		{
			posthit = true;
		}
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.9f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8 && base.Projectile.Calamity().stealthStrike)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 9; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, Main.rand.Next(169, 170), base.Projectile.oldVelocity.X * 0.3f, base.Projectile.oldVelocity.Y * 0.3f, 0, default(Color), Main.rand.NextFloat(1.2f, 1.6f));
		}
	}
}
