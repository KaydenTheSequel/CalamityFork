using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PurityProjectionSigil : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	private NPC target => Main.npc[(int)base.Projectile.ai[0]];

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Projectiles/Melee/MendedBiomeBlade_PurityProjectionSigil";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = false;
		base.Projectile.hostile = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = TrueBiomeBlade.DefaultAttunement_SigilTime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.75f, 1f, 0.24f);
		int dustParticle = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 100, default(Color), 0.9f);
		Main.dust[dustParticle].noGravity = true;
		Dust obj = Main.dust[dustParticle];
		obj.velocity *= 0.5f;
		if (target.CanBeChasedBy(base.Projectile))
		{
			base.Projectile.Center = target.Center;
			if (base.Projectile.ai[1] == 1f)
			{
				target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 5);
			}
		}
		else
		{
			base.Projectile.active = false;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
