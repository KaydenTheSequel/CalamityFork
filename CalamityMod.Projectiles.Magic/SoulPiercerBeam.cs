using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SoulPiercerBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 360;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 9f)
		{
			Vector2 projPos = base.Projectile.position;
			projPos -= base.Projectile.velocity;
			base.Projectile.alpha = 255;
			int godSlay = Dust.NewDust(projPos, 1, 1, 173, 0f, 0f, 0, default(Color), 0.5f);
			Main.dust[godSlay].position = projPos;
			Main.dust[godSlay].scale = (float)Main.rand.Next(70, 110) * 0.014f;
			Dust obj = Main.dust[godSlay];
			obj.velocity *= 0.2f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int x = 0; x < 5; x++)
		{
			if (base.Projectile.owner == Main.myPlayer && base.Projectile.numHits == 0)
			{
				CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, target.Center, fromRight: true, -500f, 500f, 0f, 500f, 10f, ModContent.ProjectileType<SoulPiercerBolt>(), base.Projectile.damage, 0f, base.Projectile.owner, clamped: false, 0f);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}
}
