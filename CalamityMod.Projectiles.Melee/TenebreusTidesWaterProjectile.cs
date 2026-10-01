using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TenebreusTidesWaterProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.aiStyle = 27;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = 300;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.tileCollide = false;
		base.AIType = 156;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 0f, 0f, (float)(255 - base.Projectile.alpha) * 1f / 255f);
		int water = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 0.4f);
		Main.dust[water].noGravity = true;
		Dust obj = Main.dust[water];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[water];
		obj2.velocity += base.Projectile.velocity * 0.1f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 300)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(50, 50, 255, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.position.X -= base.Projectile.width / 2;
		base.Projectile.position.Y -= base.Projectile.height / 2;
		Vector2 dustVel = default(Vector2);
		for (int dustIndex = 0; dustIndex <= 30; dustIndex++)
		{
			((Vector2)(ref dustVel))._002Ector((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
			float num = Main.rand.Next(3, 9);
			float dist = ((Vector2)(ref dustVel)).Length();
			dist = num / dist;
			dustVel.X *= dist;
			dustVel.Y *= dist;
			int water = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[water];
			obj.noGravity = true;
			obj.position.X = base.Projectile.Center.X;
			obj.position.Y = base.Projectile.Center.Y;
			obj.position.X += Main.rand.Next(-10, 11);
			obj.position.Y += Main.rand.Next(-10, 11);
			obj.velocity = dustVel;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		SwordSpam(target.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		SwordSpam(target.Center);
	}

	public void SwordSpam(Vector2 targetPos)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		int projAmt = 2;
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int i = 0; i < projAmt; i++)
		{
			int type = (Main.rand.NextBool() ? ModContent.ProjectileType<TenebreusTidesWaterSword>() : ModContent.ProjectileType<TenebreusTidesWaterSpear>());
			if (base.Projectile.owner == Main.myPlayer)
			{
				CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, Main.rand.NextBool(), 1000f, 1400f, 80f, 900f, Main.rand.NextFloat(25f, 35f), type, (int)((float)base.Projectile.damage * 0.4f), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
		}
	}
}
