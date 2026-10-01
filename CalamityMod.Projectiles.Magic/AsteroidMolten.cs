using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AsteroidMolten : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.position.Y > Main.player[base.Projectile.owner].position.Y - 300f)
		{
			base.Projectile.tileCollide = true;
		}
		if ((double)base.Projectile.position.Y < Main.worldSurface * 16.0)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.scale = base.Projectile.ai[1];
		base.Projectile.rotation += base.Projectile.velocity.X * 2f;
		Vector2 position = base.Projectile.Center + Vector2.Normalize(base.Projectile.velocity) * 10f;
		Dust obj = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 0, new Color(255, Main.DiscoG, 0))];
		obj.position = position;
		obj.velocity = base.Projectile.velocity.RotatedBy(1.5707963705062866) * 0.33f + base.Projectile.velocity / 4f;
		obj.position += base.Projectile.velocity.RotatedBy(1.5707963705062866);
		obj.fadeIn = 0.5f;
		obj.noGravity = true;
		Dust obj2 = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 0, new Color(255, Main.DiscoG, 0))];
		obj2.position = position;
		obj2.velocity = base.Projectile.velocity.RotatedBy(-1.5707963705062866) * 0.33f + base.Projectile.velocity / 4f;
		obj2.position += base.Projectile.velocity.RotatedBy(-1.5707963705062866);
		obj2.fadeIn = 0.5f;
		obj2.noGravity = true;
		int fiery = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 0, new Color(255, Main.DiscoG, 0));
		Dust obj3 = Main.dust[fiery];
		obj3.velocity *= 0.5f;
		Main.dust[fiery].scale *= 1.3f;
		Main.dust[fiery].fadeIn = 1f;
		Main.dust[fiery].noGravity = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item89, base.Projectile.Center);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = (int)(128f * base.Projectile.scale);
		base.Projectile.height = (int)(128f * base.Projectile.scale);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 8; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 1.5f);
		}
		for (int j = 0; j < 32; j++)
		{
			int killFire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 2.5f);
			Main.dust[killFire].noGravity = true;
			Dust obj = Main.dust[killFire];
			obj.velocity *= 3f;
			killFire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 1.5f);
			Dust obj2 = Main.dust[killFire];
			obj2.velocity *= 2f;
			Main.dust[killFire].noGravity = true;
		}
		if (!Main.dedServ)
		{
			for (int k = 0; k < 2; k++)
			{
				int gored = Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position + new Vector2((float)(base.Projectile.width * Main.rand.Next(100)) / 100f, (float)(base.Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
				Gore obj3 = Main.gore[gored];
				obj3.velocity *= 0.3f;
				obj3.velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
				obj3.velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
			}
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.localAI[1] = -1f;
			base.Projectile.maxPenetrate = 0;
			base.Projectile.Damage();
		}
		for (int l = 0; l < 5; l++)
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, 244, 259, 158);
			int exploding = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 2.5f * (float)base.Projectile.direction, -2.5f, 0, new Color(255, Main.DiscoG, 0));
			Main.dust[exploding].alpha = 200;
			Dust obj4 = Main.dust[exploding];
			obj4.velocity *= 2.4f;
			Main.dust[exploding].scale += Main.rand.NextFloat();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(323, 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		switch ((int)base.Projectile.ai[0])
		{
		case 1:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMolten2", (AssetRequestMode)2).Value;
			break;
		case 2:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMolten3", (AssetRequestMode)2).Value;
			break;
		case 3:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMolten4", (AssetRequestMode)2).Value;
			break;
		case 4:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMolten5", (AssetRequestMode)2).Value;
			break;
		case 5:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMolten6", (AssetRequestMode)2).Value;
			break;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, texture);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMoltenGlow", (AssetRequestMode)2).Value;
		switch ((int)base.Projectile.ai[0])
		{
		case 1:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMoltenGlow2", (AssetRequestMode)2).Value;
			break;
		case 2:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMoltenGlow3", (AssetRequestMode)2).Value;
			break;
		case 3:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMoltenGlow4", (AssetRequestMode)2).Value;
			break;
		case 4:
			return;
		case 5:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/AsteroidMoltenGlow6", (AssetRequestMode)2).Value;
			break;
		}
		Vector2 origin = texture.Size() / 2f;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
	}
}
