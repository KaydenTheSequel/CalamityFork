using System;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RealityRuptureStealth : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle Hitsound = new SoundStyle("CalamityMod/Sounds/Item/RealityRuptureStealthHit")
	{
		Volume = 1.2f,
		PitchVariance = 0.3f
	};

	public bool posthit;

	public int Time;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/RealityRupture";

	public override void SetDefaults()
	{
		base.Projectile.width = 43;
		base.Projectile.height = 43;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 900;
		base.Projectile.aiStyle = 0;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 7;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		float playerDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		Time++;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.003f;
		base.Projectile.scale = 1.3f;
		Vector3 DustLight = default(Vector3);
		((Vector3)(ref DustLight))._002Ector(0.209f, 0.14f, 0.202f);
		Lighting.AddLight(base.Projectile.Center, DustLight * 8f);
		if (Time > 10 && playerDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity, base.Projectile.velocity * 0.01f, affectedByGravity: false, 18, 3.4f, Color.Plum * 0.6f));
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in Hitsound, base.Projectile.position);
		posthit = true;
		for (int i = 0; i < 6; i++)
		{
			Vector2 vel = base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(20f));
			float Scale = Main.rand.NextFloat(1.8f, 2.3f);
			GeneralParticleHandler.SpawnParticle(new CrackParticle(base.Projectile.Center, vel, Color.Orchid, new Vector2(1f, 1f), 0f, Scale, Scale - 0.5f, Main.rand.Next(18, 23)));
		}
		Vector2 vel2 = base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(3f));
		float Scale2 = Main.rand.NextFloat(2.9f, 3.2f);
		GeneralParticleHandler.SpawnParticle(new CrackParticle(base.Projectile.Center, vel2, Color.Plum, new Vector2(1f, 1f), 0f, Scale2, Scale2 - 0.5f, Main.rand.Next(27, 32)));
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<SpearofDestinyStealthExplosion>(), base.Projectile.damage / 2, base.Projectile.knockBack * 2f, base.Projectile.owner);
		Main.player[base.Projectile.owner].SetScreenshake(8f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int frameY = frameHeight * base.Projectile.frame;
		float scale = base.Projectile.scale;
		float rotation = base.Projectile.rotation;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameY, texture.Width, frameHeight);
		Vector2 origin = rectangle.Size() / 2f;
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, Color.White, rotation, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 15; i++)
		{
			Vector2 sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.6f, 1.5f);
			Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity, 272, sparkVelocity.RotatedByRandom(0.10000000149011612), 0, default(Color), Main.rand.NextFloat(1.2f, 1.5f)).noGravity = true;
			int sparkLifetime = Main.rand.Next(43, 48);
			float sparkScale = Main.rand.NextFloat(2.2f, 3f);
			Color sparkColor = Color.Plum * 0.8f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
		}
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.Plum, new Vector2(2f, 2f), Main.rand.NextFloat(12f, 25f), 0.1f, 1f, 13));
	}
}
