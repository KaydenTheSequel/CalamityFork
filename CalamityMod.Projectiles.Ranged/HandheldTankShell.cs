using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HandheldTankShell : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 11;
		base.Projectile.timeLeft = 1800;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		base.DrawOffsetX = -8;
		base.DrawOriginOffsetY = 0;
		base.DrawOriginOffsetX = -2f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Vector2 pos = base.Projectile.Center;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 7f)
		{
			for (int i = 0; i < 5; i++)
			{
				pos -= base.Projectile.velocity * ((float)i * 0.25f);
				int idx = Dust.NewDust(pos, 1, 1, 158);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].position = pos;
				Main.dust[idx].scale = (float)Main.rand.Next(70, 110) * 0.013f;
				Dust obj = Main.dust[idx];
				obj.velocity *= 0.3f;
			}
			return;
		}
		for (int j = 0; j < 30; j++)
		{
			int dustID;
			switch (Main.rand.Next(6))
			{
			case 0:
				dustID = 55;
				break;
			case 1:
			case 2:
				dustID = 54;
				break;
			default:
				dustID = 53;
				break;
			}
			float num = Main.rand.NextFloat(3f, 13f);
			float angleRandom = 0.06f;
			Vector2 dustVel = Utils.RotatedBy(new Vector2(num, 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel = dustVel.RotatedBy(0f - angleRandom);
			dustVel = dustVel.RotatedByRandom(2f * angleRandom);
			float scale = Main.rand.NextFloat(0.5f, 1.6f);
			int idx2 = Dust.NewDust(pos, 1, 1, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
			Main.dust[idx2].noGravity = true;
			Main.dust[idx2].position = pos;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item62, base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.Item88, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 140);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.LargeFieryExplosion();
		base.Projectile.Damage();
	}
}
