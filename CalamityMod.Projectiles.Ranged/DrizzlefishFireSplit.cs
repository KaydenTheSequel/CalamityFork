using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DrizzlefishFireSplit : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/DrizzlefishFire";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 90;
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
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

	public override void AI()
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		Player Owner = Main.player[base.Projectile.owner];
		if (Main.zenithWorld && Time == 1 && Owner.Calamity().dragoonDrizzlefishGelBoost > 1)
		{
			base.Projectile.damage = base.Projectile.damage * Owner.Calamity().dragoonDrizzlefishGelBoost;
		}
		base.Projectile.velocity.X *= 0.98f;
		base.Projectile.velocity.Y += 0.5f;
		int dustType = 235;
		dustType = ((base.Projectile.ai[1] == 1f) ? ((!Main.rand.NextBool()) ? 162 : 174) : ((!Main.rand.NextBool()) ? 90 : 183));
		for (int i = 0; i < 2; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f) - base.Projectile.velocity * 1.5f, dustType, -base.Projectile.velocity);
			dust.noGravity = true;
			dust.velocity *= 0f;
			dust.scale = ((Owner.Calamity().dragoonDrizzlefishGelBoost > 1) ? Main.rand.NextFloat(0f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f, 0.3f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f) : Main.rand.NextFloat(0.4f, 0.8f));
		}
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0f, 0f);
		if (base.Projectile.timeLeft > 90)
		{
			base.Projectile.timeLeft = 90;
		}
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.ai[1] == 1f)
		{
			target.AddBuff(323, 60);
		}
		else
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 30);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		int dustType = 235;
		for (int i = 0; i <= 9; i++)
		{
			dustType = ((base.Projectile.ai[1] != 1f) ? ((!Main.rand.NextBool()) ? 90 : 183) : ((!Main.rand.NextBool()) ? 162 : 174));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustType, Utils.RotatedByRandom(new Vector2(0f, -5f), MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust.noGravity = false;
			dust.scale = ((Owner.Calamity().dragoonDrizzlefishGelBoost > 1) ? Main.rand.NextFloat(0f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f, 0.6f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f) : Main.rand.NextFloat(0.4f, 1.1f));
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, dustType, Utils.RotatedByRandom(new Vector2(0f, -3f), MathHelper.ToRadians(25f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust2.noGravity = false;
			dust2.scale = ((Owner.Calamity().dragoonDrizzlefishGelBoost > 1) ? Main.rand.NextFloat(0f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f, 0.6f + (float)Owner.Calamity().dragoonDrizzlefishGelBoost * 0.5f) : Main.rand.NextFloat(0.4f, 1.1f));
		}
	}
}
