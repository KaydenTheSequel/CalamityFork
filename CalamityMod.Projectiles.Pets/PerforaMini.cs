using CalamityMod.Buffs.Pets;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class PerforaMini : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Type], 6).WithOffset(-8f, -20f).WithSpriteDirection(1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 perfcenter = base.Projectile.Center;
		Vector2 vectorperf = player.Center - perfcenter;
		float playerdistance = ((Vector2)(ref vectorperf)).Length();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (!player.HasBuff(ModContent.BuffType<BloodBound>()) || playerdistance >= 4000f)
		{
			base.Projectile.Kill();
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.perfmini = false;
		}
		if (modPlayer.perfmini)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.FloatingPetAI(faceRight: true, 0.1f);
		if (Main.rand.NextBool(50))
		{
			int d1 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 100, default(Color), 1.5f);
			int d2 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 170, 0f, 0f, 170, default(Color), 0.5f);
			Main.dust[d2].noLight = true;
			Main.dust[d1].position = base.Projectile.Center;
			Main.dust[d2].position = base.Projectile.Center;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
	}
}
