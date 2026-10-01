using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SepticSkewerBacteria : ModProjectile, ILocalizedModType, IModType
{
	public bool setStats = true;

	public int rotDirection = 1;

	public int time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 80;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		if (setStats)
		{
			base.Projectile.timeLeft += Main.rand.Next(0, 26);
			rotDirection = ((!Main.rand.NextBool()) ? 1 : (-1));
			base.Projectile.rotation = Main.rand.NextFloat(-20f, 20f);
			base.Projectile.scale = Main.rand.NextFloat(0.55f, 0.8f);
			setStats = false;
		}
		if (time % 8 == 0 && time > 40)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity + Main.rand.NextVector2Circular(20f, 20f), -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.4f), "CalamityMod/Projectiles/Boss/OldDukeGore", affectedByGravity: false, Main.rand.Next(9, 21), Main.rand.NextFloat(0.4f, 0.6f), ((!ChildSafety.Disabled) ? Color.LimeGreen : Color.Lerp(Color.White, Color.Chartreuse, 0.5f)) * Main.rand.NextFloat(0.55f, 0.7f) * Utils.GetLerpValue(255f, 0f, base.Projectile.alpha), new Vector2(1f, 1f), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-1f, 1f)));
		}
		if (Main.rand.NextBool(15))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(13f, 13f), 75);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.9f, 1.3f) * Utils.GetLerpValue(255f, 0f, base.Projectile.alpha);
			dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.7f);
		}
		if (Main.rand.NextBool(3))
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 3f + Main.rand.NextVector2Circular(6f, 6f), (!ChildSafety.Disabled) ? 75 : 5, (-base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(0.8f, 1.4f));
			dust2.noGravity = true;
			dust2.alpha = MathHelper.Clamp(base.Projectile.alpha, 0, 255);
		}
		base.Projectile.rotation += 0.035f * (float)rotDirection * Utils.GetLerpValue(0f, 100f, base.Projectile.timeLeft, clamped: true);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.965f;
		if (base.Projectile.timeLeft < 60)
		{
			base.Projectile.alpha += 5;
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/OldDukeGore", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation;
		Vector2 rotationPoint = value.Size() * 0.5f;
		Main.EntitySpriteDraw(value, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
