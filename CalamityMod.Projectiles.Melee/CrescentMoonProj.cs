using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CrescentMoonProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.alpha = 100;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 2;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
		base.Projectile.extraUpdates = 2;
		base.Projectile.aiStyle = -1;
		base.AIType = -1;
		base.Projectile.timeLeft = 240 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0f, 0.6f);
		if (base.Projectile.soundDelay == 0 && ((Vector2)(ref base.Projectile.velocity)).Length() > 0.1f)
		{
			base.Projectile.soundDelay = 60;
			SoundStyle style = SoundID.Item9 with
			{
				Volume = 0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
		}
		base.Projectile.rotation += (float)base.Projectile.direction * 0.15f;
		if (base.Projectile.FinalExtraUpdate())
		{
			GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, 0.65f, 0.65f, 2, fade: false), pixelate: true, GeneralDrawLayer.AfterProjectiles);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 0.1f, "CalamityMod/Projectiles/Melee/CrescentMoonProj", affectedByGravity: false, 2, 1f, Color.White, Vector2.One, useAddativeBlend: false), pixelate: false, GeneralDrawLayer.AfterProjectiles);
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.965f;
		if (base.Projectile.timeLeft < 225 * base.Projectile.MaxUpdates)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 12f, 20f, respectIFrames: true);
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target.CanBeChasedBy(base.Projectile) && target.Calamity().IsArmored())
		{
			return false;
		}
		return base.CanHitNPC(target);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, 109, 111, 132);
			int dust = Dust.NewDust(base.Projectile.Center, 0, 0, dustType);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 2f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
