using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class NanoFlare : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 10;
		base.Projectile.timeLeft = 60;
	}

	public override void AI()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 0, default(Color), 0.75f);
			dust.velocity = base.Projectile.velocity * Main.rand.NextFloat(-0.3f, 0.3f);
			dust.color = Main.hslToRgb(Main.rand.NextFloat(0.2f, 0.6f), 0.9f, 0.5f);
			dust.color = Color.Lerp(dust.color, Color.White, 0.3f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 20; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-6f, 6f));
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 107, dspeed.X, dspeed.Y, 0, default(Color), 0.7f);
			dust.color = Main.hslToRgb(Main.rand.NextFloat(0.2f, 0.6f), 0.9f, 0.5f);
			dust.color = Color.Lerp(dust.color, Color.White, 0.3f);
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		if (Main.dedServ)
		{
			return;
		}
		Vector2 goreVec = default(Vector2);
		((Vector2)(ref goreVec))._002Ector(base.Projectile.Center.X - 24f, base.Projectile.Center.Y - 24f);
		float smokeScale = 0.66f;
		for (int j = 0; j < 2; j++)
		{
			switch (Main.rand.Next(1, 5))
			{
			case 1:
			{
				int idx7 = Gore.NewGore(base.Projectile.GetSource_Death(), goreVec, default(Vector2), Main.rand.Next(61, 64));
				Gore obj4 = Main.gore[idx7];
				obj4.velocity *= smokeScale;
				Main.gore[idx7].velocity.X++;
				Main.gore[idx7].velocity.Y++;
				break;
			}
			case 2:
			{
				int idx6 = Gore.NewGore(base.Projectile.GetSource_Death(), goreVec, default(Vector2), Main.rand.Next(61, 64));
				Gore obj3 = Main.gore[idx6];
				obj3.velocity *= smokeScale;
				Main.gore[idx6].velocity.X--;
				Main.gore[idx6].velocity.Y++;
				break;
			}
			case 3:
			{
				int idx5 = Gore.NewGore(base.Projectile.GetSource_Death(), goreVec, default(Vector2), Main.rand.Next(61, 64));
				Gore obj2 = Main.gore[idx5];
				obj2.velocity *= smokeScale;
				Main.gore[idx5].velocity.X++;
				Main.gore[idx5].velocity.Y--;
				break;
			}
			case 4:
			{
				int idx4 = Gore.NewGore(base.Projectile.GetSource_Death(), goreVec, default(Vector2), Main.rand.Next(61, 64));
				Gore obj = Main.gore[idx4];
				obj.velocity *= smokeScale;
				Main.gore[idx4].velocity.X--;
				Main.gore[idx4].velocity.Y--;
				break;
			}
			}
		}
	}
}
