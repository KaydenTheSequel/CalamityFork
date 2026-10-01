using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DrizzlefishFireball : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/DrizzlefishFire";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 5;
		base.Projectile.aiStyle = 14;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void AI()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		Player Owner = Main.player[base.Projectile.owner];
		if (Main.zenithWorld && Time == 1 && Owner.Calamity().dragoonDrizzlefishGelBoost > 1)
		{
			base.Projectile.damage = base.Projectile.damage * Owner.Calamity().dragoonDrizzlefishGelBoost;
		}
		base.Projectile.velocity.X *= 0.995f;
		base.Projectile.velocity.Y -= 0.065f;
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0f, 0f);
		int dustType = 235;
		int dustType2 = 235;
		dustType = ((base.Projectile.ai[1] == 1f) ? ((!Main.rand.NextBool()) ? 162 : 174) : ((!Main.rand.NextBool()) ? 90 : 183));
		if (Time > 7)
		{
			base.Projectile.alpha = 0;
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f) - base.Projectile.velocity * 1.5f, dustType, -base.Projectile.velocity);
				dust.noGravity = true;
				dust.velocity *= 0f;
				dust.scale = ((Owner.Calamity().dragoonDrizzlefishGelBoost > 1) ? Main.rand.NextFloat(0.4f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f, 1f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f) : Main.rand.NextFloat(0.9f, 1.5f));
			}
		}
		else
		{
			base.Projectile.alpha = 255;
		}
		if (Time == 4)
		{
			for (int j = 0; j <= 8; j++)
			{
				Dust obj = Dust.NewDustPerfect(Type: (base.Projectile.ai[1] != 1f) ? ((!Main.rand.NextBool()) ? 90 : 183) : ((!Main.rand.NextBool()) ? 162 : 174), Position: base.Projectile.Center, Velocity: base.Projectile.velocity);
				obj.scale = Main.rand.NextFloat(1.1f, 1.9f);
				obj.velocity = base.Projectile.velocity.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(0.3f, 1.3f);
				obj.noGravity = true;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 7)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value);
		}
		else if (base.Projectile.ai[1] == 1f)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/DrizzlefishFire2", (AssetRequestMode)2).Value);
		}
		else
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/DrizzlefishFire", (AssetRequestMode)2).Value);
		}
		if (base.Projectile.ai[1] == 1f)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/DrizzlefishFire2", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, 16, 16), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, 10f), base.Projectile.scale, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[1] == 1f)
		{
			target.AddBuff(323, 40);
		}
		else
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 20);
		}
	}
}
