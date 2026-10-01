using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CraniumSmasherExplosive : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 5f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.02f;
		base.Projectile.velocity.Y += 0.085f;
		base.Projectile.velocity.X *= 0.99f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(200);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		if (!Main.dedServ)
		{
			int goreAmt = 3;
			Vector2 source = base.Projectile.Center - new Vector2(24f);
			for (int goreIndex = 1; goreIndex <= goreAmt; goreIndex++)
			{
				float velocityMult = 0.33f * (float)goreIndex;
				int type = Main.rand.Next(61, 64);
				Gore gore = Gore.NewGoreDirect(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				gore.velocity *= velocityMult;
				type = Main.rand.Next(61, 64);
				Gore gore2 = Gore.NewGoreDirect(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				gore2.velocity *= velocityMult;
			}
		}
		for (int i = 0; i < 30; i++)
		{
			float edgeOffset = Main.rand.NextFloat(60f, 100f) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			float randOffset = Main.rand.NextFloat(-100f, 100f);
			Dust.NewDustPerfect(base.Projectile.Center + ((i % 2 == 0) ? new Vector2(edgeOffset, randOffset) : new Vector2(randOffset, edgeOffset)), 135, Vector2.Zero, 100, default(Color), 2f).noGravity = true;
		}
	}
}
