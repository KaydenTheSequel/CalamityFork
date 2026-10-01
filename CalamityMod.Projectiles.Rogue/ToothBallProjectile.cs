using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ToothBallProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ToothBall";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 600;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 90);
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.Calamity().stealthStrike)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, -10f, ModContent.ProjectileType<StealthNimbusCloud>(), base.Projectile.damage / 2, 0f, base.Projectile.owner, 1f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 90);
	}
}
