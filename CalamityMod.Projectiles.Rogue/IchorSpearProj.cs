using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class IchorSpearProj : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle Hitsound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeTileHit2")
	{
		PitchVariance = 0.3f,
		Volume = 0.5f
	};

	public bool posthit;

	public int framesInAir;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/IchorSpear";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 6;
		base.Projectile.timeLeft = 900;
		base.Projectile.extraUpdates = 1;
		base.Projectile.scale = 1.2f;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.SolidCollision(base.Projectile.Center, 3, 3) && !base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.Kill();
		}
		framesInAir++;
		if (framesInAir > 90 && !base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.velocity.X *= 0.998f;
			base.Projectile.velocity.Y += 0.3f;
		}
		if (Main.rand.NextBool() && !posthit)
		{
			Vector2 position = base.Projectile.Center + Vector2.Normalize(base.Projectile.velocity);
			Dust obj = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(169, 170), 0f, 0f, 0, default(Color), base.Projectile.Calamity().stealthStrike ? Main.rand.NextFloat(2.1f, 3.2f) : Main.rand.NextFloat(1.2f, 1.5f))];
			obj.position = position;
			obj.velocity = base.Projectile.velocity.RotatedBy(1.9707963705062865) * 0.1f + base.Projectile.velocity / 8f;
			obj.position += base.Projectile.velocity.RotatedBy(0.3);
			obj.fadeIn = 0.5f;
			obj.noGravity = true;
			Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(169, 170), 0f, 0f, 0, default(Color), base.Projectile.Calamity().stealthStrike ? Main.rand.NextFloat(2.1f, 3.2f) : Main.rand.NextFloat(1.2f, 1.5f))];
			obj2.position = position;
			obj2.velocity = base.Projectile.velocity.RotatedBy(-1.9707963705062865) * 0.1f + base.Projectile.velocity / 8f;
			obj2.position += base.Projectile.velocity.RotatedBy(-0.3);
			obj2.fadeIn = 0.5f;
			obj2.noGravity = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		bool stealthSet = true;
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.extraUpdates = 2;
			if (base.Projectile.ai[0] == 0f && stealthSet)
			{
				base.Projectile.localNPCHitCooldown = 60;
				stealthSet = false;
			}
			base.Projectile.StickyProjAI(10);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		Vector2 bloodSpawnPosition = target.Center + Main.rand.NextVector2Circular(target.width, target.height) * 0.04f;
		(base.Projectile.Center - bloodSpawnPosition).SafeNormalize(Vector2.UnitY);
		int sparkCount = Main.rand.Next(4, 6);
		for (int i = 0; i < sparkCount; i++)
		{
			Vector2 sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.6f, 1.1f);
			int sparkLifetime = Main.rand.Next(23, 25);
			float sparkScale = Main.rand.NextFloat(0.8f, 1f) * 0.955f;
			Color sparkColor = Color.Lerp(Color.Gold, Color.Goldenrod, Main.rand.NextFloat(0.7f));
			sparkColor = Color.Lerp(sparkColor, Color.Gold, Main.rand.NextFloat());
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
		}
		SoundEngine.PlaySound(in Hitsound, base.Projectile.position);
		target.AddBuff(69, base.Projectile.Calamity().stealthStrike ? 900 : 180);
		if (base.Projectile.Calamity().stealthStrike)
		{
			posthit = true;
			for (int j = 0; j <= 17; j++)
			{
				Dust obj = Main.dust[Dust.NewDust(target.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(169, 170), 0f, 0f, 0, default(Color), 3.2f)];
				obj.noGravity = true;
				obj.velocity.Y -= Main.rand.NextFloat(2.5f, 10.5f);
				obj.velocity.X += Main.rand.NextFloat(-3f, 3f);
			}
			SoundEngine.PlaySound(in SoundID.NPCHit18, base.Projectile.position);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.Calamity().stealthStrike && !posthit)
		{
			base.Projectile.ModifyHitNPCSticky(2);
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

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(69, base.Projectile.Calamity().stealthStrike ? 600 : 120);
	}
}
