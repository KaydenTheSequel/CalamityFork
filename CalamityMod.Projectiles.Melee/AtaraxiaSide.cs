using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AtaraxiaSide : ModProjectile, ILocalizedModType, IModType
{
	private static int NumAnimationFrames = 5;

	private static int AnimationFrameTime = 9;

	public int time;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = NumAnimationFrames;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		base.DrawOffsetX = -28;
		base.DrawOriginOffsetY = -2;
		base.DrawOriginOffsetX = 12f;
		if (base.Projectile.ai[1] == 2f)
		{
			base.Projectile.rotation = base.Projectile.velocity.RotatedBy(0.20000000298023224).ToRotation();
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.RotatedBy(-0.20000000298023224).ToRotation();
		}
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.1f, 0.45f);
		if (time > 8 && Main.rand.NextBool())
		{
			float colorRando = Main.rand.NextFloat(0f, 1f);
			Vector2 dustvel = ((base.Projectile.ai[1] == 2f) ? base.Projectile.velocity.RotatedBy(0.20000000298023224) : base.Projectile.velocity.RotatedBy(-0.20000000298023224));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity, 261, -dustvel * Main.rand.NextFloat(0.2f, 1.2f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.DarkOrchid, Color.IndianRed, colorRando);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > AnimationFrameTime)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= NumAnimationFrames)
		{
			base.Projectile.frame = 0;
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(153, 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item89, base.Projectile.Center);
		int numSplits = 6;
		int splitID = ModContent.ProjectileType<AtaraxiaSplit>();
		int damage = (int)((float)base.Projectile.damage * 0.02f);
		float angleVariance = (float)Math.PI * 2f / (float)numSplits;
		Vector2 projVec = Utils.RotatedByRandom(new Vector2(4.5f, 0f), 6.2831854820251465);
		for (int i = 0; i < numSplits; i++)
		{
			projVec = projVec.RotatedBy(angleVariance);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, projVec, splitID, damage, 1.5f, Main.myPlayer);
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 15f, targetHitbox);
	}
}
