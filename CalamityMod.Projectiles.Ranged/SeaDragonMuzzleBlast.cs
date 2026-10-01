using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SeaDragonMuzzleBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 80;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 8;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 30; i++)
		{
			int dustID;
			switch (Main.rand.Next(6))
			{
			case 0:
				dustID = 170;
				break;
			case 1:
			case 2:
				dustID = 174;
				break;
			default:
				dustID = 33;
				break;
			}
			float num = Main.rand.NextFloat(3f, 13f);
			float angleRandom = 0.07f;
			Vector2 dustVel = Utils.RotatedBy(new Vector2(num, 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel = dustVel.RotatedBy(0f - angleRandom);
			dustVel = dustVel.RotatedByRandom(2f * angleRandom);
			float scale = Main.rand.NextFloat(0.7f, 1.8f);
			int idx = Dust.NewDust(base.Projectile.Center, 1, 1, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
			Main.dust[idx].noGravity = true;
			Main.dust[idx].position = base.Projectile.Center;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 240);
	}
}
