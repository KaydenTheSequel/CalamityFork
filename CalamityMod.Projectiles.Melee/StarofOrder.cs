using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StarofOrder : ModProjectile, ILocalizedModType, IModType
{
	public Color mainColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 44;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 4;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		mainColor = player.Calamity().lightRGB;
		if (time == 0f)
		{
			base.Projectile.scale = Main.rand.NextFloat(0.65f, 0.8f);
		}
		float sine = (float)Math.Sin(time * 0.575f * base.Projectile.scale / (float)Math.PI);
		base.Projectile.rotation = 0.25f * sine;
		if (time < 20f)
		{
			base.Projectile.extraUpdates = 1;
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy((0f - base.Projectile.ai[1]) * 0.05f);
		}
		else
		{
			base.Projectile.extraUpdates = 0;
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 900f, 25f, MathHelper.Clamp(30f - time, 15f, 30f));
		}
		if (time % 2f == 0f && base.Projectile.numHits < 1)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 13, 0.35f, mainColor * 0.75f, new Vector2(0.8f, 1.35f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.3f));
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color val = Color.Lerp(mainColor, Color.White, 0.2f);
		((Color)(ref val)).A = 0;
		Color drawColor = val;
		_ = base.Projectile.rotation;
		Vector2 rotationPoint = texture.Size() * 0.5f;
		bool roted = true;
		for (int i = 0; i < 5; i++)
		{
			Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, (roted ? 0f : ((float)Math.PI / 2f)) + base.Projectile.rotation, rotationPoint, new Vector2(1f - 0.12f * (float)i, 1f + 0.75f * (float)i) * base.Projectile.scale * 0.2f * Main.rand.NextFloat(0.8f, 1.1f), (SpriteEffects)0);
			if (roted && i == 4)
			{
				i = -1;
				roted = false;
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		int buffType = ModContent.BuffType<ElementalMix>();
		target.AddBuff(buffType, 60);
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.timeLeft = 180;
			time = 0f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.2f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		int points = 4;
		float radians = (float)Math.PI * 2f / (float)points;
		Vector2 spinningPoint = Vector2.Normalize(Utils.RotatedBy(new Vector2(-1f, -1f), (double)base.Projectile.rotation, default(Vector2)));
		for (int k = 0; k < points; k++)
		{
			Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.44999998807907104);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + velocity * 2f, ModContent.DustType<SquashDust>(), velocity * 8f);
			dust.scale = 3.5f;
			dust.color = mainColor;
			dust.noGravity = true;
			dust.fadeIn = 6f;
		}
	}
}
