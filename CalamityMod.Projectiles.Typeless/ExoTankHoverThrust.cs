using CalamityMod.Items.Mounts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ExoTankHoverThrust : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 186;
		base.Projectile.height = 74;
		base.Projectile.friendly = true;
	}

	public override void AI()
	{
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null || Owner.dead || Owner.mount == null || !Owner.mount.Active)
		{
			base.Projectile.Kill();
		}
		if (Owner.mount._type != ModContent.MountType<ExoTank>())
		{
			base.Projectile.Kill();
		}
		if (!((ExoTank.ExoTankData)Owner.mount._mountSpecificData).Hovering)
		{
			base.Projectile.Kill();
		}
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 3 % Main.projFrames[base.Type];
		base.Projectile.Center = Owner.Bottom + Vector2.UnitY * (float)(base.Projectile.height / 2);
		base.Projectile.spriteDirection = (base.Projectile.direction = Owner.direction);
		base.Projectile.timeLeft = 2;
		Dust dust = Dust.NewDustPerfect(base.Projectile.Top - Vector2.UnitX * Main.rand.NextFloat(44f, 64f) * (float)Owner.direction, 59);
		dust.noGravity = true;
		dust.noLight = true;
		dust.velocity = Owner.velocity + Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(6f)) * Main.rand.NextFloat(5f, 12f);
		dust.scale = Main.rand.NextFloat(0.5f, 1.5f);
		Dust dust2 = Dust.NewDustPerfect(base.Projectile.Top + Vector2.UnitX * Main.rand.NextFloat(30f, 50f) * (float)Owner.direction, 59);
		dust2.noGravity = true;
		dust2.noLight = true;
		dust2.velocity = Owner.velocity + Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(6f)) * Main.rand.NextFloat(5f, 12f);
		dust2.scale = Main.rand.NextFloat(0.5f, 1.5f);
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
