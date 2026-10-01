using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class GraveGrimreaverProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/GraveGrimreaver";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 80;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 210;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 22;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.position);
		}
		if (base.Projectile.timeLeft % 90 == 0)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0.2f, ModContent.ProjectileType<GrimreaverSkull>(), (int)((float)base.Projectile.damage * 0.4f), base.Projectile.knockBack, base.Projectile.owner, 1f);
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 250f, 4f, 14f);
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 1f).ToDirectionInt());
		base.Projectile.rotation += 0.1f * (float)base.Projectile.spriteDirection;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(31, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(31, 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
		if (base.Projectile.Calamity().stealthStrike)
		{
			SoundStyle style = SoundID.NPCDeath52 with
			{
				Volume = SoundID.NPCDeath52.Volume * 0.75f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			SpawnBats(10, -12, 12);
			DustExplosion(15, 6, 12, 30, 2.4f);
			for (int i = 0; i < 8; i++)
			{
				CalamityUtils.ProjectileRain(base.Projectile.GetSource_FromThis(), base.Projectile.Center, 600f, 100f, 700f, 1000f, 20f, ModContent.ProjectileType<GrimreaverSkull>(), (int)((float)base.Projectile.damage * 0.35f), 3f, base.Projectile.owner);
			}
		}
		else
		{
			SpawnBats(4, -12, 12);
			DustExplosion(10, 3, 9, 20, 2.15f);
		}
	}

	public void SpawnBats(int amount, int minspread, int maxspread)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 speed = default(Vector2);
			for (int i = 0; i < amount; i++)
			{
				((Vector2)(ref speed))._002Ector(Main.rand.NextFloat(minspread, maxspread), Main.rand.NextFloat(minspread, maxspread));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, speed, ModContent.ProjectileType<GrimreaverBat>(), (int)((float)base.Projectile.damage * 0.25f), 0f, base.Projectile.owner, 4f);
			}
		}
	}

	public void DustExplosion(int spreadspeed, int minspeed, int maxspeed, int amount, float size)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < amount; i++)
		{
			float random1 = Main.rand.Next(-spreadspeed, spreadspeed + 1);
			float random2 = Main.rand.Next(-spreadspeed, spreadspeed + 1);
			float num = Main.rand.Next(minspeed, maxspeed);
			float randomAdjust = (float)Math.Sqrt(random1 * random1 + random2 * random2);
			randomAdjust = num / randomAdjust;
			random1 *= randomAdjust;
			random2 *= randomAdjust;
			int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 0, default(Color), size);
			Dust obj = Main.dust[d];
			obj.noGravity = true;
			obj.position.X = base.Projectile.Center.X;
			obj.position.Y = base.Projectile.Center.Y;
			obj.position.X += Main.rand.Next(-spreadspeed, spreadspeed + 1);
			obj.position.Y += Main.rand.Next(-spreadspeed, spreadspeed + 1);
			obj.velocity.X = random1;
			obj.velocity.Y = random2;
		}
	}
}
