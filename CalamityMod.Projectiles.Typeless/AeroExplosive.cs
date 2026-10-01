using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AeroExplosive : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Items/Weapons/Typeless/Skynamite";

	public override void SetDefaults()
	{
		base.Projectile.width = 15;
		base.Projectile.height = 15;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 2)
		{
			base.Projectile.damage = 250;
			base.Projectile.knockBack = 10f;
		}
		if (base.Projectile.timeLeft % 4 == 0 && base.Projectile.timeLeft < 270)
		{
			base.Projectile.ai[0]++;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.999f - base.Projectile.ai[0] * Main.rand.NextFloat(0.00075f, 0.00125f);
		base.Projectile.rotation += ((Vector2)(ref base.Projectile.velocity)).Length() * 0.09f * (float)base.Projectile.direction;
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 187, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 100, new Color(53, Main.DiscoG, 255));
		}
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 16, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		if (Main.rand.NextBool())
		{
			int smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100);
			Main.dust[smoke].scale = 0.1f + Main.rand.NextFloat(0f, 0.5f);
			Main.dust[smoke].fadeIn = 1.5f + Main.rand.NextFloat(0f, 0.5f);
			Main.dust[smoke].noGravity = true;
			Main.dust[smoke].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (float)(-base.Projectile.height) / 2f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
			Main.dust[fire].scale = 1f + Main.rand.NextFloat(0f, 0.5f);
			Main.dust[fire].noGravity = true;
			Main.dust[fire].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (float)(-base.Projectile.height) / 2f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = (0f - oldVelocity.X) * 0.1f;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.1f;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(200);
		base.Projectile.maxPenetrate = (base.Projectile.penetrate = -1);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int d = 0; d < 40; d++)
		{
			int smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[smoke];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[smoke].scale = 0.5f;
				Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 70; i++)
		{
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fire].noGravity = true;
			Dust obj2 = Main.dust[fire];
			obj2.velocity *= 5f;
			fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fire];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			Vector2 goreSource = base.Projectile.Center;
			int goreAmt = 3;
			Vector2 source = default(Vector2);
			((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
			for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
			{
				float velocityMult = 0.33f;
				if (goreIndex < goreAmt / 3)
				{
					velocityMult = 0.66f;
				}
				if (goreIndex >= 2 * goreAmt / 3)
				{
					velocityMult = 1f;
				}
				ModContent.GetInstance<CalamityMod>();
				int type = Main.rand.Next(61, 64);
				int smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj4 = Main.gore[smoke2];
				obj4.velocity *= velocityMult;
				obj4.velocity.X++;
				obj4.velocity.Y++;
				type = Main.rand.Next(61, 64);
				smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj5 = Main.gore[smoke2];
				obj5.velocity *= velocityMult;
				obj5.velocity.X--;
				obj5.velocity.Y++;
				type = Main.rand.Next(61, 64);
				smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj6 = Main.gore[smoke2];
				obj6.velocity *= velocityMult;
				obj6.velocity.X++;
				obj6.velocity.Y--;
				type = Main.rand.Next(61, 64);
				smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj7 = Main.gore[smoke2];
				obj7.velocity *= velocityMult;
				obj7.velocity.X--;
				obj7.velocity.Y--;
			}
		}
		base.Projectile.ExpandHitboxBy(15);
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.ExplodeTiles(7);
		}
	}
}
