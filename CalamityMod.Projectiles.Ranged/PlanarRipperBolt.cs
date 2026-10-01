using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PlanarRipperBolt : ModProjectile, ILocalizedModType, IModType
{
	public static int frameWidth = 12;

	public static int frameHeight = 26;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Rogue/ShockBolt";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 10;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.aiStyle = 1;
		base.AIType = 242;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		target.AddBuff(144, 180);
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (target.life <= 0 && target.realLife == -1)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<PlanarRipperExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			if (hit.Crit && modPlayer.planarSpeedBoost < 20)
			{
				modPlayer.planarSpeedBoost++;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		target.AddBuff(144, 180);
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (target.statLife <= 0)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<PlanarRipperExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			if (modPlayer.planarSpeedBoost < 20)
			{
				modPlayer.planarSpeedBoost++;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.6f);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundStyle sound = (Main.rand.NextBool() ? SoundID.Item93 : SoundID.Item92);
		SoundEngine.PlaySound(sound with
		{
			Volume = sound.Volume * 0.5f
		}, base.Projectile.position);
		for (int i = 0; i < 5; i++)
		{
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, 132, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 0.5f);
			Main.dust[dust].noGravity = true;
		}
		int rando = Main.rand.Next(10, 20);
		for (int j = 0; j < rando; j++)
		{
			int dusty = Dust.NewDust(base.Projectile.Center - base.Projectile.velocity / 2f, 0, 0, 135, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[dusty];
			obj.velocity *= 2f;
			Main.dust[dusty].noGravity = true;
		}
	}
}
